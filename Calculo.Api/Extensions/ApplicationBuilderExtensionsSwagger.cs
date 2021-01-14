using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Calculo.Api.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApplicationBuilderExtensionsSwagger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public static void UseSwaggerInApplication(this IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio técnico");
            });
        }
    }
}
