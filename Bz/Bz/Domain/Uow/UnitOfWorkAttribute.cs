using System;
using System.Reflection;
using System.Transactions;

namespace Bz.Domain.Uow
{
    /// <summary>
    /// 该特性用于指示声明的方法将会启用UOW
    /// 在工作单元末尾会自动将所有改变提交到数据库
    /// 如果失败则回滚
    /// 这个特性会失效，如果已经有UOW在此之前应用，所以会使用相同的UOW
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public  class UnitOfWorkAttribute:Attribute
    {
        /// <summary>
        /// 作用于选项.
        /// </summary>
        public TransactionScopeOption? Scope { get; set; }

        /// <summary>
        /// 是否是启用事务?
        /// 如果没有应用则启用默认值.
        /// </summary>
        public bool? IsTransactional { get; private set; }

        /// <summary>
        /// 事务超时时间，毫秒计算
        /// Uses default value if not supplied.
        /// </summary>
        public TimeSpan? Timeout { get; private set; }

        /// <summary>
        ///如果工作单元是事务级别的，该属性设置事务隔离级别
        /// Uses default value if not supplied.
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// 禁止启用事务
        /// 如果已经有一个事务存在，该属性就忽略
        /// 默认：False
        /// </summary>
        public bool IsDisabled { get; set; }

        public UnitOfWorkAttribute()
        {

        }

        public UnitOfWorkAttribute(bool isTransactional)
        {
            IsTransactional = isTransactional;
        }

        public UnitOfWorkAttribute(int timeout)
        {
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }
        public UnitOfWorkAttribute(bool isTransactional, int timeout)
        {
            IsTransactional = isTransactional;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        public UnitOfWorkAttribute(IsolationLevel isolationLevel)
        {
            IsTransactional = true;
            IsolationLevel = isolationLevel;
        }
        public UnitOfWorkAttribute(IsolationLevel isolationLevel, int timeout)
        {
            IsTransactional = true;
            IsolationLevel = isolationLevel;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        public UnitOfWorkAttribute(TransactionScopeOption scope)
        {
            IsTransactional = true;
            Scope = scope;
        }

        public UnitOfWorkAttribute(TransactionScopeOption scope, int timeout)
        {
            IsTransactional = true;
            Scope = scope;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// 获取给定的方法的UnitOfWorkAttribute，没查到返回null
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        internal static UnitOfWorkAttribute GetUnitOfWorkAttributeOrNull(MemberInfo methodInfo)
        {
            var attrs = methodInfo.GetCustomAttributes(typeof(UnitOfWorkAttribute),false);

            if (attrs.Length>0)
            {
                return (UnitOfWorkAttribute)attrs[0];
            }

            if (UnitOfWorkHelper.IsConventionalUowClass(methodInfo.DeclaringType))
            {
                return new UnitOfWorkAttribute();//默认
            }
            return null;
        }

        internal UnitOfWorkOptions CreateOptions()
        {
            return new UnitOfWorkOptions
            {
                IsTransactional = IsTransactional,
                IsolationLevel = IsolationLevel,
                Timeout = Timeout,
                Scope = Scope
            };
        }
    }
}
