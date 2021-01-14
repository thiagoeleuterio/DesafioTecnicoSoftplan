using Calculo.Api.App;
using Calculo.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Calculo.Api.Middlewares
{
    /// <summary>
    /// 
    /// </summary>
    public class DomainNotificationHandlerFilter : IAsyncResultFilter
    {
        private readonly DomainNotificationHandler _domainNotification;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notifications"></param>
        public DomainNotificationHandlerFilter(INotificationHandler<DomainNotification> notifications)
        {
            _domainNotification = (DomainNotificationHandler)notifications;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (_domainNotification.HasNotifications)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.HttpContext.Response.ContentType = "application/json";

                var erros = new ResultMessageResponse<string>(null, _domainNotification.GetNotifications().Select(GetMessageNotifications).ToArray());

                await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(erros));

                return;
            }

            await next();
        }

        private string GetMessageNotifications(DomainNotification notification)
        {
            return notification.Description;
        }
    }
}