using Xunit;
using mode_api_canonical.Client;

namespace mode_api.Tests.IntegrationTests.Confederates.BattleLanguage
{
    public static class ExtensionMethods
    {
        public static void AssertEqual(this ModeDetailItem actual, ModeDetailUpsert expected) {
            Assert.Equal(expected.Name, actual.Name);
        }
    }
}
