using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AOPForYouAndMe.Models;
using AOPForYouAndMe.Models.AOP;
using AOPForYouAndMe.Models.Decorator;
using Castle.DynamicProxy;
using Couchbase.Extensions.Caching;
using Couchbase.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AOPForYouAndMe
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
            services.AddMvc();

            services.AddCouchbase(opt =>
            {
                opt.Servers = new List<Uri>
                {
                    new Uri("http://localhost:8091")
                };
                opt.Username = "matt";
                opt.Password = "password";
            });

            services.AddDistributedCouchbaseCache("mycache", opt => { });

            services.AddTransient<ISlowLegacyService, SlowLegacyService>();

            // 4 - decorator pattern
            //services.Decorate<ISlowLegacyService, CachingDecorator>();

            // 5 - castle dynamic proxy
//            services.AddTransient<CacheInterceptor>();
//            var sp = services.BuildServiceProvider();                   // not sure about
//            var cacheInterceptor = sp.GetService<CacheInterceptor>();   // not sure about
//            var proxyGenerator = new ProxyGenerator();
//            services.Decorate<ISlowLegacyService>(x =>
//                proxyGenerator.CreateInterfaceProxyWithTargetInterface(x, cacheInterceptor));

            // 6 - postsharp
            var sp = services.BuildServiceProvider();                   // not sure about
            CacheAspect.Cache = sp.GetService<IDistributedCache>();     // definitely not sure about
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
