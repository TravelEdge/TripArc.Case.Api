using TripArc.Case.Shared.Case.InputModels;
using TripArc.Case.Shared.Case.Queries;
using Entities = TripArc.Case.Domain.Case.Entities;

namespace TripArc.Case.Data.Case.AutoMapperProfiles;

public class CaseMapperProfile : AutoMapper.Profile
{
    public CaseMapperProfile()
    {
        CreateMap<Entities.Case, CaseBaseResponse>();
        CreateMap<Entities.Case, CaseSearchByIdResponse>();

        CreateMap<CaseSearchByIdInputModel, CaseSearchByIdQuery>();
        CreateMap<FollowUpSearchByProfileIdInputModel, FollowUpSearchByProfileIdQuery>();

        CreateMap<Entities.FollowUp, FollowUpSearchByProfileIdResponse>();
    }
}