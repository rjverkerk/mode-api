using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using mode_canonical_api.Common;
using mode_canonical_api.data.Repositories.Confederates.BattleLanguageCanonical;
using mode_canonical_api.Services.Confederates.BattleLanguageCanonical;

namespace mode_canonical_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureServices((_, services) =>
                services
                    .AddScoped<IModeDetailCanonicalService, ModeDetailCanonicalService>()
                    .AddScoped<IModeDetailCanonicalRepository, ModeDetailCanonicalRepository>()
                    .AddScoped<IRequestContext, MockRequestContext>()
                    .AddScoped<ITimeProvider, TimeProvider>()
        );
    }
}
