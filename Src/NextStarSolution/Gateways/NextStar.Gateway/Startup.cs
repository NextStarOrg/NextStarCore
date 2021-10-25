using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NextStar.Framework.AspNetCore.Extensions;
using NextStar.Framework.AspNetCore.Stores;
using NextStar.Gateway.Configs;
using Ocelot.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;

namespace NextStar.Gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static IServiceProvider MyServiceProvider { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSetting = Configuration.Get<AppSettingPartial>();
            
            services.AddNextStarApiAuthentication(appSetting);
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(appSetting.AllowedOrigins.ToArray())
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });

            });
            
            
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = appSetting.DataBaseSetting.Redis;
            });

            services.AddNextStarSession(appSetting);

            //网关组件服务
            services.AddOcelot();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            MyServiceProvider = app.ApplicationServices;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("NextStar Gateway!");
                });
            });

            var configuration = new OcelotPipelineConfiguration
            {
                AuthenticationMiddleware = NextStarAuthenticationMiddleware
            };

            //网关中间件
            app.UseOcelot(configuration).Wait();
        }
        
        /// <summary>
        /// 自定义的身份验证逻辑，判断当前sessionid在数据库中是否存在，不存在则返回401
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        private async Task NextStarAuthenticationMiddleware(HttpContext httpContext, Func<Task> next)
        {
            var downstreamRoute = httpContext.Items.DownstreamRoute();

            if (httpContext.Request.Method.ToUpper() != "OPTIONS" && IsAuthenticatedRoute(downstreamRoute))
            {
                Log.Information($"{httpContext.Request.Path} is an authenticated route. PreAuthenticationMiddleware checking if client is authenticated");

                var result = await httpContext.AuthenticateAsync(downstreamRoute.AuthenticationOptions.AuthenticationProviderKey);

                httpContext.User = result.Principal;

                if (httpContext.User.Identity.IsAuthenticated)
                {
                    //判断Session是否存在在数据库中 不存在则返回401
                    Guid? sessionId = httpContext.User.GetNextStarSessionId();
                    if (sessionId != null)
                    {
                        //判断SessionId是否存在
                        var sessionStore = MyServiceProvider.GetService<INextStarSessionStore>();
                        if (await sessionStore.IsExistsAsync(sessionId.Value))
                        {
                            Log.Information($"Client has been authenticated for {httpContext.Request.Path}");
                            //验证通过 执行下一步
                            await next.Invoke();
                            return;
                        }
                    }
                }

                // 认证失败返回401
                var error = new UnauthenticatedError(
                    $"Request for authenticated route {httpContext.Request.Path} by {httpContext.User.Identity.Name} was unauthenticated");

                Log.Warning($"Client has NOT been authenticated for {httpContext.Request.Path} and pipeline error set. {error}");

                httpContext.Items.SetError(error);
            }
            else
            {
                Log.Information($"No authentication needed for {httpContext.Request.Path}");

                await next.Invoke();
            }
        }

        private static bool IsAuthenticatedRoute(DownstreamRoute route)
        {
            return route.IsAuthenticated;
        }
    }
}