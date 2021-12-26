using AutoFixture.Xunit2;
using mode_canonical_api.Contracts.Confederates.BattleLanguageCanonical.ModeDetailCanonical;
using mode_canonical_api.Controllers.Confederates.BattleLanguageCanonical;
using mode_canonical_api.Services.Confederates.BattleLanguageCanonical;
using Moq;
using System;
using mode_canonical_api.Tests.Common;
using System.Net;
using Xunit;

namespace mode_canonical_api.Tests.Controllers.Confederates.BattleLanguageCanonical
{
    public class ModeDetailCanonicalControllerTests
    {
        private Mock<IModeDetailCanonicalService> _mockModeDetailCanonicalService;
        private ModeDetailCanonicalController _sut;

        public ModeDetailCanonicalControllerTests() {
            _mockModeDetailCanonicalService = new Mock<IModeDetailCanonicalService>();
            _sut = new ModeDetailCanonicalController(_mockModeDetailCanonicalService.Object);
        }

        [Theory, AutoData]
        public async void SearchAsync_Returns_ModeDetailCanonicalItemResponse(
            ModeDetailCanonicalResponse expected) {
            _mockModeDetailCanonicalService
                .Setup(x => x.SearchByCriteria()).ReturnsAsync(expected);

            var response = await _sut.SearchAsync()
                .AssertSuccessful();

            Assert.Equal(expected, response);
        }

        [Theory, AutoData]
        public async void GetByExternalId_Returns_ModeDetailCanonicalItemResponse(
            ModeDetailCanonicalItem expected) {
            _mockModeDetailCanonicalService
                .Setup(x => x.GetByExternalId(expected.Id)).ReturnsAsync(expected);

            var response = await _sut.GetByExternalIdAsync(expected.Id)
                .AssertSuccessful();

            Assert.Equal(expected, response);
        }

        [Theory, AutoData]
        public async void GetByExternalId_Returns_NotFound_IfNotExists(Guid id) {
            _mockModeDetailCanonicalService
                .Setup(x => x.GetByExternalId(It.IsAny<Guid>()))
                .ReturnsAsync((ModeDetailCanonicalItem)null);

            await _sut.GetByExternalIdAsync(id)
                .AssertStatusCode(HttpStatusCode.NotFound);
        }

        [Theory, AutoData]
        public async void DeleteAsync_Deletes_ModeDetailCanonical(
            Guid id, bool expected) {
            _mockModeDetailCanonicalService
                .Setup(x => x.Delete(id)).ReturnsAsync(expected);

            var result = await _sut.DeleteAsync(id)
                .AssertSuccessful();

            _mockModeDetailCanonicalService.Verify(x => x.Delete(id), Times.Once);

            Assert.Equal(expected, result);
        }

        [Theory, AutoData]
        public async void UpdateAsync_Updates_ModeDetailCanonical(
            ModeDetailCanonicalUpsert upsertRequest, Guid id, ModeDetailCanonicalItem expected) {
            _mockModeDetailCanonicalService
                .Setup(x => x.Update(upsertRequest, id)).ReturnsAsync(expected);

            var result = await _sut.UpdateAsync(id, upsertRequest)
                .AssertSuccessful();

            _mockModeDetailCanonicalService.Verify(x => x.Update(upsertRequest, id), Times.Once);

            Assert.Equal(expected, result);
        }

        [Theory, AutoData]
        public async void UpdateAsync_Returns_NotFound_IfNotExists(
            ModeDetailCanonicalUpsert upsertRequest, Guid id) {
            _mockModeDetailCanonicalService
                .Setup(x => x.Update(It.IsAny<ModeDetailCanonicalUpsert>(), It.IsAny<Guid>()))
                .ReturnsAsync((ModeDetailCanonicalItem)null);

            await _sut.UpdateAsync(id, upsertRequest)
                .AssertStatusCode(HttpStatusCode.NotFound);
        }

        [Theory, AutoData]
        public async void CreateAsync_Creates_ModeDetailCanonical(
            ModeDetailCanonicalUpsert upsertRequest, ModeDetailCanonicalItem expected) {
            _mockModeDetailCanonicalService
                .Setup(x => x.Create(upsertRequest)).ReturnsAsync(expected);

            var result = await _sut.CreateAsync(upsertRequest)
                .AssertSuccessful();

            _mockModeDetailCanonicalService.Verify(x => x.Create(upsertRequest), Times.Once);

            Assert.Equal(expected, result);
        }
    }
}
