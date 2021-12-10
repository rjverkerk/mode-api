using AutoMapper;
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
        private readonly ITimeProvider _timeProvider;

        public ModeDetailPlatonicService(
            IRequestContext context, 
            IMapper mapper, 
            IModeDetailPlatonicRepository modeDetailPlatonicRepository,
            ITimeProvider timeProvider)
        {
            _modeDetailPlatonicRepository = modeDetailPlatonicRepository;
            _mapper = mapper;
            _context = context;
            _timeProvider = timeProvider;
        }

        public async Task<ModeDetailPlatonicResponse> SearchByCriteria() {
            var modeDetailPlatonics = await _modeDetailPlatonicRepository.GetAll();

            return new ModeDetailPlatonicResponse() {
                ModeDetailPlatonics = _mapper.Map<IEnumerable<ModeDetailPlatonicItem>>(modeDetailPlatonics)
            };
        }

        public async Task<ModeDetailPlatonicItem> GetByExternalId(Guid externalId) {
            var modeDetailPlatonic = (await _modeDetailPlatonicRepository.GetByExternalIds(new[] { externalId }))
                .FirstOrDefault();

            if (modeDetailPlatonic == null) {
                return null;
            }

            return _mapper.Map<ModeDetailPlatonicItem>(modeDetailPlatonic);
        }

        public async Task<bool> Delete(Guid externalId) {
            var modeDetailPlatonics = await _modeDetailPlatonicRepository.GetByExternalIds(new[] { externalId });

            if (!modeDetailPlatonics.Any()) {
                return false;
            }

            _modeDetailPlatonicRepository.RemoveRange(modeDetailPlatonics);

            await _modeDetailPlatonicRepository.SaveAsync();

            return true;
        }

        public async Task<ModeDetailPlatonicItem> Update(ModeDetailPlatonicUpsert modeDetailPlatonic, Guid externalId) {
            var modeDetailPlatonicToUpdate = (await _modeDetailPlatonicRepository
                .GetByExternalIds(new[] { externalId }))
                .FirstOrDefault();

            if (modeDetailPlatonicToUpdate == null) {
                return null;
            }

            modeDetailPlatonicToUpdate.Update(
                new ModeDetailPlatonicDto(
                    modeDetailPlatonicToUpdate.ExternalId, 
                    modeDetailPlatonic.NamePlatonic, 
                    _context.UserId
                    ), _timeProvider.UTCNow());

            await _modeDetailPlatonicRepository.SaveAsync();

            return _mapper.Map<ModeDetailPlatonic, ModeDetailPlatonicItem>(modeDetailPlatonicToUpdate);
        }

        public async Task<ModeDetailPlatonicItem> Create(ModeDetailPlatonicUpsert modeDetailPlatonic) {
            var dto = new ModeDetailPlatonicDto(
                Guid.NewGuid(), 
                modeDetailPlatonic.NamePlatonic, 
                _context.UserId);

            var modeDetailPlatonicToCreate = new ModeDetailPlatonic(dto, _timeProvider.UTCNow());

            _modeDetailPlatonicRepository.Add(modeDetailPlatonicToCreate);

            await _modeDetailPlatonicRepository.SaveAsync();

            return _mapper.Map<ModeDetailPlatonicItem>(modeDetailPlatonicToCreate);
        }
    }
}
