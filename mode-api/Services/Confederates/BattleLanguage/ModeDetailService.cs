using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public ModeDetailService(IRequestContext context, IMapper mapper, IModeDetailRepository modeDetailRepository)
        {
            _modeDetailRepository = modeDetailRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ModeDetailResponse> SearchByCriteria() {
            var modeDetails = await _modeDetailRepository.GetAll().ToListAsync();
            return new ModeDetailResponse() {
                ModeDetails = _mapper.Map<IEnumerable<ModeDetailItem>>(modeDetails)
            };
        }

        public async Task<ModeDetailItem> GetById(Guid id) {
            var modeDetails = await _modeDetailRepository.GetByExternalId(id).FirstOrDefaultAsync();
            return _mapper.Map<ModeDetailItem>(modeDetails);
        }

        public async Task Delete(IEnumerable<Guid> externalIds) {
            var modeDetails = _modeDetailRepository.Find(x => externalIds.Contains(x.ExternalId)).ToList();

            _modeDetailRepository.RemoveRange(modeDetails);

            await _modeDetailRepository.SaveAsync();
        }

        public async Task<ModeDetailItem> Update(ModeDetailUpsert modeDetail, Guid externalId) {
            var modeDetailToUpdate = await _modeDetailRepository
                .GetByExternalId(externalId)
                .FirstOrDefaultAsync();

            modeDetailToUpdate.Update(
                new ModeDetailDto(modeDetailToUpdate.ExternalId, modeDetail.Name, _context.UserId));

            await _modeDetailRepository.SaveAsync();

            return _mapper.Map<ModeDetailItem>(modeDetailToUpdate);
        }

        public async Task<ModeDetailItem> Create(ModeDetailUpsert modeDetail) {
            var dto = new ModeDetailDto(Guid.NewGuid(), modeDetail.Name, _context.UserId);

            var modeDetailToCreate = new ModeDetail(dto);

            _modeDetailRepository.Add(modeDetailToCreate);

            await _modeDetailRepository.SaveAsync();

            return _mapper.Map<ModeDetailItem>(modeDetailToCreate);
        }
    }
}
