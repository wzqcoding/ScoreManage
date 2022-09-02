using Microsoft.AspNetCore.Builder;
using ScoreManager.WebApp.Extensions;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseExceptionHandle(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandleMiddleware>();//UseMiddleware添加中间件
            return app;
        }
    }
}
