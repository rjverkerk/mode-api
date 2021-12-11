using AutoFixture.Xunit2;
using mode_platonic_api.Contracts.Confederates.BattleLanguagePlatonic.ModeDetailPlatonic;
using mode_platonic_api.Controllers.Confederates.BattleLanguagePlatonic;
using mode_platonic_api.Services.Confederates.BattleLanguagePlatonic;
using Moq;
using System;
using mode_platonic_api.UnitTests.Common;
using System.Net;
using Xunit;

namespace mode_platonic_api.UnitTests.Controllers.Confederates.BattleLanguagePlatonic
{
    public class ModeDetailPlatonicControllerTests
    {
        private Mock<IModeDetailPlatonicService> _mockModeDetailPlatonicService;
        private ModeDetailPlatonicController _sut;

        public ModeDetailPlatonicControllerTests() {
            _mockModeDetailPlatonicService = new Mock<IModeDetailPlatonicService>();
            _sut = new ModeDetailPlatonicController(_mockModeDetailPlatonicService.Object);
        }

        [Theory, AutoData]
        public async void SearchAsync_Returns_ModeDetailPlatonicItemResponse(
            ModeDetailPlatonicResponse expected) {
            _mockModeDetailPlatonicService
                .Setup(x => x.SearchByCriteria()).ReturnsAsync(expected);

            var response = await _sut.SearchAsync()
                .AssertSuccessful();

            Assert.Equal(expected, response);
        }

        [Theory, AutoData]
        public async void GetByExternalId_Returns_ModeDetailPlatonicItemResponse(
            ModeDetailPlatonicItem expected) {
            _mockModeDetailPlatonicService
                .Setup(x => x.GetByExternalId(expected.Id)).ReturnsAsync(expected);

            var response = await _sut.GetByExternalIdAsync(expected.Id)
                .AssertSuccessful();

            Assert.Equal(expected, response);
        }

        [Theory, AutoData]
        public async void GetByExternalId_Returns_NotFound(Guid id) {
            _mockModeDetailPlatonicService
                .Setup(x => x.GetByExternalId(It.IsAny<Guid>()))
                .ReturnsAsync((ModeDetailPlatonicItem)null);

            await _sut.GetByExternalIdAsync(id)
                .AssertStatusCode(HttpStatusCode.NotFound);
        }

        [Theory, AutoData]
        public async void DeleteAsync_Deletes_ModeDetailPlatonic(
            Guid id, bool expected) {
            _mockModeDetailPlatonicService
                .Setup(x => x.Delete(id)).ReturnsAsync(expected);

            var result = await _sut.DeleteAsync(id)
                .AssertSuccessful();

            _mockModeDetailPlatonicService.Verify(x => x.Delete(id), Times.Once);

            Assert.Equal(expected, result);
        }

        [Theory, AutoData]
        public async void UpdateAsync_Updates_ModeDetailPlatonic(
            ModeDetailPlatonicUpsert upsertRequest, Guid id, ModeDetailPlatonicItem expected) {
            _mockModeDetailPlatonicService
                .Setup(x => x.Update(upsertRequest, id)).ReturnsAsync(expected);

            var result = await _sut.UpdateAsync(id, upsertRequest)
                .AssertSuccessful();

            _mockModeDetailPlatonicService.Verify(x => x.Update(upsertRequest, id), Times.Once);

            Assert.Equal(expected, result);
        }

        [Theory, AutoData]
        public async void UpdateAsync_Returns_NotFound(
            ModeDetailPlatonicUpsert upsertRequest, Guid id) {
            _mockModeDetailPlatonicService
                .Setup(x => x.Update(It.IsAny<ModeDetailPlatonicUpsert>(), It.IsAny<Guid>()))
                .ReturnsAsync((ModeDetailPlatonicItem)null);

            await _sut.UpdateAsync(id, upsertRequest)
                .AssertStatusCode(HttpStatusCode.NotFound);
        }

        [Theory, AutoData]
        public async void CreateAsync_Creates_ModeDetailPlatonic(
            ModeDetailPlatonicUpsert upsertRequest, ModeDetailPlatonicItem expected) {
            _mockModeDetailPlatonicService
                .Setup(x => x.Create(upsertRequest)).ReturnsAsync(expected);

            var result = await _sut.CreateAsync(upsertRequest)
                .AssertSuccessful();

            _mockModeDetailPlatonicService.Verify(x => x.Create(upsertRequest), Times.Once);

            Assert.Equal(expected, result);
        }
    }
}
