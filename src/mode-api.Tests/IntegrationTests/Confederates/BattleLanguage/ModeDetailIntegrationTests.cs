using AutoFixture;
using AutoFixture.Xunit2;
using mode_api_canonical.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace mode_api.Tests.IntegrationTests.Confederates.BattleLanguage
{
    public class ModeDetailIntegrationTests : BaseIntegrationTest
    {
        public ModeDetailIntegrationTests(CustomWebApplicationFactory factory) : base(factory) 
        {
        }

        [Theory, AutoData]
        public async Task Create_Creates_ModeDetailPlatonic(ModeDetailUpsert request) 
        {
            using(var httpClient = CreateHttpClient()) {
                var client = GetModeDetailClient(httpClient);

                var createResponse = await client.CreateAsync(request);

                createResponse.AssertEqual(request);
                Assert.Null(createResponse.LastModifiedBy);
                Assert.Null(createResponse.LastModifiedDate);
                Assert.Equal(1, createResponse.Version);

                var getResponse = await client.GetByExternalIdAsync(createResponse.Id);

                Assert.Equal(createResponse, getResponse, new ModeDetailItemComparer());
            }
        }

        [Fact]
        public async Task Delete_Deletes_ModeDetailPlatonic() {
            var modeDetailPlatonics = await SeedData();
            var toDelete = modeDetailPlatonics.Take(1).Single();

            using(var httpClient = CreateHttpClient()) {
                var client = GetModeDetailClient(httpClient);

                var deleteResponse = await client.DeleteAsync(toDelete.Id);

                Assert.True(deleteResponse);

                var exception = await Assert.ThrowsAsync<ApiException>(async () => await client.GetByExternalIdAsync(toDelete.Id));

                Assert.NotNull(exception);
                Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)exception.StatusCode);
            }
        }

        [Theory, AutoData]
        public async Task Update_Updates_ModeDetailPlatonic(ModeDetailUpsert expected) {
            var modeDetailPlatonics = await SeedData();
            var toUpdate = modeDetailPlatonics.Take(1).Single();

            using(var httpClient = CreateHttpClient()) {
                var client = GetModeDetailClient(httpClient);

                var updateResponse = await client.UpdateAsync(toUpdate.Id, expected);

                updateResponse.AssertEqual(expected);
                Assert.Equal(2, updateResponse.Version);
                Assert.NotNull(updateResponse.LastModifiedBy);
                Assert.NotNull(updateResponse.LastModifiedDate);
            }
        }

        [Theory, AutoData]
        public async Task Update_Returns_NotFound_If_DoesNotExist(Guid idToUpdate, ModeDetailUpsert request) {
            
            using ( var httpClient = CreateHttpClient() ) {
                var client = GetModeDetailClient(httpClient);
                
                var exception = await Assert.ThrowsAsync<ApiException>(async () => await client.UpdateAsync(idToUpdate, request));

                Assert.NotNull(exception);
                Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)exception.StatusCode);
            }
        }

        private async Task<IEnumerable<ModeDetailItem>> SeedData() {
            var fixture = new Fixture();
            var modeDetailToCreate = Enumerable.Range(1, 3).Select(x => fixture.Create<ModeDetailUpsert>());

            using ( var httpClient = CreateHttpClient() ) {
                var client = GetModeDetailClient(httpClient);

                return await Task.WhenAll(modeDetailToCreate.Select(async x => await client.CreateAsync(x)));
            }
        }
    }
}
