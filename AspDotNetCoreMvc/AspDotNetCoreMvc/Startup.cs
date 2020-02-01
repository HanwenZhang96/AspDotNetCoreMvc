using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspDotNetCoreMvc.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspDotNetCoreMvc
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // 生命周期为 Singleton
            // 每当有其他的类来请求 ICinemaService 的时候，容器都会返回 CinemaMemoryService 的实例
            // 在 Controller 的构造函数和方法中都可以注入，注入的为接口
            services.AddSingleton<ICinemaService, CinemaMemoryService>();
            services.AddSingleton<IMovieService, MovieMemoryService>();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context,next) =>
            {
                logger.LogInformation("M1 Start");
                await context.Response.WriteAsync("Hello World!");
                await next();
                logger.LogInformation("M1 End");
            });
            app.Run(async (context) =>
            {
                logger.LogInformation("M2 Start");
                await context.Response.WriteAsync("Another Hello!");
                logger.LogInformation("M2 End");

            });
        }
    }
}
