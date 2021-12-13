using AutoFixture.Xunit2;
using AutoMapper;
using mode_canonical_api.Common;
using mode_canonical_api.Contracts.Confederates.BattleLanguageCanonical.ModeDetailCanonical;
using mode_canonical_api.data.Repositories.Confederates.BattleLanguageCanonical;
using mode_canonical_api.Domain.DomainModel.Confederates.BattleLanguageCanonical;
using mode_canonical_api.Services.Confederates.BattleLanguageCanonical;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace mode_canonical_api.UnitTests.Services.Confederates.BattleLanguageCanonical
{
    public class ModeDetailCanonicalServiceTests
    {
        private Mock<IMapper> _mockMapper;
        private Mock<IModeDetailCanonicalRepository> _mockModeDetailCanonicalRepository;
        private Mock<IRequestContext> _mockContext;
        private Mock<ITimeProvider> _mockTimeProvider;
        private ModeDetailCanonicalService _sut;

        public ModeDetailCanonicalServiceTests() {
            _mockMapper = new Mock<IMapper>();
            _mockModeDetailCanonicalRepository = new Mock<IModeDetailCanonicalRepository>();
            _mockContext = new Mock<IRequestContext>();
            _mockTimeProvider = new Mock<ITimeProvider>();
            _sut = new ModeDetailCanonicalService(
                _mockContext.Object, 
                _mockMapper.Object, 
                _mockModeDetailCanonicalRepository.Object,
                _mockTimeProvider.Object);
        }

        [Theory, AutoData]
        public async void SearchByCriteria_Returns_ModeDetailCanonicalResponse(
            List<ModeDetailCanonical> modeDetailCanonicals, 
            List<ModeDetailCanonicalItem> expected) {

            _mockModeDetailCanonicalRepository.Setup(x => x.GetAll())
                .ReturnsAsync(modeDetailCanonicals);

            _mockMapper.Setup(x => x.Map<IEnumerable<ModeDetailCanonicalItem>>(modeDetailCanonicals))
                .Returns(expected);

            var result = await _sut.SearchByCriteria();

            Assert.Equal(result.ModeDetailCanonicals, expected);
        }

        [Theory, AutoData]
        public async void GetByExternalId_Throws_Exception_If_ResourceNotFound(
            ModeDetailCanonical modeDetailCanonical) {

            _mockModeDetailCanonicalRepository
                .Setup(x => x.GetByExternalIds(new[] { modeDetailCanonical.ExternalId }))
                .ReturnsAsync(Array.Empty<ModeDetailCanonical>());

            var result = await _sut.GetByExternalId(modeDetailCanonical.ExternalId);
            
            Assert.Null(result);        
        }

        [Theory, AutoData]
        public async void GetByExternalId_Returns_ModeDetailCanonicalItem(
            ModeDetailCanonical modeDetailCanonical,
            ModeDetailCanonicalItem expected) {

            _mockModeDetailCanonicalRepository
                .Setup(x => x.GetByExternalIds(new[] { modeDetailCanonical.ExternalId }))
                .ReturnsAsync(new[] { modeDetailCanonical });

            _mockMapper.Setup(x => x.Map<ModeDetailCanonicalItem>(modeDetailCanonical))
                .Returns(expected);

            var result = await _sut.GetByExternalId(modeDetailCanonical.ExternalId);

            Assert.Equal(result, expected);
        }

        [Theory, AutoData]
        public async void Delete_Removes_ModeDetailCanonicalItems(
            ModeDetailCanonical modeDetailCanonical) {

            var itemsToDelete = new[] { modeDetailCanonical };

            _mockModeDetailCanonicalRepository
                .Setup(x => x.GetByExternalIds(new[] { modeDetailCanonical.ExternalId }))
                .ReturnsAsync(itemsToDelete);

            var result = await _sut.Delete(modeDetailCanonical.ExternalId);

            _mockModeDetailCanonicalRepository.Verify(x => x.RemoveRange(itemsToDelete), Times.Once);

            _mockModeDetailCanonicalRepository.Verify(x => x.SaveAsync(), Times.Once);

            Assert.True(result);
        }

        [Theory, AutoData]
        public async void Delete_DoesNothing_If_ResourceNotFound(
            ModeDetailCanonical modeDetailCanonical) {

            _mockModeDetailCanonicalRepository
                .Setup(x => x.GetByExternalIds(new[] { modeDetailCanonical.ExternalId }))
                .ReturnsAsync(Array.Empty<ModeDetailCanonical>());

            var result = await _sut.Delete(modeDetailCanonical.ExternalId);

            _mockModeDetailCanonicalRepository
                .Verify(x => x.RemoveRange(It.IsAny<IEnumerable<ModeDetailCanonical>>()), Times.Never);

            _mockModeDetailCanonicalRepository.Verify(x => x.SaveAsync(), Times.Never);

            Assert.False(result);
        }

        [Theory, AutoData]
        public async void Update_Updates_ModeDetailCanonical(
            ModeDetailCanonicalUpsert upsertRequest,
            ModeDetailCanonical modeDetailCanonical,
            Guid externalId) {

            _mockModeDetailCanonicalRepository
                .Setup(x => x.GetByExternalIds(new[] { externalId }))
                .ReturnsAsync(new[] { modeDetailCanonical });

            ModeDetailCanonical result = null;
            _mockMapper.Setup(x => x.Map<ModeDetailCanonical, ModeDetailCanonicalItem>(It.IsAny<ModeDetailCanonical>()))
                .Callback<ModeDetailCanonical>(x => result = x);

            await _sut.Update(upsertRequest, externalId);

            _mockModeDetailCanonicalRepository.Verify(x => x.SaveAsync(), Times.Once);

            result.Equals(upsertRequest);
            Assert.Equal(result.LastModifiedBy, _mockContext.Object.UserId);
            Assert.Equal(result.LastModifiedDate, _mockTimeProvider.Object.UTCNow());
        }

        [Theory, AutoData]
        public async void Update_ReturnsNull_If_ModeDetailCanonical_NotFound(
            ModeDetailCanonicalUpsert upsertRequest,
            Guid externalId) {

            _mockModeDetailCanonicalRepository
                .Setup(x => x.GetByExternalIds(new[] { externalId }))
                .ReturnsAsync(Array.Empty<ModeDetailCanonical>());

            var result = await _sut.Update(upsertRequest, externalId);

            _mockModeDetailCanonicalRepository.Verify(x => x.SaveAsync(), Times.Never);

            Assert.Null(result);
        }

        [Theory, AutoData]
        public async void Create_Creates_ModeDetailCanonical(
            ModeDetailCanonicalUpsert upsertRequest) {

            ModeDetailCanonical created = null;
            _mockModeDetailCanonicalRepository
                .Setup(x => x.Add(It.IsAny<ModeDetailCanonical>()))
                .Callback<ModeDetailCanonical>(x => created = x);

            var result = await _sut.Create(upsertRequest);

            _mockModeDetailCanonicalRepository.Verify(x => x.SaveAsync(), Times.Once);

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
