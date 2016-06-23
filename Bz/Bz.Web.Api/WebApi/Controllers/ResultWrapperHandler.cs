using Bz.Dependency;
using Bz.Domain.Uow;
using Bz.Runtime.Session;
using Bz.Web.Models;
using Bz.WebApi.Configuraion;
using Castle.Core.Logging;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bz.WebApi.Controllers
{
    /// <summary>
    /// Wrapps Web API的包装类返回<see cref="AjaxResponse"/>
    /// </summary>
    public class ResultWrapperHandler : DelegatingHandler, ITransientDependency
    {
        private readonly IBzWebApiModuleConfiguration _webApiModuleConfiguration;

        public ResultWrapperHandler(IBzWebApiModuleConfiguration webApiModuleConfiguration)
        {
            _webApiModuleConfiguration = webApiModuleConfiguration;
        }

        protected virtual void WrapResultIfNeeded(HttpRequestMessage request, HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                return;
            }

            var wrapAttr = HttpActionDescriptorHelper.GetWrapResultAttributeOrNull(request.GetActionDescriptor())
                           ?? DontWrapResultAttribute.Default;

            if (!wrapAttr.WrapOnSuccess)
            {
                return;
            }

            object resultObject;
            if (!response.TryGetContentValue(out resultObject) || resultObject == null)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new ObjectContent<AjaxResponse>(
                    new AjaxResponse(),
                    _webApiModuleConfiguration.HttpConfiguration.Formatters.JsonFormatter
                    );
                return;
            }

            if (resultObject is AjaxResponse)
            {
                return;
            }

            response.Content = new ObjectContent<AjaxResponse>(
                new AjaxResponse(resultObject),
                _webApiModuleConfiguration.HttpConfiguration.Formatters.JsonFormatter
                );
        }
    }
}
