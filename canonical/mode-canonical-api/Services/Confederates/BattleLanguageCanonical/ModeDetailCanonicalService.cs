using AutoMapper;
using mode_canonical_api.Common;
using mode_canonical_api.Contracts.Confederates.BattleLanguageCanonical.ModeDetailCanonical;
using mode_canonical_api.data.Repositories.Confederates.BattleLanguageCanonical;
using mode_canonical_api.Domain.DomainModel.Confederates.BattleLanguageCanonical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mode_canonical_api.Services.Confederates.BattleLanguageCanonical
{
    public class ModeDetailCanonicalService : IModeDetailCanonicalService
    {
        private readonly IModeDetailCanonicalRepository _modeDetailCanonicalRepository;
        private readonly IMapper _mapper;
        private readonly IRequestContext _context;
        private readonly ITimeProvider _timeProvider;

        public ModeDetailCanonicalService(
            IRequestContext context, 
            IMapper mapper, 
            IModeDetailCanonicalRepository modeDetailCanonicalRepository,
            ITimeProvider timeProvider)
        {
            _modeDetailCanonicalRepository = modeDetailCanonicalRepository;
            _mapper = mapper;
            _context = context;
            _timeProvider = timeProvider;
        }

        public async Task<ModeDetailCanonicalResponse> SearchByCriteria() {
            var modeDetailCanonicals = await _modeDetailCanonicalRepository.GetAll();

            return new ModeDetailCanonicalResponse() {
                ModeDetailCanonicals = _mapper.Map<IEnumerable<ModeDetailCanonicalItem>>(modeDetailCanonicals)
            };
        }

        public async Task<ModeDetailCanonicalItem> GetByExternalId(Guid externalId) {
            var modeDetailCanonical = (await _modeDetailCanonicalRepository.GetByExternalIds(new[] { externalId }))
                .FirstOrDefault();

            if (modeDetailCanonical == null) {
                return null;
            }

            return _mapper.Map<ModeDetailCanonicalItem>(modeDetailCanonical);
        }

        public async Task<bool> Delete(Guid externalId) {
            var modeDetailCanonicals = await _modeDetailCanonicalRepository.GetByExternalIds(new[] { externalId });

            if (!modeDetailCanonicals.Any()) {
                return false;
            }

            _modeDetailCanonicalRepository.RemoveRange(modeDetailCanonicals);

            await _modeDetailCanonicalRepository.SaveAsync();

            return true;
        }

        public async Task<ModeDetailCanonicalItem> Update(ModeDetailCanonicalUpsert modeDetailCanonical, Guid externalId) {
            var modeDetailCanonicalToUpdate = (await _modeDetailCanonicalRepository
                .GetByExternalIds(new[] { externalId }))
                .FirstOrDefault();

            if (modeDetailCanonicalToUpdate == null) {
                return null;
            }

            modeDetailCanonicalToUpdate.Update(
                new ModeDetailCanonicalDto(
                    modeDetailCanonicalToUpdate.ExternalId, 
                    modeDetailCanonical.NameCanonical, 
                    _context.UserId
                    ), _timeProvider.UTCNow());

            await _modeDetailCanonicalRepository.SaveAsync();

            return _mapper.Map<ModeDetailCanonical, ModeDetailCanonicalItem>(modeDetailCanonicalToUpdate);
        }

        public async Task<ModeDetailCanonicalItem> Create(ModeDetailCanonicalUpsert modeDetailCanonical) {
            var dto = new ModeDetailCanonicalDto(
                Guid.NewGuid(), 
                modeDetailCanonical.NameCanonical, 
                _context.UserId);

            var modeDetailCanonicalToCreate = new ModeDetailCanonical(dto, _timeProvider.UTCNow());

            _modeDetailCanonicalRepository.Add(modeDetailCanonicalToCreate);

            await _modeDetailCanonicalRepository.SaveAsync();

            return _mapper.Map<ModeDetailCanonicalItem>(modeDetailCanonicalToCreate);
        }
    }
}
