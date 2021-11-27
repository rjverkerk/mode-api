using AutoMapper;
using mode_platonic_api.Contracts.Confederates.BattleLanguagePlatonic.ModeDetailPlatonic;
using mode_platonic_api.Domain.DomainModel.Confederates.BattleLanguagePlatonic;

public class AutoMapping : Profile
{
    public AutoMapping() {
        CreateMap<ModeDetailPlatonic, ModeDetailPlatonicItem>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExternalId)); ;
        CreateMap<ModeDetailPlatonicItem, ModeDetailPlatonicDto>();

    }
}
