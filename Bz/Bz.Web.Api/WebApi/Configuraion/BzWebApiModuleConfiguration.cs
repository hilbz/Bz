using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Bz.WebApi.Configuraion
{
    public class BzWebApiModuleConfiguration:IBzWebApiModuleConfiguration
    {
        public HttpConfiguration HttpConfiguration { get; set; }

        public BzWebApiModuleConfiguration()
        {
            HttpConfiguration = GlobalConfiguration.Configuration;
        }
    }
}
