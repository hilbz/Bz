using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Domain.Uow
{
    /// <summary>
    /// 这个处理用内部工作单元范畴
    /// 实际上是引用Outer Unit Of Work Scope
    /// 对<see cref="IUnitOfWorkCompleteHandle.Complete"/>来说是没影响
    /// 但是如果没有被调用的话，在UOW完成的时候会抛出异常
    /// </summary>
    internal class InnerUnitOfWorkCompleteHandle:IUnitOfWorkCompleteHandle
    {
        public const string DidNotCallCompleteMethodExceptionMessage = "没有调用UOW完成方法";

        private volatile bool _isCompleteCalled;
        private volatile bool _isDisposed;

        public void Complete()
        {
            _isCompleteCalled = true;
        }

        public async Task CompleteAsync()
        {
            _isCompleteCalled = true;
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }
            _isDisposed = true;

            if (!_isCompleteCalled)
            {
                if (HasException())
                {
                    return;
                }
                throw new BzException(DidNotCallCompleteMethodExceptionMessage);
            }
        }

        private static bool HasException()
        {
            try
            {
                return Marshal.GetExceptionCode() != 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
