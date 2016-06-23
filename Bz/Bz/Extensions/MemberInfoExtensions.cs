namespace System.Reflection
{
    /// <summary>
    /// <see cref="MemberInfo"/>
    /// </summary>
    public static class MemberInfoExtensions
    {

        /// <summary>
        /// 获取一个成员的单个特性
        /// </summary>
        /// <typeparam name="T">特性的Type</typeparam>
        /// <param name="memberInfo">成员</param>
        /// <param name="inherit">是否包含继承的属性</param>
        /// <returns>找到则返回，没找到则返回null</returns>
        public static T GetSingleAttributeOrNull<T>(this MemberInfo memberInfo, bool inherit = true) where T : class
        {
            if (memberInfo==null)
            {
                throw new ArgumentException("memberInfo");
            }

            var attrs = memberInfo.GetCustomAttributes(typeof(T), inherit);

            if (attrs.Length>0)
            {
                return (T)attrs[0];
            }

            return default(T);
        }
    }
}
