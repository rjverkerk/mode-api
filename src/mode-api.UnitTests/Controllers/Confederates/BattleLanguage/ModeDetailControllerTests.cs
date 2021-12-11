using AutoFixture.Xunit2;
using mode_api.Contracts.Confederates.BattleLanguage.ModeDetail;
using mode_api.Controllers.Confederates.BattleLanguage;
using mode_api.Services.Confederates.BattleLanguage;
using Moq;
using System;
using mode_api.UnitTests.Common;
using System.Net;
using Xunit;

namespace mode_api.UnitTests.Controllers.Confederates.BattleLanguage
{
    public class ModeDetailControllerTests
    {
        private Mock<IModeDetailService> _mockModeDetailService;
        private ModeDetailController _sut;

        public ModeDetailControllerTests() {
            _mockModeDetailService = new Mock<IModeDetailService>();
            _sut = new ModeDetailController(_mockModeDetailService.Object);
        }

        [Theory, AutoData]
        public async void SearchAsync_Returns_ModeDetailItemResponse(
            ModeDetailResponse expected) {
            _mockModeDetailService
                .Setup(x => x.SearchByCriteria()).ReturnsAsync(expected);

            var response = await _sut.SearchAsync()
                .AssertSuccessful();

            Assert.Equal(expected, response);
        }

        [Theory, AutoData]
        public async void GetByExternalId_Returns_ModeDetailItemResponse(
            ModeDetailItem expected) {
            _mockModeDetailService
                .Setup(x => x.GetByExternalId(expected.Id)).ReturnsAsync(expected);

            var response = await _sut.GetByExternalIdAsync(expected.Id)
                .AssertSuccessful();

            Assert.Equal(expected, response);
        }

        [Theory, AutoData]
        public async void GetByExternalId_Returns_NotFound(Guid id) {
            _mockModeDetailService
                .Setup(x => x.GetByExternalId(It.IsAny<Guid>()))
                .ReturnsAsync((ModeDetailItem)null);

            await _sut.GetByExternalIdAsync(id)
                .AssertStatusCode(HttpStatusCode.NotFound);
        }

        [Theory, AutoData]
        public async void DeleteAsync_Deletes_ModeDetail(
            Guid id, bool expected) {
            _mockModeDetailService
                .Setup(x => x.Delete(id)).ReturnsAsync(expected);

            var result = await _sut.DeleteAsync(id)
                .AssertSuccessful();

            _mockModeDetailService.Verify(x => x.Delete(id), Times.Once);

            Assert.Equal(expected, result);
        }

        [Theory, AutoData]
        public async void UpdateAsync_Updates_ModeDetail(
            ModeDetailUpsert upsertRequest, Guid id, ModeDetailItem expected) {
            _mockModeDetailService
                .Setup(x => x.Update(upsertRequest, id)).ReturnsAsync(expected);

            var result = await _sut.UpdateAsync(id, upsertRequest)
                .AssertSuccessful();

            _mockModeDetailService.Verify(x => x.Update(upsertRequest, id), Times.Once);

            Assert.Equal(expected, result);
        }

        [Theory, AutoData]
        public async void UpdateAsync_Returns_NotFound(
            ModeDetailUpsert upsertRequest, Guid id) {
            _mockModeDetailService
                .Setup(x => x.Update(It.IsAny<ModeDetailUpsert>(), It.IsAny<Guid>()))
                .ReturnsAsync((ModeDetailItem)null);

            await _sut.UpdateAsync(id, upsertRequest)
                .AssertStatusCode(HttpStatusCode.NotFound);
        }

        [Theory, AutoData]
        public async void CreateAsync_Creates_ModeDetail(
            ModeDetailUpsert upsertRequest, ModeDetailItem expected) {
            _mockModeDetailService
                .Setup(x => x.Create(upsertRequest)).ReturnsAsync(expected);

            var result = await _sut.CreateAsync(upsertRequest)
                .AssertSuccessful();

            _mockModeDetailService.Verify(x => x.Create(upsertRequest), Times.Once);

            Assert.Equal(expected, result);
        }
    }
}
