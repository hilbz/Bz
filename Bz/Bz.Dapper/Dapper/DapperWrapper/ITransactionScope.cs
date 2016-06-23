using System;

namespace Bz.DapperWrapper
{
    public interface ITransactionScope : IDisposable
    {
        void Complete();
    }
}
