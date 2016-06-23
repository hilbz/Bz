using Bz.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Runtime.Validation.Interception
{
    /// <summary>
    /// 该类用来验证方法调用的参数
    /// </summary>
    internal class MethodInvocationValidator
    {
        private readonly MethodInfo _method;
        private readonly object[] _parameterValues;
        private readonly ParameterInfo[] _parameters;
        private readonly List<ValidationResult> _validationErrors;

        public MethodInvocationValidator(MethodInfo method, object[] parameterValues)
        {
            _method = method;
            _parameterValues = parameterValues;
            _parameters = method.GetParameters();
            _validationErrors = new List<ValidationResult>();
        }

        public void Validate()
        {
            if (!_method.IsPublic)
            {
                //验证的请求只有在公用的方法上
                return;
            }

            if (_method.IsDefined(typeof(DisableValidationAttribute)))
            {
                //对于特殊的请求不进行验证
                return;
            }

            if (_parameters.IsNullOrEmpty())
            {
                //没有参数不需要验证
                return;
            }

            if (_parameters.Length!=_parameters.Length)
            {
                //实际上这是不可能的
                throw new Exception("需要验证的方法的参数不一致");
            }

            for (int i = 0; i < _parameters.Length; i++)
            {
                ValidateMethodParameter(_parameters[1], _parameterValues[i]);
            }
            if (_validationErrors.Any())
            {
                throw new BzValidationException("参数验证不通过：", _validationErrors);
            }

            foreach (var parameterValue in _parameterValues)
            {
                NormalizeParameter(parameterValue);
            }                    
        }

        /// <summary>
        /// 验证方法的参数
        /// </summary>
        /// <param name="parameterInfo"></param>
        /// <param name="parameterValue"></param>
        private void ValidateMethodParameter(ParameterInfo parameterInfo,object parameterValue)
        {
            if (parameterInfo==null)
            {
                if (!parameterInfo.IsOptional&&!parameterInfo.IsOut&&!TypeHelper.IsPrimitiveExtendedIncludingNullable(parameterInfo.ParameterType))
                {
                    _validationErrors.Add(new ValidationResult("参数："+parameterInfo.Name+"为空！",new[] {parameterInfo.Name }));
                }
                return;
            }
            ValidateObjectRecursively(parameterValue);
        }

        private void ValidateObjectRecursively(object validatingObject)
        {
            if (validatingObject is IEnumerable &&!(validatingObject is IQueryable))
            {
                foreach (var item in (validatingObject as IEnumerable))
                {
                    ValidateObjectRecursively(item);
                }
            }
            if (!(validatingObject is IValidate))
            {
                return;
            }

            SetValidationAttributeErrors(validatingObject);
            if (validatingObject is ICustomValidate)
            {
                (validatingObject as ICustomValidate).AddValidationErrors(_validationErrors);
            }

            var properties = TypeDescriptor.GetProperties(validatingObject).Cast<PropertyDescriptor>();
            foreach (var property in properties)
            {
                ValidateObjectRecursively(property.GetValue(validatingObject));
            }
        }

        /// <summary>
        /// 检查所有的DataAnnotations属性
        /// </summary>
        private void SetValidationAttributeErrors(object validatingObject)
        {
            var properties = TypeDescriptor.GetProperties(validatingObject).Cast<PropertyDescriptor>();

            foreach (var property in properties)
            {
                var validationAttributes = property.Attributes.OfType<ValidationAttribute>().ToArray();
                if (validationAttributes.IsNullOrEmpty())
                {
                    continue;
                }

                var validationContext = new ValidationContext(validatingObject)
                {
                    DisplayName = property.Name,
                    MemberName = property.Name
                };

                foreach (var attribute in validationAttributes)
                {
                    var result = attribute.GetValidationResult(property.GetValue(validatingObject), validationContext);
                    if (result!=null)
                    {
                        _validationErrors.Add(result);
                        return;
                    }
                }
            }
        }

        private static void NormalizeParameter(object parameterValue)
        {
            if (parameterValue is IShouldNormalize)
            {
                (parameterValue as IShouldNormalize).Normalize();
            }
        }
    }
}
