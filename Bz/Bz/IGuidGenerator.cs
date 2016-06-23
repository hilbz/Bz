using System;

namespace Bz
{
    /// <summary>
    /// 用于生成Ids
    /// </summary>
    public interface IGuidGenerator
    {
        /// <summary>
        /// Creates a GUID.
        /// </summary>
        Guid Create();
    }
}
