using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using mode_platonic_api.Common;
using mode_platonic_api.data.Repositories.Confederates.BattleLanguagePlatonic;
using mode_platonic_api.Services.Confederates.BattleLanguagePlatonic;

namespace mode_platonic_api
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
                    .AddScoped<IModeDetailPlatonicService, ModeDetailPlatonicService>()
                    .AddScoped<IModeDetailPlatonicRepository, ModeDetailPlatonicRepository>()
                    .AddScoped<IRequestContext, MockRequestContext>()
                    .AddScoped<ITimeProvider, TimeProvider>()
        );
    }
}
