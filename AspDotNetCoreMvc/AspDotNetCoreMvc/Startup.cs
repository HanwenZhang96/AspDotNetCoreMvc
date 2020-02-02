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
            // AddMvc()：注册MVC所需要的一些相关的服务到IoC容器里
            services.AddMvc();

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
                // 该中间件最好只在开发时使用，否则容易引起安全问题
                app.UseDeveloperExceptionPage();
            }

            // 把返回的http错误直接显示在页面上
            app.UseStatusCodePages();

            // 跳转至自定义的错误页面
            // app.UseStatusCodePagesWithRedirects("此处为页面地址");

            // 把wwwroot中的文件serve出来，不添加这句话，就无法访问这些文件
            app.UseStaticFiles();

            // 对MVC中间件最简单的使用和配置，并配置路由
            app.UseMvc( routes =>
            {
                routes.MapRoute(
                    "default", 
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
