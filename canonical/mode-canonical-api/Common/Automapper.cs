using AutoMapper;
using mode_canonical_api.Contracts.Confederates.BattleLanguageCanonical.ModeDetailCanonical;
using mode_canonical_api.Domain.DomainModel.Confederates.BattleLanguageCanonical;

public class AutoMapping : Profile
{
    public AutoMapping() {
        CreateMap<ModeDetailCanonical, ModeDetailCanonicalItem>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExternalId)); ;
        CreateMap<ModeDetailCanonicalItem, ModeDetailCanonicalDto>();

    }
}
