using Xunit;
using mode_api_canonical.Client;

namespace mode_canonical_api.Tests.IntegrationTests.Confederates.BattleLanguageCanonical
{
    public static class ExtensionMethods
    {
        public static void AssertEqual(this ModeDetailCanonicalItem actual, ModeDetailCanonicalUpsert expected) {
            Assert.Equal(expected.NameCanonical, actual.NameCanonical);
        }
    }
}
