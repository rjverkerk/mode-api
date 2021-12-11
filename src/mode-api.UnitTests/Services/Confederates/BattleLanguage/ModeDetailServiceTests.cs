using AutoFixture.Xunit2;
using AutoMapper;
using mode_api.Common;
using mode_api.Contracts.Confederates.BattleLanguage.ModeDetail;
using mode_api.data.Repositories.Confederates.BattleLanguage;
using mode_api.Domain.DomainModel.Confederates.BattleLanguage;
using mode_api.Services.Confederates.BattleLanguage;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace mode_api.UnitTests.Services.Confederates.BattleLanguage
{
    public class ModeDetailServiceTests
    {
        private Mock<IMapper> _mockMapper;
        private Mock<IModeDetailRepository> _mockModeDetailRepository;
        private Mock<IRequestContext> _mockContext;
        private Mock<ITimeProvider> _mockTimeProvider;
        private ModeDetailService _sut;

        public ModeDetailServiceTests() {
            _mockMapper = new Mock<IMapper>();
            _mockModeDetailRepository = new Mock<IModeDetailRepository>();
            _mockContext = new Mock<IRequestContext>();
            _mockTimeProvider = new Mock<ITimeProvider>();
            _sut = new ModeDetailService(
                _mockContext.Object, 
                _mockMapper.Object, 
                _mockModeDetailRepository.Object,
                _mockTimeProvider.Object);
        }

        [Theory, AutoData]
        public async void SearchByCriteria_Returns_ModeDetailResponse(
            List<ModeDetail> modeDetails, 
            List<ModeDetailItem> expected) {

            _mockModeDetailRepository.Setup(x => x.GetAll())
                .ReturnsAsync(modeDetails);

            _mockMapper.Setup(x => x.Map<IEnumerable<ModeDetailItem>>(modeDetails))
                .Returns(expected);

            var result = await _sut.SearchByCriteria();

            Assert.Equal(result.ModeDetails, expected);
        }

        [Theory, AutoData]
        public async void GetByExternalId_Throws_Exception_If_ResourceNotFound(
            ModeDetail modeDetail) {

            _mockModeDetailRepository
                .Setup(x => x.GetByExternalIds(new[] { modeDetail.ExternalId }))
                .ReturnsAsync(Array.Empty<ModeDetail>());

            var result = await _sut.GetByExternalId(modeDetail.ExternalId);
            
            Assert.Null(result);        
        }

        [Theory, AutoData]
        public async void GetByExternalId_Returns_ModeDetailItem(
            ModeDetail modeDetail,
            ModeDetailItem expected) {

            _mockModeDetailRepository
                .Setup(x => x.GetByExternalIds(new[] { modeDetail.ExternalId }))
                .ReturnsAsync(new[] { modeDetail });

            _mockMapper.Setup(x => x.Map<ModeDetailItem>(modeDetail))
                .Returns(expected);

            var result = await _sut.GetByExternalId(modeDetail.ExternalId);

            Assert.Equal(result, expected);
        }

        [Theory, AutoData]
        public async void Delete_Removes_ModeDetailItems(
            ModeDetail modeDetail) {

            var itemsToDelete = new[] { modeDetail };

            _mockModeDetailRepository
                .Setup(x => x.GetByExternalIds(new[] { modeDetail.ExternalId }))
                .ReturnsAsync(itemsToDelete);

            var result = await _sut.Delete(modeDetail.ExternalId);

            _mockModeDetailRepository.Verify(x => x.RemoveRange(itemsToDelete), Times.Once);

            _mockModeDetailRepository.Verify(x => x.SaveAsync(), Times.Once);

            Assert.True(result);
        }

        [Theory, AutoData]
        public async void Delete_DoesNothing_If_ResourceNotFound(
            ModeDetail modeDetail) {

            _mockModeDetailRepository
                .Setup(x => x.GetByExternalIds(new[] { modeDetail.ExternalId }))
                .ReturnsAsync(Array.Empty<ModeDetail>());

            var result = await _sut.Delete(modeDetail.ExternalId);

            _mockModeDetailRepository
                .Verify(x => x.RemoveRange(It.IsAny<IEnumerable<ModeDetail>>()), Times.Never);

            _mockModeDetailRepository.Verify(x => x.SaveAsync(), Times.Never);

            Assert.False(result);
        }

        [Theory, AutoData]
        public async void Update_Updates_ModeDetail(
            ModeDetailUpsert upsertRequest,
            ModeDetail modeDetail,
            Guid externalId) {

            _mockModeDetailRepository
                .Setup(x => x.GetByExternalIds(new[] { externalId }))
                .ReturnsAsync(new[] { modeDetail });

            ModeDetail result = null;
            _mockMapper.Setup(x => x.Map<ModeDetail, ModeDetailItem>(It.IsAny<ModeDetail>()))
                .Callback<ModeDetail>(x => result = x);

            await _sut.Update(upsertRequest, externalId);

            _mockModeDetailRepository.Verify(x => x.SaveAsync(), Times.Once);

            result.Equals(upsertRequest);
            Assert.Equal(result.LastModifiedBy, _mockContext.Object.UserId);
            Assert.Equal(result.LastModifiedDate, _mockTimeProvider.Object.UTCNow());
        }

        [Theory, AutoData]
        public async void Update_ReturnsNull_If_ModeDetail_NotFound(
            ModeDetailUpsert upsertRequest,
            Guid externalId) {

            _mockModeDetailRepository
                .Setup(x => x.GetByExternalIds(new[] { externalId }))
                .ReturnsAsync(Array.Empty<ModeDetail>());

            var result = await _sut.Update(upsertRequest, externalId);

            _mockModeDetailRepository.Verify(x => x.SaveAsync(), Times.Never);

            Assert.Null(result);
        }

        [Theory, AutoData]
        public async void Create_Creates_ModeDetail(
            ModeDetailUpsert upsertRequest) {

            ModeDetail created = null;
            _mockModeDetailRepository
                .Setup(x => x.Add(It.IsAny<ModeDetail>()))
                .Callback<ModeDetail>(x => created = x);

            var result = await _sut.Create(upsertRequest);

            _mockModeDetailRepository.Verify(x => x.SaveAsync(), Times.Once);

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
