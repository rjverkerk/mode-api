using AutoFixture;
using AutoFixture.Xunit2;
using mode_api.data.Repositories.Confederates.BattleLanguage;
using mode_api.Domain;
using mode_api.Domain.DomainModel.Confederates.BattleLanguage;
using mode_api.UnitTests.Common.Confederates.BattleLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace mode_api.UnitTests.Repositories.Confederates.BattleLanguage
{
    public class ModeDetailRepositoryTests : BaseRepositoryTest
    {
        [Theory, AutoData]
        public async void Add_Adds_ModeDetail() 
        {
            var initialData = await SeedData();
            var expected = CreateModeDetails().Take(1).Single();

            using (var context = new ApplicationContext(ContextOptions)) 
            {
                var sut = new ModeDetailRepository(context);

                sut.Add(expected);

                await sut.SaveAsync();

                var result = await sut.GetAll();

                Assert.Equal(initialData.Count() + 1, result.Count());
                Assert.Contains(expected, result);
            }
        }

        [Theory, AutoData]
        public async void Remove_Deletes_ModeDetail() {
            var initialData = await SeedData();
            var toDeleteCount = 1;
            var toDelete = initialData.Take(toDeleteCount).Single();

            using ( var context = new ApplicationContext(ContextOptions) ) {
                var sut = new ModeDetailRepository(context);

                sut.Remove(toDelete);

                await sut.SaveAsync();

                var result = await sut.GetAll();

                Assert.Equal(initialData.Count() - toDeleteCount, result.Count());
                Assert.DoesNotContain(toDelete, result);
            }
        }

        [Theory, AutoData]
        public async void RemoveRange_Deletes_ModeDetails() {
            var initialData = await SeedData();
            var toDeleteCount = 2;
            var toDelete = initialData.Take(toDeleteCount);

            using ( var context = new ApplicationContext(ContextOptions) ) {
                var sut = new ModeDetailRepository(context);

                sut.RemoveRange(toDelete);

                await sut.SaveAsync();

                var result = await sut.GetAll();

                Assert.Equal(initialData.Count() - toDeleteCount, result.Count());
                Assert.All(toDelete, x => Assert.DoesNotContain(x, result));
            }
        }

        [Theory, AutoData]
        public async void GetByExternalIds_Returns_ModeDetail() {
            var initialData = await SeedData();
            var toFetchCount = 2;
            var expected = initialData.Take(toFetchCount);

            using ( var context = new ApplicationContext(ContextOptions) ) {
                var sut = new ModeDetailRepository(context);

                var result = await sut.GetByExternalIds(expected.Select(x => x.ExternalId));

                Assert.True(expected.SequenceEqual(result, new ModeDetailComparer()));
            }
        }

        private async Task<IEnumerable<ModeDetail>> SeedData() 
        {
            using ( var context = new ApplicationContext(ContextOptions) ) {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var sut = new ModeDetailRepository(context);

                var expected = CreateModeDetails();
                sut.AddRange(expected);

                await sut.SaveAsync();

                var result = await sut.GetAll();

                Assert.True(result.SequenceEqual(expected, new ModeDetailComparer()));

                return result;
            }
        }

        private IEnumerable<ModeDetail> CreateModeDetails() {
            var fixture = new Fixture();
            return fixture
                .Create<IEnumerable<ModeDetailDto>>()
                .Select(dto => new ModeDetail(dto, fixture.Create<DateTime>()))
                .ToList();
        }
    }
}
