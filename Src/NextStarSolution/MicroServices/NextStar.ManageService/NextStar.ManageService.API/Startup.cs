using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NextStar.Framework.AspNetCore.Auditing;
using NextStar.Framework.AspNetCore.Extensions;
using NextStar.Framework.AspNetCore.Result;
using NextStar.ManageService.API.Configs;
using NextStar.ManageService.API.Extensions;

namespace NextStar.ManageService.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public ILifetimeScope AutofacContainer { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSetting = Configuration.Get<AppSettingPartial>();
            services.AddSingleton(Configuration);
            services.AddHttpContextAccessor();
            services.AddMemoryCache();
            //添加身份认证相关设定
            services.AddNextStarJwtAuthentication(appSetting);
            services.AddNextStarApiAuthorization();
            services.AddCustomRedisCache(appSetting);
            services.AddNextStarSession(appSetting);

            services.AddDatabase(appSetting);
            services.AddControllers(options =>
            {
                //追加Action审计日志
                options.Filters.Add<NextStarAuditActionFilter>();
            }).AddNewtonsoftJson(options =>
            {
                // options.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'z'";
                options.SerializerSettings.ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddCustomSwaggerGen();
        }
        
        public static void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac, like:
            builder.RegisterModule<AutofacServiceModules>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NextStar.ManageService.API v1"));
            }
            
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            
            if (env.IsProduction())
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}