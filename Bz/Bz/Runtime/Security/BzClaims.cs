namespace Bz.Runtime.Security
{
    /// <summary>
    /// Used to get Bz-specific claim type names.
    /// </summary>
    public static class BzClaimTypes
    {
        /// <summary>
        /// TenantId.
        /// </summary>
        public const string TenantId = "http://www.Bz.com/identity/claims/tenantId";

        /// <summary>
        /// ImpersonatorUserId.
        /// </summary>
        public const string ImpersonatorUserId = "http://www.Bz.com/identity/claims/impersonatorUserId";

        /// <summary>
        /// ImpersonatorTenantId
        /// </summary>
        public const string ImpersonatorTenantId = "http://www.Bz.com/identity/claims/impersonatorTenantId";
    }
}

