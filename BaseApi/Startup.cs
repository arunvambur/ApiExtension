using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PluginFramework;
using System.Reflection;

namespace BaseApi
{
    public class Startup
    {
        List<IRegisterService> _registerServices = new List<IRegisterService>();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugin");
            if(Directory.Exists(path))
            {
                foreach(var name in Directory.GetFiles(path, "*.dll"))
                {
                    var assembly = Assembly.LoadFrom(name);
                    var type = assembly.GetTypes().Where(t => typeof(IRegisterService).IsAssignableFrom(t)).FirstOrDefault();
                    if(type != null)
                    {
                        IRegisterService service = Activator.CreateInstance(type) as IRegisterService;
                        if (service != null) _registerServices.Add(service);

                        services.AddMvc().AddApplicationPart(assembly).AddControllersAsServices();
                    }
                }
            }

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BaseApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BaseApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
