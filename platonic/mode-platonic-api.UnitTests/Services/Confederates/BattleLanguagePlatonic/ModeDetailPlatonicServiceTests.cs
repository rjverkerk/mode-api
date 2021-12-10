using AutoFixture.Xunit2;
using AutoMapper;
using mode_platonic_api.Common;
using mode_platonic_api.Contracts.Confederates.BattleLanguagePlatonic.ModeDetailPlatonic;
using mode_platonic_api.data.Repositories.Confederates.BattleLanguagePlatonic;
using mode_platonic_api.Domain.DomainModel.Confederates.BattleLanguagePlatonic;
using mode_platonic_api.Services.Confederates.BattleLanguagePlatonic;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace mode_platonic_api.UnitTests.Services.Confederates.BattleLanguagePlatonic
{
    public class ModeDetailPlatonicServiceTests
    {
        private Mock<IMapper> _mockMapper;
        private Mock<IModeDetailPlatonicRepository> _mockModeDetailPlatonicRepository;
        private Mock<IRequestContext> _mockContext;
        private Mock<ITimeProvider> _mockTimeProvider;
        private ModeDetailPlatonicService _sut;

        public ModeDetailPlatonicServiceTests() {
            _mockMapper = new Mock<IMapper>();
            _mockModeDetailPlatonicRepository = new Mock<IModeDetailPlatonicRepository>();
            _mockContext = new Mock<IRequestContext>();
            _mockTimeProvider = new Mock<ITimeProvider>();
            _sut = new ModeDetailPlatonicService(
                _mockContext.Object, 
                _mockMapper.Object, 
                _mockModeDetailPlatonicRepository.Object,
                _mockTimeProvider.Object);
        }

        [Theory, AutoData]
        public async void SearchByCriteria_Returns_ModeDetailPlatonicResponse(
            List<ModeDetailPlatonic> modeDetailPlatonics, 
            List<ModeDetailPlatonicItem> expected) {

            _mockModeDetailPlatonicRepository.Setup(x => x.GetAll())
                .ReturnsAsync(modeDetailPlatonics);

            _mockMapper.Setup(x => x.Map<IEnumerable<ModeDetailPlatonicItem>>(modeDetailPlatonics))
                .Returns(expected);

            var result = await _sut.SearchByCriteria();

            Assert.Equal(result.ModeDetailPlatonics, expected);
        }

        [Theory, AutoData]
        public async void GetByExternalId_Throws_Exception_If_ResourceNotFound(
            ModeDetailPlatonic modeDetailPlatonic) {

            _mockModeDetailPlatonicRepository
                .Setup(x => x.GetByExternalIds(new[] { modeDetailPlatonic.ExternalId }))
                .ReturnsAsync(Array.Empty<ModeDetailPlatonic>());

            var result = await _sut.GetByExternalId(modeDetailPlatonic.ExternalId);
            
            Assert.Null(result);        
        }

        [Theory, AutoData]
        public async void GetByExternalId_Returns_ModeDetailPlatonicItem(
            ModeDetailPlatonic modeDetailPlatonic,
            ModeDetailPlatonicItem expected) {

            _mockModeDetailPlatonicRepository
                .Setup(x => x.GetByExternalIds(new[] { modeDetailPlatonic.ExternalId }))
                .ReturnsAsync(new[] { modeDetailPlatonic });

            _mockMapper.Setup(x => x.Map<ModeDetailPlatonicItem>(modeDetailPlatonic))
                .Returns(expected);

            var result = await _sut.GetByExternalId(modeDetailPlatonic.ExternalId);

            Assert.Equal(result, expected);
        }

        [Theory, AutoData]
        public async void Delete_Removes_ModeDetailPlatonicItems(
            ModeDetailPlatonic modeDetailPlatonic) {

            var itemsToDelete = new[] { modeDetailPlatonic };

            _mockModeDetailPlatonicRepository
                .Setup(x => x.GetByExternalIds(new[] { modeDetailPlatonic.ExternalId }))
                .ReturnsAsync(itemsToDelete);

            var result = await _sut.Delete(modeDetailPlatonic.ExternalId);

            _mockModeDetailPlatonicRepository.Verify(x => x.RemoveRange(itemsToDelete), Times.Once);

            _mockModeDetailPlatonicRepository.Verify(x => x.SaveAsync(), Times.Once);

            Assert.True(result);
        }

        [Theory, AutoData]
        public async void Delete_DoesNothing_If_ResourceNotFound(
            ModeDetailPlatonic modeDetailPlatonic) {

            _mockModeDetailPlatonicRepository
                .Setup(x => x.GetByExternalIds(new[] { modeDetailPlatonic.ExternalId }))
                .ReturnsAsync(Array.Empty<ModeDetailPlatonic>());

            var result = await _sut.Delete(modeDetailPlatonic.ExternalId);

            _mockModeDetailPlatonicRepository
                .Verify(x => x.RemoveRange(It.IsAny<IEnumerable<ModeDetailPlatonic>>()), Times.Never);

            _mockModeDetailPlatonicRepository.Verify(x => x.SaveAsync(), Times.Never);

            Assert.False(result);
        }

        [Theory, AutoData]
        public async void Update_Updates_ModeDetailPlatonic(
            ModeDetailPlatonicUpsert upsertRequest,
            ModeDetailPlatonic modeDetailPlatonic,
            Guid externalId) {

            _mockModeDetailPlatonicRepository
                .Setup(x => x.GetByExternalIds(new[] { externalId }))
                .ReturnsAsync(new[] { modeDetailPlatonic });

            ModeDetailPlatonic result = null;
            _mockMapper.Setup(x => x.Map<ModeDetailPlatonic, ModeDetailPlatonicItem>(It.IsAny<ModeDetailPlatonic>()))
                .Callback<ModeDetailPlatonic>(x => result = x);

            await _sut.Update(upsertRequest, externalId);

            _mockModeDetailPlatonicRepository.Verify(x => x.SaveAsync(), Times.Once);

            result.Equals(upsertRequest);
            Assert.Equal(result.LastModifiedBy, _mockContext.Object.UserId);
            Assert.Equal(result.LastModifiedDate, _mockTimeProvider.Object.UTCNow());
        }

        [Theory, AutoData]
        public async void Update_ReturnsNull_If_ModeDetailPlatonic_NotFound(
            ModeDetailPlatonicUpsert upsertRequest,
            Guid externalId) {

            _mockModeDetailPlatonicRepository
                .Setup(x => x.GetByExternalIds(new[] { externalId }))
                .ReturnsAsync(Array.Empty<ModeDetailPlatonic>());

            var result = await _sut.Update(upsertRequest, externalId);

            _mockModeDetailPlatonicRepository.Verify(x => x.SaveAsync(), Times.Never);

            Assert.Null(result);
        }

        [Theory, AutoData]
        public async void Create_Creates_ModeDetailPlatonic(
            ModeDetailPlatonicUpsert upsertRequest) {

            ModeDetailPlatonic created = null;
            _mockModeDetailPlatonicRepository
                .Setup(x => x.Add(It.IsAny<ModeDetailPlatonic>()))
                .Callback<ModeDetailPlatonic>(x => created = x);

            var result = await _sut.Create(upsertRequest);

            _mockModeDetailPlatonicRepository.Verify(x => x.SaveAsync(), Times.Once);

            created.Equals(upsertRequest);
            created.Equals(result);
            Assert.Null(created.LastModifiedBy);
            Assert.Null(created.LastModifiedDate);
            Assert.Equal(created.CreatedBy, _mockContext.Object.UserId);
            Assert.Equal(created.CreatedDate, _mockTimeProvider.Object.UTCNow());
            Assert.Equal(1, created.Version);
        }
    }
}
