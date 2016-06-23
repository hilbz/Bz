using Bz.Domain.Uow;
using Bz.Runtime.Session;
using Castle.Core.Logging;
using System.Web.Http;

namespace Bz.WebApi.Controllers
{
    public abstract class BzApiController:ApiController
    {
        /// <summary>
        /// 获取当前session的信息.
        /// </summary>
        public IBzSession BzSession { get; set; }

        /// <summary>
        /// 引用一个日记管理用于记录logs.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Reference to <see cref="IUnitOfWorkManager"/>.
        /// </summary>
        public IUnitOfWorkManager UnitOfWorkManager
        {
            get
            {
                if (_unitOfWorkManager == null)
                {
                    throw new BzException("再使用工作单元之前必须设置他.");
                }

                return _unitOfWorkManager;
            }
            set { _unitOfWorkManager = value; }
        }

        private IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// 获取当前的工作单元.
        /// </summary>
        protected IActiveUnitOfWork CurrentUnitOfWork { get { return UnitOfWorkManager.Current; } }

        protected BzApiController()
        {
            BzSession = NullBzSession.Instance;
            Logger = NullLogger.Instance;
        }

    }
}
