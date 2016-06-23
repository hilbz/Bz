using Bz.Configuration.Startup;
using Bz.Dependency;
using Bz.MultiTenancy;
using Bz.Runtime.Session;

namespace Bz.TestBase.Runtime.Session
{
    public class TestBzSession:IBzSession,ISingletonDependency
    {
        public long? UserId { get; set; }

        public int? TenantId
        {
            get
            {
                if (!_multiTenancy.IsEnabled)
                {
                    return 1;
                }

                return _tenantId;
            }
            set
            {
                if (!_multiTenancy.IsEnabled && value != 1)
                {
                    throw new BzException("多租户未开启，不能设置TenantId的值");
                }

                _tenantId = value;
            }
        }

        public MultiTenancySides MultiTenancySide { get { return GetCurrentMultiTenancySide(); } }

        public long? ImpersonatorUserId { get; set; }
        public int? ImpersonatorTenantId { get; set; }

        private readonly IMultiTenancyConfig _multiTenancy;

        private int? _tenantId;

        public TestBzSession(IMultiTenancyConfig multiTenancy)
        {
            _multiTenancy = multiTenancy;
        }
        private MultiTenancySides GetCurrentMultiTenancySide()
        {
            return _multiTenancy.IsEnabled && !TenantId.HasValue
                ? MultiTenancySides.Host
                : MultiTenancySides.Tenant;
        }
    }
}
