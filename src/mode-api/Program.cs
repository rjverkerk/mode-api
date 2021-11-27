using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using mode_api.Common;
using mode_api.data.Repositories.Confederates.BattleLanguage;
using mode_api.Services.Confederates.BattleLanguage;

namespace mode_api
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
                    .AddScoped<IModeDetailService, ModeDetailService>()
                    .AddScoped<IModeDetailRepository, ModeDetailRepository>()
                    .AddScoped<IRequestContext, MockRequestContext>()
        );
    }
}
