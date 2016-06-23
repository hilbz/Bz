using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Bz.WebApi.Configuraion
{
    /// <summary>
    /// 用于配置Bz WebApi模块
    /// </summary>
    public interface IBzWebApiModuleConfiguration
    {
        /// <summary>
        /// Gets/sets <see cref="HttpConfiguration"/>.
        /// </summary>
        HttpConfiguration HttpConfiguration { get; set; }
    }
}
