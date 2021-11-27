using AutoMapper;
using Microsoft.EntityFrameworkCore;
using mode_platonic_api.Common;
using mode_platonic_api.Contracts.Confederates.BattleLanguage.ContextDetail;
using mode_platonic_api.data.Repositories.Confederates.BattleLanguage;
using mode_platonic_api.Domain.DomainModel.Confederates.BattleLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mode_platonic_api.Services.Confederates.BattleLanguage
{
    public class ContextDetailService : IContextDetailService
    {
        private readonly IContextDetailRepository _contextDetailRepository;
        private readonly IMapper _mapper;
        private readonly IRequestContext _context;

        public ContextDetailService(IRequestContext context, IMapper mapper, IContextDetailRepository contextDetailRepository)
        {
            _contextDetailRepository = contextDetailRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ContextDetailResponse> SearchByCriteria() {
            var contextDetails = await _contextDetailRepository.GetAll().ToListAsync();
            return new ContextDetailResponse() {
                ContextDetails = _mapper.Map<IEnumerable<ContextDetailItem>>(contextDetails)
            };
        }

        public async Task<ContextDetailItem> GetById(Guid id) {
            var contextDetails = await _contextDetailRepository.GetByExternalId(id).FirstOrDefaultAsync();
            return _mapper.Map<ContextDetailItem>(contextDetails);
        }

        public async Task Delete(IEnumerable<Guid> externalIds) {
            var contextDetails = _contextDetailRepository.Find(x => externalIds.Contains(x.ExternalId)).ToList();

            _contextDetailRepository.RemoveRange(contextDetails);

            await _contextDetailRepository.SaveAsync();
        }

        public async Task<ContextDetailItem> Update(ContextDetailUpsert contextDetail, Guid externalId) {
            var contextDetailToUpdate = await _contextDetailRepository
                .GetByExternalId(externalId)
                .FirstOrDefaultAsync();

            contextDetailToUpdate.Update(
                new ContextDetailDto(contextDetailToUpdate.ExternalId, contextDetail.Name, _context.UserId));

            await _contextDetailRepository.SaveAsync();

            return _mapper.Map<ContextDetailItem>(contextDetailToUpdate);
        }

        public async Task<ContextDetailItem> Create(ContextDetailUpsert contextDetail) {
            var dto = new ContextDetailDto(Guid.NewGuid(), contextDetail.Name, _context.UserId);

            var contextDetailToCreate = new ContextDetail(dto);

            _contextDetailRepository.Add(contextDetailToCreate);

            await _contextDetailRepository.SaveAsync();

            return _mapper.Map<ContextDetailItem>(contextDetailToCreate);
        }
    }
}
