namespace Bz.Runtime.Validation
{
    /// <summary>
    /// 在方法执行之前进行自定义验证
    /// </summary>
    public interface IShouldNormalize
    {
        /// <summary>
        /// 会在方法执行之前和基本验证（Validation）之后
        /// </summary>
        void Normalize();
    }
}
