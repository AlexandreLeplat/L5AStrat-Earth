using HostApp.Security;
using Entities.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace HostApp
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
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });
            services.AddTokenAuthentication(Configuration);
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "Front/dist";
            });
            services.AddDbContext<DAL>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("MainDatabase"), b => b.MigrationsAssembly("HostApp"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DAL dal)
        {
            try
            {
                Log.Information("Startup: Configure called");

                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                dal.Database.Migrate();
                Log.Information("Startup: Database migrated");

                app.UseHttpsRedirection();
                app.UseSpaStaticFiles();
                app.UseRouting();
                app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials());
                app.UseAuthentication();
                app.UseAuthorization();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                }).UseSpa(spa =>
                {
                    spa.Options.SourcePath = "Front";
                    if (env.IsDevelopment())
                    {
                        spa.UseAngularCliServer(npmScript: "start");
                    }
                });
                Log.Information("Startup: Configure ended");
            }
            catch(Exception e)
            {
                Log.Error(e.ToString());
            }
        }
    }
}
