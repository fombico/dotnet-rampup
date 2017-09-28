using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.Actuators;
using NotesApp.Models;
using NotesApp.Services;
using Steeltoe.CloudFoundry.Connector.MySql.EFCore;
using Steeltoe.Extensions.Configuration;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using Steeltoe.Management.Endpoint.Info;

namespace NotesApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            if (!env.IsEnvironment("Integration Test"))
            {
                Configuration = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .AddCloudFoundry()
                    .Build();
            }

            Environment = env;
        }

        public IConfiguration Configuration { get; set; }
        public IHostingEnvironment Environment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<NoteContext>(opt =>
            {
                if (Environment.IsEnvironment("Integration Test"))
                {
                    opt.UseInMemoryDatabase("notes");
                }
                else
                {
                    opt.UseMySql(Configuration);
                }
            });

            services.AddTransient(typeof(NoteService));

            services.AddOptions();

            services.Configure<CloudFoundryApplicationOptions>(Configuration);
            services.Configure<CloudFoundryServicesOptions>(Configuration);

            services.AddMvc();
            services.AddSingleton<IInfoContributor, CustomInfoContributor>();
            services.AddInfoActuator(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseInfoActuator();
        }
    }
}