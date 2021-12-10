using AutoMapper;
using mode_api.Common;
using mode_api.Contracts.Confederates.BattleLanguage.ModeDetail;
using mode_api.data.Repositories.Confederates.BattleLanguage;
using mode_api.Domain.DomainModel.Confederates.BattleLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mode_api.Services.Confederates.BattleLanguage
{
    public class ModeDetailService : IModeDetailService
    {
        private readonly IModeDetailRepository _modeDetailRepository;
        private readonly IMapper _mapper;
        private readonly IRequestContext _context;
        private readonly ITimeProvider _timeProvider;

        public ModeDetailService(
            IRequestContext context, 
            IMapper mapper, 
            IModeDetailRepository modeDetailRepository,
            ITimeProvider timeProvider)
        {
            _modeDetailRepository = modeDetailRepository;
            _mapper = mapper;
            _context = context;
            _timeProvider = timeProvider;
        }

        public async Task<ModeDetailResponse> SearchByCriteria() {
            var modeDetails = await _modeDetailRepository.GetAll();

            return new ModeDetailResponse() {
                ModeDetails = _mapper.Map<IEnumerable<ModeDetailItem>>(modeDetails)
            };
        }

        public async Task<ModeDetailItem> GetByExternalId(Guid externalId) {
            var modeDetail = (await _modeDetailRepository.GetByExternalIds(new[] { externalId }))
                .FirstOrDefault();

            if (modeDetail == null) {
                return null;
            }

            return _mapper.Map<ModeDetailItem>(modeDetail);
        }

        public async Task<bool> Delete(Guid externalId) {
            var modeDetails = await _modeDetailRepository.GetByExternalIds(new[] { externalId });

            if (!modeDetails.Any()) {
                return false;
            }

            _modeDetailRepository.RemoveRange(modeDetails);

            await _modeDetailRepository.SaveAsync();

            return true;
        }

        public async Task<ModeDetailItem> Update(ModeDetailUpsert modeDetail, Guid externalId) {
            var modeDetailToUpdate = (await _modeDetailRepository
                .GetByExternalIds(new[] { externalId }))
                .FirstOrDefault();

            if (modeDetailToUpdate == null) {
                return null;
            }

            modeDetailToUpdate.Update(
                new ModeDetailDto(
                    modeDetailToUpdate.ExternalId, 
                    modeDetail.Name, 
                    _context.UserId
                    ), _timeProvider.UTCNow());

            await _modeDetailRepository.SaveAsync();

            return _mapper.Map<ModeDetail, ModeDetailItem>(modeDetailToUpdate);
        }

        public async Task<ModeDetailItem> Create(ModeDetailUpsert modeDetail) {
            var dto = new ModeDetailDto(
                Guid.NewGuid(), 
                modeDetail.Name, 
                _context.UserId);

            var modeDetailToCreate = new ModeDetail(dto, _timeProvider.UTCNow());

            _modeDetailRepository.Add(modeDetailToCreate);

            await _modeDetailRepository.SaveAsync();

            return _mapper.Map<ModeDetailItem>(modeDetailToCreate);
        }
    }
}
