using mode_canonical_api.Domain.DomainModel.Confederates.BattleLanguageCanonical;
using mode_canonical_api.UnitTests.Common.Confederates.BattleLanguageCanonical;
using System;
using Xunit;
using AutoFixture.Xunit2;

namespace mode_canonical_api.UnitTests.DomainModel.Confederates.BattleLanguage
{
    public class ModeDetailCanonicalTests
    {
        [Theory, AutoData]
        public void Constructor_Initializes_ModeDetailCanonical(
            ModeDetailCanonicalDto dto, DateTime createdDate) {
            
            var sut = new ModeDetailCanonical(dto, createdDate);

            sut.AssertEqual(dto);
            Assert.Equal(dto.ActorId, sut.CreatedBy);
            Assert.Equal(createdDate, sut.CreatedDate);
            Assert.Null(sut.LastModifiedBy);
            Assert.Null(sut.LastModifiedDate);
            Assert.Equal(1, sut.Version);
        }

        [Theory, AutoData]
        public void Update_Updates_ModeDetailCanonical(
            string nameCanonical, 
            DateTime modifiedDate,
            int modifiedBy,
            ModeDetailCanonical sut) {

            var previousVersion = sut.Version;
            var dto = new ModeDetailCanonicalDto(sut.ExternalId, nameCanonical, modifiedBy);

            sut.Update(dto, modifiedDate);

            sut.AssertEqual(dto);
            Assert.Equal(dto.ActorId, sut.LastModifiedBy);
            Assert.Equal(modifiedDate, sut.LastModifiedDate);
            Assert.Equal(previousVersion + 1, sut.Version);
        }
    }
}
