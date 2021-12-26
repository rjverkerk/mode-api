using mode_canonical_api.Contracts.Confederates.BattleLanguageCanonical.ModeDetailCanonical;
using mode_canonical_api.Domain.DomainModel.Confederates.BattleLanguageCanonical;
using Xunit;

namespace mode_canonical_api.Tests.Common.Confederates.BattleLanguageCanonical
{
    public static class ExtensionMethods
    {
        public static void AssertEqual(this ModeDetailCanonical actual, ModeDetailCanonicalUpsert expected) {
            Assert.Equal(expected.NameCanonical, actual.NameCanonical);
        }

        public static void AssertEqual(this ModeDetailCanonical actual, ModeDetailCanonicalItem expected) {
            Assert.Equal(expected.NameCanonical, actual.NameCanonical);
            Assert.Equal(expected.CreatedBy, actual.CreatedBy);
            Assert.Equal(expected.CreatedDate, actual.CreatedDate);
            Assert.Equal(expected.Id, actual.ExternalId);
            Assert.Equal(expected.LastModifiedBy, actual.LastModifiedBy);
            Assert.Equal(expected.LastModifiedDate, actual.LastModifiedDate);
        }

        public static void AssertEqual(this ModeDetailCanonical actual, ModeDetailCanonicalDto expected) {
            Assert.Equal(expected.NameCanonical, actual.NameCanonical);
            Assert.Equal(expected.ExternalId, actual.ExternalId);
        }

        public static void AssertEqual(this ModeDetailCanonicalItem actual, ModeDetailCanonicalUpsert expected) {
            Assert.Equal(expected.NameCanonical, actual.NameCanonical);
        }
    }
}
