using Bz;

namespace System.Data
{
    /// <summary>
    /// 事务隔离级别定义
    /// </summary>
    public static class IsolationLevelExtensions
    {
        /// <summary>
        /// 转化 <see cref="System.Transactions.IsolationLevel"/> to <see cref="System.Data.IsolationLevel"/>.
        /// </summary>
        public static IsolationLevel ToSystemDataIsolationLevel(this System.Transactions.IsolationLevel isolationLevel)
        {
            switch (isolationLevel)
            {
                case System.Transactions.IsolationLevel.Chaos:
                    return IsolationLevel.Chaos;
                case System.Transactions.IsolationLevel.ReadCommitted:
                    return IsolationLevel.ReadCommitted;
                case System.Transactions.IsolationLevel.ReadUncommitted:
                    return IsolationLevel.ReadUncommitted;
                case System.Transactions.IsolationLevel.RepeatableRead:
                    return IsolationLevel.RepeatableRead;
                case System.Transactions.IsolationLevel.Serializable:
                    return IsolationLevel.Serializable;
                case System.Transactions.IsolationLevel.Snapshot:
                    return IsolationLevel.Snapshot;
                case System.Transactions.IsolationLevel.Unspecified:
                    return IsolationLevel.Unspecified;
                default:
                    throw new BzException("未知别的Isolation Level: " + isolationLevel);
            }

        }
    }
}
