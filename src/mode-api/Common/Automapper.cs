using AutoMapper;
using mode_api.Contracts.Confederates.BattleLanguage.ModeDetail;
using mode_api.Domain.DomainModel.Confederates.BattleLanguage;

public class AutoMapping : Profile
{
    public AutoMapping() {
        CreateMap<ModeDetail, ModeDetailItem>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExternalId)); ;
        CreateMap<ModeDetailItem, ModeDetailDto>();

    }
}
