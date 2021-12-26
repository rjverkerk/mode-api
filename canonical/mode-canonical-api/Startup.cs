using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using mode_canonical_api.Common;
using mode_canonical_api.data.Repositories.Confederates.BattleLanguageCanonical;
using mode_canonical_api.Domain;
using mode_canonical_api.Services.Confederates.BattleLanguageCanonical;
using System.Linq;

namespace mode_canonical_api
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

            services.AddControllers()
                .PartManager
                .ApplicationParts
                .Add(new AssemblyPart(typeof(Startup).Assembly)); 

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "mode_canonical_api", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IModeDetailCanonicalService, ModeDetailCanonicalService>()
                    .AddScoped<IModeDetailCanonicalRepository, ModeDetailCanonicalRepository>()
                    .AddScoped<IRequestContext, MockRequestContext>()
                    .AddScoped<ITimeProvider, TimeProvider>();

            ConfigureExternalServices(services);
        }

        protected virtual void ConfigureExternalServices(IServiceCollection services) {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "mode_canonical_api v1"));
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