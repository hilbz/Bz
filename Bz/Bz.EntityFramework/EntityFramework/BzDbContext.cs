using Bz.Domain.Entities;
using Bz.Domain.Uow;
using Bz.Runtime.Session;
using Castle.Core;
using Castle.Core.Logging;
using EntityFramework.DynamicFilters;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bz.EntityFramework
{
    public abstract class BzDbContext : DbContext, IShouldInitialize, IInitializable
    {
        /// <summary>
        /// 获取当前Session的值.
        /// </summary>
        public IBzSession BzSession { get; set; }

        public ILogger Logger { get; set; }

        public IGuidGenerator GuidGenerator { get; set; }

        protected BzDbContext()
        {
            SetNullsForInjectedProperties();
        }

        protected BzDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            SetNullsForInjectedProperties();
        }

        protected BzDbContext(DbCompiledModel model)
            : base(model)
        {
            SetNullsForInjectedProperties();
        }

        protected BzDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            SetNullsForInjectedProperties();
        }

        protected BzDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
            SetNullsForInjectedProperties();
        }

        private void SetNullsForInjectedProperties()
        {
            Logger = NullLogger.Instance;
            BzSession = NullBzSession.Instance;
            GuidGenerator = SequentialGuidGenerator.Instance;
        }

        public virtual void Initialize()
        {
            Database.Initialize(false);
            this.SetFilterScopedParameterValue(BzDataFilters.MustHaveTenant, BzDataFilters.Parameters.TenantId, BzSession.TenantId ?? 0);
            this.SetFilterScopedParameterValue(BzDataFilters.MayHaveTenant, BzDataFilters.Parameters.TenantId, BzSession.TenantId);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Filter(BzDataFilters.SoftDelete, (ISoftDelete d) => d.IsDeleted, false);
            modelBuilder.Filter(BzDataFilters.MustHaveTenant, (IMustHaveTenant t, int tenantId) => t.TenantId == tenantId, 0);
            modelBuilder.Filter(BzDataFilters.MayHaveTenant, (IMayHaveTenant t, int? tenantId) => t.TenantId == tenantId, 0);
        }

        public override int SaveChanges()
        {
            try
            {
                ApplyBzConcepts();
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                LogDbEntityValidationException(ex);
                throw;
            }
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                ApplyBzConcepts();
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbEntityValidationException ex)
            {
                LogDbEntityValidationException(ex);
                throw;
            }
        }

        protected virtual void ApplyBzConcepts()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        CheckAndSetId(entry);
                        SetCreationAuditProperties(entry);
                        CheckAndSetTenantIdProperty(entry);
                        break;
                    case EntityState.Modified:
                        CheckAndSetTenantIdProperty(entry);

                        if (entry.Entity is ISoftDelete && entry.Entity.As<ISoftDelete>().IsDeleted)
                        {
                            SetDeletionAuditProperties(entry);
                        }
                        else
                        {
                        }

                        break;
                    case EntityState.Deleted:
                        HandleSoftDelete(entry);
                        break;
                }
            }
        }

        protected virtual void CheckAndSetId(DbEntityEntry entry)
        {
            if (entry.Entity is IEntity<Guid>)
            {
                var entity = entry.Entity as IEntity<Guid>;
                if (entity.IsTransient())
                {
                    entity.Id = GuidGenerator.Create();
                }
            }
        }

        protected virtual void CheckAndSetTenantIdProperty(DbEntityEntry entry)
        {
            if (entry.Entity is IMustHaveTenant)
            {
                CheckAndSetMustHaveTenant(entry);
            }
            else if (entry.Entity is IMayHaveTenant)
            {
                CheckMayHaveTenant(entry);
            }
        }

        protected virtual void CheckAndSetMustHaveTenant(DbEntityEntry entry)
        {
            var entity = entry.Cast<IMustHaveTenant>().Entity;

            if (!this.IsFilterEnabled(BzDataFilters.MustHaveTenant))
            {
                if (BzSession.TenantId != null && entity.TenantId == 0)
                {
                    entity.TenantId = BzSession.GetTenantId();
                }

                return;
            }

            var currentTenantId = (int)this.GetFilterParameterValue(BzDataFilters.MustHaveTenant, BzDataFilters.Parameters.TenantId);

            if (currentTenantId == 0)
            {
                throw new DbEntityValidationException("不能保存一个IMustHaveTenant实体当MustHaveTenant filter is enabledand current filter parameter value is not set (Probably, no tenant user logged in)");
            }

            if (entity.TenantId == 0)
            {
                entity.TenantId = currentTenantId;
            }
            else if (entity.TenantId != currentTenantId && entity.TenantId != BzSession.TenantId)
            {
                throw new DbEntityValidationException("不能保存一个IMustHaveTenant实体 to (要修改的数据和登录ID不一致) or IBzSession.TenantId while MustHaveTenant filter is enabled!");
            }
        }
        protected virtual void CheckMayHaveTenant(DbEntityEntry entry)
        {
            if (!this.IsFilterEnabled(BzDataFilters.MayHaveTenant))
            {
                return;
            }

            var currentTenantId = (int?)this.GetFilterParameterValue(BzDataFilters.MayHaveTenant, BzDataFilters.Parameters.TenantId);

            var entity = entry.Cast<IMayHaveTenant>().Entity;

            if (entity.TenantId != currentTenantId && entity.TenantId != BzSession.TenantId)
            {
                throw new DbEntityValidationException("Can not set TenantId to a different value than the current filter parameter value or IBzSession.TenantId while MayHaveTenant filter is enabled!");
            }
        }
        protected virtual void SetCreationAuditProperties(DbEntityEntry entry)
        {
            //if (entry.Entity is IHasCreationTime)
            //{
            //    entry.Cast<IHasCreationTime>().Entity.CreationTime = Clock.Now;
            //}

            //if (entry.Entity is ICreationAudited && AbpSession.UserId.HasValue)
            //{
            //    entry.Cast<ICreationAudited>().Entity.CreatorUserId = AbpSession.UserId;
            //}
        }

        protected virtual void HandleSoftDelete(DbEntityEntry entry)
        {
            if (!(entry.Entity is ISoftDelete))
            {
                return;
            }

            var softDeleteEntry = entry.Cast<ISoftDelete>();

            softDeleteEntry.State = EntityState.Unchanged;
            softDeleteEntry.Entity.IsDeleted = true;

            SetDeletionAuditProperties(entry);
        }
        protected virtual void SetDeletionAuditProperties(DbEntityEntry entry)
        {
            //if (entry.Entity is IHasDeletionTime)
            //{
            //    entry.Cast<IHasDeletionTime>().Entity.DeletionTime = Clock.Now;
            //}

            //if (entry.Entity is IDeletionAudited && AbpSession.UserId.HasValue)
            //{
            //    entry.Cast<IDeletionAudited>().Entity.DeleterUserId = AbpSession.UserId;
            //}
        }
        private void LogDbEntityValidationException(DbEntityValidationException exception)
        {
            Logger.Error("在用EntityFramework进行数据保存时出现错误:");
            foreach (var ve in exception.EntityValidationErrors.SelectMany(eve => eve.ValidationErrors))
            {
                Logger.Error(" - " + ve.PropertyName + ": " + ve.ErrorMessage);
            }
        }
    }
}
