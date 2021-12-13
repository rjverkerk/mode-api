using AutoFixture;
using AutoFixture.Xunit2;
using mode_canonical_api.data.Repositories.Confederates.BattleLanguageCanonical;
using mode_canonical_api.Domain;
using mode_canonical_api.Domain.DomainModel.Confederates.BattleLanguageCanonical;
using mode_canonical_api.UnitTests.Common.Confederates.BattleLanguageCanonical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace mode_canonical_api.UnitTests.Repositories.Confederates.BattleLanguageCanonical
{
    public class ModeDetailCanonicalRepositoryTests : BaseRepositoryTest
    {
        [Theory, AutoData]
        public async void Add_Adds_ModeDetailCanonical() 
        {
            var initialData = await SeedData();
            var expected = CreateModeDetailCanonicals().Take(1).Single();

            using (var context = new ApplicationContext(ContextOptions)) 
            {
                var sut = new ModeDetailCanonicalRepository(context);

                sut.Add(expected);

                await sut.SaveAsync();

                var result = await sut.GetAll();

                Assert.Equal(initialData.Count() + 1, result.Count());
                Assert.Contains(expected, result);
            }
        }

        [Theory, AutoData]
        public async void Remove_Deletes_ModeDetailCanonical() {
            var initialData = await SeedData();
            var toDeleteCount = 1;
            var toDelete = initialData.Take(toDeleteCount).Single();

            using ( var context = new ApplicationContext(ContextOptions) ) {
                var sut = new ModeDetailCanonicalRepository(context);

                sut.Remove(toDelete);

                await sut.SaveAsync();

                var result = await sut.GetAll();

                Assert.Equal(initialData.Count() - toDeleteCount, result.Count());
                Assert.DoesNotContain(toDelete, result);
            }
        }

        [Theory, AutoData]
        public async void RemoveRange_Deletes_ModeDetailCanonicals() {
            var initialData = await SeedData();
            var toDeleteCount = 2;
            var toDelete = initialData.Take(toDeleteCount);

            using ( var context = new ApplicationContext(ContextOptions) ) {
                var sut = new ModeDetailCanonicalRepository(context);

                sut.RemoveRange(toDelete);

                await sut.SaveAsync();

                var result = await sut.GetAll();

                Assert.Equal(initialData.Count() - toDeleteCount, result.Count());
                Assert.All(toDelete, x => Assert.DoesNotContain(x, result));
            }
        }

        [Theory, AutoData]
        public async void GetByExternalIds_Returns_ModeDetailCanonical() {
            var initialData = await SeedData();
            var toFetchCount = 2;
            var expected = initialData.Take(toFetchCount);

            using ( var context = new ApplicationContext(ContextOptions) ) {
                var sut = new ModeDetailCanonicalRepository(context);

                var result = await sut.GetByExternalIds(expected.Select(x => x.ExternalId));

                Assert.True(expected.SequenceEqual(result, new ModeDetailCanonicalComparer()));
            }
        }

        private async Task<IEnumerable<ModeDetailCanonical>> SeedData() 
        {
            using ( var context = new ApplicationContext(ContextOptions) ) {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var sut = new ModeDetailCanonicalRepository(context);

                var expected = CreateModeDetailCanonicals();
                sut.AddRange(expected);

                await sut.SaveAsync();

                var result = await sut.GetAll();

                Assert.True(result.SequenceEqual(expected, new ModeDetailCanonicalComparer()));

                return result;
            }
        }

        private IEnumerable<ModeDetailCanonical> CreateModeDetailCanonicals() {
            var fixture = new Fixture();
            return fixture
                .Create<IEnumerable<ModeDetailCanonicalDto>>()
                .Select(dto => new ModeDetailCanonical(dto, fixture.Create<DateTime>()))
                .ToList();
        }
    }
}
