using mode_api.Contracts.Confederates.BattleLanguage.ModeDetail;
using mode_api.Domain.DomainModel.Confederates.BattleLanguage;
using Xunit;

namespace mode_api.UnitTests.Common.Confederates.BattleLanguage
{
    public static class ExtensionMethods
    {
        public static void Equals(this ModeDetail actual, ModeDetailUpsert expected) {
            Assert.Equal(expected.Name, actual.Name);
        }

        public static void Equals(this ModeDetail actual, ModeDetailItem expected) {
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.CreatedBy, actual.CreatedBy);
            Assert.Equal(expected.CreatedDate, actual.CreatedDate);
            Assert.Equal(expected.Id, actual.ExternalId);
            Assert.Equal(expected.LastModifiedBy, actual.LastModifiedBy);
            Assert.Equal(expected.LastModifiedDate, actual.LastModifiedDate);
        }
    }
}
