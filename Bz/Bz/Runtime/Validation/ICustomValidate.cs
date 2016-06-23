using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Runtime.Validation
{

    /// <summary>
    /// 实现此接口的类必须有自定义验证规则
    /// So,实现类可以有自己的验证逻辑
    /// </summary>
    public interface ICustomValidate:IValidate
    {
        /// <summary>
        /// 该方法用来验证一个实体
        /// </summary>
        /// <param name="results">验证错误列表，添加到错误列表里面</param>
        void AddValidationErrors(List<ValidationResult> results);
    }
}
