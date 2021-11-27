using AutoMapper;
using Microsoft.EntityFrameworkCore;
using mode_platonic_api.Common;
using mode_platonic_api.Contracts.Confederates.BattleLanguagePlatonic.ModeDetailPlatonic;
using mode_platonic_api.data.Repositories.Confederates.BattleLanguagePlatonic;
using mode_platonic_api.Domain.DomainModel.Confederates.BattleLanguagePlatonic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mode_platonic_api.Services.Confederates.BattleLanguagePlatonic
{
    public class ModeDetailPlatonicService : IModeDetailPlatonicService
    {
        private readonly IModeDetailPlatonicRepository _modeDetailPlatonicRepository;
        private readonly IMapper _mapper;
        private readonly IRequestContext _context;

        public ModeDetailPlatonicService(IRequestContext context, IMapper mapper, IModeDetailPlatonicRepository modeDetailPlatonicRepository)
        {
            _modeDetailPlatonicRepository = modeDetailPlatonicRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ModeDetailPlatonicResponse> SearchByCriteria() {
            var modeDetailPlatonics = await _modeDetailPlatonicRepository.GetAll().ToListAsync();
            return new ModeDetailPlatonicResponse() {
                ModeDetailPlatonics = _mapper.Map<IEnumerable<ModeDetailPlatonicItem>>(modeDetailPlatonics)
            };
        }

        public async Task<ModeDetailPlatonicItem> GetById(Guid id) {
            var modeDetailPlatonics = await _modeDetailPlatonicRepository.GetByExternalId(id).FirstOrDefaultAsync();
            return _mapper.Map<ModeDetailPlatonicItem>(modeDetailPlatonics);
        }

        public async Task Delete(IEnumerable<Guid> externalIds) {
            var modeDetailPlatonics = _modeDetailPlatonicRepository.Find(x => externalIds.Contains(x.ExternalId)).ToList();

            _modeDetailPlatonicRepository.RemoveRange(modeDetailPlatonics);

            await _modeDetailPlatonicRepository.SaveAsync();
        }

        public async Task<ModeDetailPlatonicItem> Update(ModeDetailPlatonicUpsert modeDetailPlatonic, Guid externalId) {
            var modeDetailPlatonicToUpdate = await _modeDetailPlatonicRepository
                .GetByExternalId(externalId)
                .FirstOrDefaultAsync();

            modeDetailPlatonicToUpdate.Update(
                new ModeDetailPlatonicDto(modeDetailPlatonicToUpdate.ExternalId, modeDetailPlatonic.NamePlatonic, _context.UserId));

            await _modeDetailPlatonicRepository.SaveAsync();

            return _mapper.Map<ModeDetailPlatonicItem>(modeDetailPlatonicToUpdate);
        }

        public async Task<ModeDetailPlatonicItem> Create(ModeDetailPlatonicUpsert modeDetailPlatonic) {
            var dto = new ModeDetailPlatonicDto(Guid.NewGuid(), modeDetailPlatonic.NamePlatonic, _context.UserId);

            var modeDetailPlatonicToCreate = new ModeDetailPlatonic(dto);

            _modeDetailPlatonicRepository.Add(modeDetailPlatonicToCreate);

            await _modeDetailPlatonicRepository.SaveAsync();

            return _mapper.Map<ModeDetailPlatonicItem>(modeDetailPlatonicToCreate);
        }
    }
}
