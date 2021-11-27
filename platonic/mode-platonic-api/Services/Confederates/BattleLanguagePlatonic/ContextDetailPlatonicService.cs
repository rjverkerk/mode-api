using AutoMapper;
using Microsoft.EntityFrameworkCore;
using mode_platonic_api.Common;
using mode_platonic_api.Contracts.Confederates.BattleLanguagePlatonic.ContextDetailPlatonic;
using mode_platonic_api.data.Repositories.Confederates.BattleLanguagePlatonic;
using mode_platonic_api.Domain.DomainModel.Confederates.BattleLanguagePlatonic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mode_platonic_api.Services.Confederates.BattleLanguagePlatonic
{
    public class ContextDetailPlatonicService : IContextDetailPlatonicService
    {
        private readonly IContextDetailPlatonicRepository _contextDetailPlatonicRepository;
        private readonly IMapper _mapper;
        private readonly IRequestContext _context;

        public ContextDetailPlatonicService(IRequestContext context, IMapper mapper, IContextDetailPlatonicRepository contextDetailPlatonicRepository)
        {
            _contextDetailPlatonicRepository = contextDetailPlatonicRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ContextDetailPlatonicResponse> SearchByCriteria() {
            var contextDetailPlatonics = await _contextDetailPlatonicRepository.GetAll().ToListAsync();
            return new ContextDetailPlatonicResponse() {
                ContextDetailPlatonics = _mapper.Map<IEnumerable<ContextDetailPlatonicItem>>(contextDetailPlatonics)
            };
        }

        public async Task<ContextDetailPlatonicItem> GetById(Guid id) {
            var contextDetailPlatonics = await _contextDetailPlatonicRepository.GetByExternalId(id).FirstOrDefaultAsync();
            return _mapper.Map<ContextDetailPlatonicItem>(contextDetailPlatonics);
        }

        public async Task Delete(IEnumerable<Guid> externalIds) {
            var contextDetailPlatonics = _contextDetailPlatonicRepository.Find(x => externalIds.Contains(x.ExternalId)).ToList();

            _contextDetailPlatonicRepository.RemoveRange(contextDetailPlatonics);

            await _contextDetailPlatonicRepository.SaveAsync();
        }

        public async Task<ContextDetailPlatonicItem> Update(ContextDetailPlatonicUpsert contextDetailPlatonic, Guid externalId) {
            var contextDetailPlatonicToUpdate = await _contextDetailPlatonicRepository
                .GetByExternalId(externalId)
                .FirstOrDefaultAsync();

            contextDetailPlatonicToUpdate.Update(
                new ContextDetailPlatonicDto(contextDetailPlatonicToUpdate.ExternalId, contextDetailPlatonic.NameContextDetailPlatonic, _context.UserId));

            await _contextDetailPlatonicRepository.SaveAsync();

            return _mapper.Map<ContextDetailPlatonicItem>(contextDetailPlatonicToUpdate);
        }

        public async Task<ContextDetailPlatonicItem> Create(ContextDetailPlatonicUpsert contextDetailPlatonic) {
            var dto = new ContextDetailPlatonicDto(Guid.NewGuid(), contextDetailPlatonic.NameContextDetailPlatonic, _context.UserId);

            var contextDetailPlatonicToCreate = new ContextDetailPlatonic(dto);

            _contextDetailPlatonicRepository.Add(contextDetailPlatonicToCreate);

            await _contextDetailPlatonicRepository.SaveAsync();

            return _mapper.Map<ContextDetailPlatonicItem>(contextDetailPlatonicToCreate);
        }
    }
}
