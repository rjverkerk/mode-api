using mode_api_canonical.Client;
using System.Net.Http;
using Xunit;

namespace mode_canonical_api.Tests.IntegrationTests
{
    public class BaseIntegrationTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public BaseIntegrationTest(CustomWebApplicationFactory factory) {
            _factory = factory;
        }

        protected HttpClient CreateHttpClient() {
            return _factory.CreateClient();
        }

        protected ModeDetailCanonicalClient GetModeDetailCanonicalClient(HttpClient httpClient) {
            return new ModeDetailCanonicalClient(httpClient.BaseAddress.ToString(), httpClient);
        }
    }
}
