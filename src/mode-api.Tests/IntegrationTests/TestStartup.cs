using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using mode_api.Domain;

namespace mode_api.Tests.IntegrationTests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration): base(configuration) {}

        protected override void ConfigureExternalServices(IServiceCollection services) {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseInMemoryDatabase("TestDatabase"));
        }
    }
}
