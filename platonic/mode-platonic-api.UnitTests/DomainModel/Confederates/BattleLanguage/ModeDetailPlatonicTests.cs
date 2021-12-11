using mode_platonic_api.Domain.DomainModel.Confederates.BattleLanguagePlatonic;
using mode_platonic_api.UnitTests.Common.Confederates.BattleLanguagePlatonic;
using System;
using Xunit;
using AutoFixture.Xunit2;

namespace mode_platonic_api.UnitTests.DomainModel.Confederates.BattleLanguage
{
    public class ModeDetailPlatonicTests
    {
        [Theory, AutoData]
        public void Constructor_Initializes_ModeDetailPlatonic(
            ModeDetailPlatonicDto dto, DateTime createdDate) {
            
            var sut = new ModeDetailPlatonic(dto, createdDate);

            sut.AssertEqual(dto);
            Assert.Equal(dto.ActorId, sut.CreatedBy);
            Assert.Equal(createdDate, sut.CreatedDate);
            Assert.Null(sut.LastModifiedBy);
            Assert.Null(sut.LastModifiedDate);
            Assert.Equal(1, sut.Version);
        }

        [Theory, AutoData]
        public void Update_Updates_ModeDetailPlatonic(
            string namePlatonic, 
            DateTime modifiedDate,
            int modifiedBy,
            ModeDetailPlatonic sut) {

            var previousVersion = sut.Version;
            var dto = new ModeDetailPlatonicDto(sut.ExternalId, namePlatonic, modifiedBy);

            sut.Update(dto, modifiedDate);

            sut.AssertEqual(dto);
            Assert.Equal(dto.ActorId, sut.LastModifiedBy);
            Assert.Equal(modifiedDate, sut.LastModifiedDate);
            Assert.Equal(previousVersion + 1, sut.Version);
        }
    }
}
