using Bz.Dependency;
using Castle.Core;
using Castle.Core.Logging;
using System.Collections.Concurrent;
using System.Runtime.Remoting.Messaging;

namespace Bz.Domain.Uow
{
    /// <summary>
    /// 利用CallContext获取当前的UOW<see cref="ICurrentUnitOfWorkProvider"/>
    /// </summary>
    public class CallContextCurrentUnitOfWorkProvider:ICurrentUnitOfWorkProvider,ITransientDependency
    {
        public ILogger Logger { get; set; }

        private const string ContextKey = "Bz.UnitOfWork.Current";

        //TODO:是否应该周期性清除..
        private static readonly ConcurrentDictionary<string, IUnitOfWork> UnitOfWorkDictionary = new ConcurrentDictionary<string, IUnitOfWork>();

        public CallContextCurrentUnitOfWorkProvider()
        {
            Logger = NullLogger.Instance;
        }

        private static IUnitOfWork GetCurrentUow(ILogger logger)
        {
            var unitOfWorkKey = CallContext.LogicalGetData(ContextKey) as string;
            if (unitOfWorkKey==null)
                return null;
            IUnitOfWork unitOfWork;
            if (!UnitOfWorkDictionary.TryGetValue(unitOfWorkKey, out unitOfWork))
            {
                CallContext.FreeNamedDataSlot(ContextKey);
                return null;
            }

            if (unitOfWork.IsDisposed)
            {
                UnitOfWorkDictionary.TryRemove(unitOfWorkKey, out unitOfWork);
                CallContext.FreeNamedDataSlot(ContextKey);
                return null;
            }

            return unitOfWork;
        }

        private static void SetCurrentUow(IUnitOfWork value, ILogger logger)
        {
            if (value==null)
            {
                ExistFromCurrentUowScope(logger);
                return;
            }
            var unitOfWorkKey = CallContext.LogicalGetData(ContextKey) as string;

            if (unitOfWorkKey != null)
            {
                IUnitOfWork outer;
                if (UnitOfWorkDictionary.TryGetValue(unitOfWorkKey, out outer))
                {
                    if (outer == value)
                    {
                        return;
                    }

                    value.Outer = outer;
                }
                else
                {
                }
            }
            unitOfWorkKey = value.Id;
            if (!UnitOfWorkDictionary.TryAdd(unitOfWorkKey, value))
            {
                throw new BzException("不能设置工作单元! UnitOfWorkDictionary.TryAdd returns false!");
            }

            CallContext.LogicalSetData(ContextKey, unitOfWorkKey);
        }

        private static void ExistFromCurrentUowScope(ILogger logger)
        {
            var unitOfWorkKey = CallContext.LogicalGetData(ContextKey) as string;
            if (unitOfWorkKey == null)
                return;
            IUnitOfWork unitOfWork;

            if (!UnitOfWorkDictionary.TryGetValue(unitOfWorkKey,out unitOfWork))
            {
                CallContext.FreeNamedDataSlot(ContextKey);
                return;
            }

            UnitOfWorkDictionary.TryRemove(unitOfWorkKey, out unitOfWork);

            if (unitOfWork.Outer==null)
            {
                CallContext.FreeNamedDataSlot(ContextKey);
                return;
            }

            //重新存储Outer UOW
            var outerUnitOfWorkKey = unitOfWork.Outer.Id;
            if (!UnitOfWorkDictionary.TryGetValue(outerUnitOfWorkKey,out unitOfWork))
            {
                //外部的UOW为null
                CallContext.FreeNamedDataSlot(ContextKey);
                return;
            }

            CallContext.LogicalSetData(ContextKey, outerUnitOfWorkKey);
        }

        [DoNotWire]
        public IUnitOfWork Current
        {
            get { return GetCurrentUow(Logger); }
            set { SetCurrentUow(value, Logger); }
        }
    }
}
