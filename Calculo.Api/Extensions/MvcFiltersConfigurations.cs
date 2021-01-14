using Calculo.Api.Middlewares;
using Microsoft.AspNetCore.Mvc;

namespace Calculo.Api.Api.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class MvcFiltersConfigurations
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static MvcOptions ConfigureFilters(this MvcOptions options)
        {
            options.Filters.Add<DomainNotificationHandlerFilter>();

            return options;
        }
    }
}