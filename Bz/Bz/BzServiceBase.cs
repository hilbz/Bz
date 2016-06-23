using Bz.Domain.Uow;
using Castle.Core.Logging;

namespace Bz
{
    /// <summary>
    /// 作为应用层实体基类
    /// </summary>
    public abstract class BzServiceBase
    {
        /// <summary>
        /// Reference to <see cref="IUnitOfWorkManager"/>.
        /// </summary>
        public IUnitOfWorkManager UnitOfWorkManager
        {
            get
            {
                if (_unitOfWorkManager == null)
                {
                    throw new BzException("在使用工作单元之前必须为其赋值.");
                }

                return _unitOfWorkManager;
            }
            set { _unitOfWorkManager = value; }
        }
        private IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// 获取当前的UoW.
        /// </summary>
        protected IActiveUnitOfWork CurrentUnitOfWork { get { return UnitOfWorkManager.Current; } }

        /// <summary>
        /// 引用一个Logger用来写日记.
        /// </summary>
        public ILogger Logger { protected get; set; }

        protected BzServiceBase()
        {
            Logger = NullLogger.Instance;
        }
    }
}
