using Castle.DynamicProxy;

namespace Bz.Runtime.Validation.Interception
{
    /// <summary>
    /// 该拦截器将会触发在应用层方法调用时检查方法的参数合法性
    /// </summary>
    public class ValidationInterceptor : IInterceptor
    {
        /// <summary>
        /// 对一个方法进行拦截
        /// </summary>
        /// <param name="invocation">调用者</param>
        public void Intercept(IInvocation invocation)
        {
            new MethodInvocationValidator(
                invocation.Method,
                invocation.Arguments
            ).Validate();

            invocation.Proceed();
        }
    }
}
