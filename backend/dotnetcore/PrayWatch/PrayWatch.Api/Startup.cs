using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PrayWatch.Domain.Interfaces;
using PrayWatch.Domain.Services;
using PrayWatch.Infra.Data.Contexts;

namespace PrayWatch.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(x => x.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "PrayWatch.Api",
                Version = "v1"
            }));

            // Services
            services.AddScoped<IPrayersService, PrayersService>();
            services.AddScoped<IPurposesService, PurposesService>();

            // Repositories
            services.AddDbContext<PrayWatchDbContext>(options => options.UseInMemoryDatabase(databaseName: "PrayWatch"));
            services.AddScoped<IPurposesRepository, Infra.Data.Repositories.InMemory.PurposesRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.RoutePrefix = string.Empty;
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "PrayWatch.Api");
            });
            app.UseMvc();
        }
    }
}
