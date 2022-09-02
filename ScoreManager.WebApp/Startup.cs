using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using ScoreManager.ServiceImpl;
using ScoreManager.ServiceInterface;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace ScoreManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.Encoder= JavaScriptEncoder.Create(UnicodeRanges.All);
                option.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            services.AddCustomService();
            services.AddScoped<ISqlSugarClient>(option =>
            {
                SqlSugarClient client = new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = Configuration.GetSection("sqlconnetction").Value,
                    DbType = DbType.Oracle,
                    InitKeyType = InitKeyType.Attribute,
                    IsAutoCloseConnection = true,
                });

                client.Aop.OnLogExecuting = (sql, par) =>
                {
                    Console.WriteLine($"Sql���{sql}");
                    Debug.WriteLine($"Sql���{sql}");
                    UtilMethods.GetSqlString(DbType.Oracle, sql, par);
                };

                return client;

            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
            {
                option.LoginPath = "/Account/Login";
                option.AccessDeniedPath = "/Home/Error";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandle();
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot"))
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
