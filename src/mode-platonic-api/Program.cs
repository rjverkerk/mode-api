using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using mode_platonic_api.Common;
using mode_platonic_api.data.Repositories.Confederates.BattleLanguage;
using mode_platonic_api.Services.Confederates.BattleLanguage;

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
                    .AddScoped<IContextDetailService, ContextDetailService>()
                    .AddScoped<IContextDetailRepository, ContextDetailRepository>()
                    .AddScoped<IRequestContext, MockRequestContext>()
        );
    }
}
