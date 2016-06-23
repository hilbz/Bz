﻿using Bz.Dependency;
using Bz.Domain.Uow;
using Bz.Reflection;
using Castle.Core.Internal;
using EntityFramework.DynamicFilters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Bz.EntityFramework.Uow
{
    public class EfUnitOfWork:UnitOfWorkBase,ITransientDependency
    {
        protected readonly IDictionary<Type, DbContext> ActiveDbContexts;

        protected IIocResolver IocResolver { get; private set; }

        protected TransactionScope CurrentTransaction;

        public EfUnitOfWork(IIocResolver iocResolver, IUnitOfWorkDefaultOptions defaultOptions)
             : base(defaultOptions)
        {
            IocResolver = iocResolver;
            ActiveDbContexts = new Dictionary<Type, DbContext>();
        }

        protected override void BeginUow()
        {
            if (Options.IsTransactional == true)
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = Options.IsolationLevel.GetValueOrDefault(IsolationLevel.ReadUncommitted),
                };

                if (Options.Timeout.HasValue)
                {
                    transactionOptions.Timeout = Options.Timeout.Value;
                }

                CurrentTransaction = new TransactionScope(
                    Options.Scope.GetValueOrDefault(TransactionScopeOption.Required),
                    transactionOptions,
                    Options.AsyncFlowOption.GetValueOrDefault(TransactionScopeAsyncFlowOption.Enabled)
                    );
            }
        }

        public override void SaveChanges()
        {
            ActiveDbContexts.Values.ForEach(SaveChangesInDbContext);
        }

        public override async Task SaveChangesAsync()
        {
            foreach (var dbContext in ActiveDbContexts.Values)
            {
                await SaveChangesInDbContextAsync(dbContext);
            }
        }

        protected override void CompleteUow()
        {
            SaveChanges();
            if (CurrentTransaction != null)
            {
                CurrentTransaction.Complete();
            }
        }

        protected override async Task CompleteUowAsync()
        {
            await SaveChangesAsync();
            if (CurrentTransaction != null)
            {
                CurrentTransaction.Complete();
            }
        }

        protected override void ApplyDisableFilter(string filterName)
        {
            foreach (var activeDbContext in ActiveDbContexts.Values)
            {
                activeDbContext.DisableFilter(filterName);
            }
        }

        protected override void ApplyEnableFilter(string filterName)
        {
            foreach (var activeDbContext in ActiveDbContexts.Values)
            {
                activeDbContext.EnableFilter(filterName);
            }
        }

        protected override void ApplyFilterParameterValue(string filterName, string parameterName, object value)
        {
            foreach (var activeDbContext in ActiveDbContexts.Values)
            {
                if (TypeHelper.IsFunc<object>(value))
                {
                    activeDbContext.SetFilterScopedParameterValue(filterName, parameterName, (Func<object>)value);
                }
                else
                {
                    activeDbContext.SetFilterScopedParameterValue(filterName, parameterName, value);
                }
            }
        }

        public virtual TDbContext GetOrCreateDbContext<TDbContext>()
      where TDbContext : DbContext
        {
            DbContext dbContext;
            if (!ActiveDbContexts.TryGetValue(typeof(TDbContext), out dbContext))
            {
                dbContext = Resolve<TDbContext>();

                foreach (var filter in Filters)
                {
                    if (filter.IsEnabled)
                    {
                        dbContext.EnableFilter(filter.FilterName);
                    }
                    else
                    {
                        dbContext.DisableFilter(filter.FilterName);
                    }

                    foreach (var filterParameter in filter.FilterParameters)
                    {
                        if (TypeHelper.IsFunc<object>(filterParameter.Value))
                        {
                            dbContext.SetFilterScopedParameterValue(filter.FilterName, filterParameter.Key, (Func<object>)filterParameter.Value);
                        }
                        else
                        {
                            dbContext.SetFilterScopedParameterValue(filter.FilterName, filterParameter.Key, filterParameter.Value);
                        }
                    }
                }

                ActiveDbContexts[typeof(TDbContext)] = dbContext;
            }

            return (TDbContext)dbContext;
        }

        protected override void DisposeUow()
        {
            ActiveDbContexts.Values.ForEach(Release);

            if (CurrentTransaction != null)
            {
                CurrentTransaction.Dispose();
            }
        }
        protected virtual void SaveChangesInDbContext(DbContext dbContext)
        {
            dbContext.SaveChanges();
        }

        protected virtual async Task SaveChangesInDbContextAsync(DbContext dbContext)
        {
            await dbContext.SaveChangesAsync();
        }
        protected virtual TDbContext Resolve<TDbContext>()
        {
            return IocResolver.Resolve<TDbContext>();
        }

        protected virtual void Release(DbContext dbContext)
        {
            dbContext.Dispose();
            IocResolver.Release(dbContext);
        }
    }
}
