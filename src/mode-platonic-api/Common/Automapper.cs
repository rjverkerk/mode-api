using AutoMapper;
using mode_platonic_api.Contracts.Confederates.BattleLanguage.ContextDetail;
using mode_platonic_api.Domain.DomainModel.Confederates.BattleLanguage;

public class AutoMapping : Profile
{
    public AutoMapping() {
        CreateMap<ContextDetail, ContextDetailItem>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExternalId)); ;
        CreateMap<ContextDetailItem, ContextDetailDto>();

    }
}
