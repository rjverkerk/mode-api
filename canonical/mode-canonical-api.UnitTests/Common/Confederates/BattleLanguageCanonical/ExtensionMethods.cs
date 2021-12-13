using mode_canonical_api.Contracts.Confederates.BattleLanguageCanonical.ModeDetailCanonical;
using mode_canonical_api.Domain.DomainModel.Confederates.BattleLanguageCanonical;
using System.Collections.Generic;
using Xunit;

namespace mode_canonical_api.UnitTests.Common.Confederates.BattleLanguageCanonical
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

        public static void AssertEqual(this ModeDetailCanonical actual, ModeDetailCanonical expected) {
            Assert.Equal(expected.NameCanonical, actual.NameCanonical);
            Assert.Equal(expected.ExternalId, actual.ExternalId);
            Assert.Equal(expected.CreatedBy, actual.CreatedBy);
            Assert.Equal(expected.CreatedDate, actual.CreatedDate);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.LastModifiedBy, actual.LastModifiedBy);
            Assert.Equal(expected.LastModifiedDate, actual.LastModifiedDate);
            Assert.Equal(expected.Version, actual.Version);
        }

        public static void AssertEqual(this IEnumerable<ModeDetailCanonical> actual, IEnumerable<ModeDetailCanonical> expected) {
            
        }
    }
}
