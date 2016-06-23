using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Domain.Uow
{
    /// <summary>
    /// Null实现 Of Uow.
    /// 用于无任何注入时<see cref="IUnitOfWork"/>.
    /// 在不适用数据库时.
    /// </summary>
    public sealed class NullUnitOfWork : UnitOfWorkBase
    {
        public override void SaveChanges()
        {

        }

        public async override Task SaveChangesAsync()
        {

        }

        protected override void BeginUow()
        {

        }

        protected override void CompleteUow()
        {

        }

        protected async override Task CompleteUowAsync()
        {

        }

        protected override void DisposeUow()
        {

        }

        public NullUnitOfWork(IUnitOfWorkDefaultOptions defaultOptions)
            : base(defaultOptions)
        {
        }
    }
}
