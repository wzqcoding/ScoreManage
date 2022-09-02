using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ScoreManager.WebApp.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ScoreManager.WebApp.Extensions
{
    public class ExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public ExceptionHandleMiddleware(RequestDelegate next, ILogger<ExceptionHandleMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                 HandleExceptionAsync(httpContext, ex);
            }
        }
        private void HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var error = exception.ToString();
            _logger.LogError(error);
            context.Response.Redirect("/Home/Error");
            //return context.Response.WriteAsync(JsonConvert.SerializeObject(ApiResult.Error(error), new JsonSerializerSettings
            //{
            //    ContractResolver = new CamelCasePropertyNamesContractResolver()
            //}));
        }
    }
}
