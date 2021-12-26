using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace mode_api.Tests.IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<TestStartup>
    {
        protected override IHostBuilder CreateHostBuilder() {
            var builder = Host.CreateDefaultBuilder()
                              .ConfigureWebHostDefaults(x => {
                                  x.UseStartup<TestStartup>()
                                  .UseTestServer();
                              });
            return builder;
        }
    }
}
