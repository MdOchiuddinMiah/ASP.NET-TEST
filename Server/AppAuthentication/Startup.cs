using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAuthentication.Interface;
using AppAuthentication.Repository;
using AppAuthenticationModel.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AppAuthentication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<AppSurveyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AppSurveyDatabase"), sqlServerOptions => sqlServerOptions.CommandTimeout(1000))); //, ServiceLifetime.Transient
            services.AddScoped<IBaseRepository, BaseRepository>(serviceProvider => new BaseRepository(serviceProvider.GetService<AppSurveyContext>(), Configuration));
            services.AddScoped<IAppSurveyRepository, AppSurveyRepository>(serviceProvider => new AppSurveyRepository(serviceProvider.GetService<AppSurveyContext>(), Configuration));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors(builder =>
            {
                var clients = Configuration.GetSection("Clients").Get<string[]>();
                builder.WithOrigins(clients).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
            });
            app.UseMvc();
        }
    }
}
