using mode_api.Domain.DomainModel.Confederates.BattleLanguage;
using mode_api.UnitTests.Common.Confederates.BattleLanguage;
using System;
using Xunit;
using AutoFixture.Xunit2;

namespace mode_api.UnitTests.DomainModel.Confederates.BattleLanguage
{
    public class ModeDetailTests
    {
        [Theory, AutoData]
        public void Constructor_Initializes_ModeDetail(
            ModeDetailDto dto, DateTime createdDate) {
            
            var sut = new ModeDetail(dto, createdDate);

            sut.AssertEqual(dto);
            Assert.Equal(dto.ActorId, sut.CreatedBy);
            Assert.Equal(createdDate, sut.CreatedDate);
            Assert.Null(sut.LastModifiedBy);
            Assert.Null(sut.LastModifiedDate);
            Assert.Equal(1, sut.Version);
        }

        [Theory, AutoData]
        public void Update_Updates_ModeDetail(
            string name, 
            DateTime modifiedDate,
            int modifiedBy,
            ModeDetail sut) {

            var previousVersion = sut.Version;
            var dto = new ModeDetailDto(sut.ExternalId, name, modifiedBy);

            sut.Update(dto, modifiedDate);

            sut.AssertEqual(dto);
            Assert.Equal(dto.ActorId, sut.LastModifiedBy);
            Assert.Equal(modifiedDate, sut.LastModifiedDate);
            Assert.Equal(previousVersion + 1, sut.Version);
        }
    }
}
