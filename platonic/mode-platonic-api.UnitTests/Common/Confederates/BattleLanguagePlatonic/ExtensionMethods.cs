using mode_platonic_api.Contracts.Confederates.BattleLanguagePlatonic.ModeDetailPlatonic;
using mode_platonic_api.Domain.DomainModel.Confederates.BattleLanguagePlatonic;
using Xunit;

namespace mode_platonic_api.UnitTests.Common.Confederates.BattleLanguagePlatonic
{
    public static class ExtensionMethods
    {
        public static void AssertEqual(this ModeDetailPlatonic actual, ModeDetailPlatonicUpsert expected) {
            Assert.Equal(expected.NamePlatonic, actual.NamePlatonic);
        }

        public static void AssertEqual(this ModeDetailPlatonic actual, ModeDetailPlatonicItem expected) {
            Assert.Equal(expected.NamePlatonic, actual.NamePlatonic);
            Assert.Equal(expected.CreatedBy, actual.CreatedBy);
            Assert.Equal(expected.CreatedDate, actual.CreatedDate);
            Assert.Equal(expected.Id, actual.ExternalId);
            Assert.Equal(expected.LastModifiedBy, actual.LastModifiedBy);
            Assert.Equal(expected.LastModifiedDate, actual.LastModifiedDate);
        }

        public static void AssertEqual(this ModeDetailPlatonic actual, ModeDetailPlatonicDto expected) {
            Assert.Equal(expected.NamePlatonic, actual.NamePlatonic);
            Assert.Equal(expected.ExternalId, actual.ExternalId);
        }
    }
}
