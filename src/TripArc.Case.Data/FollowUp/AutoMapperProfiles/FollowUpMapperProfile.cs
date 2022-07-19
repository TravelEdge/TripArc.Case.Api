using TripArc.Case.Shared.FollowUp.InputModels;
using TripArc.Case.Shared.FollowUp.Queries;

namespace TripArc.Case.Data.FollowUp.AutoMapperProfiles;

public class FollowUpMapperProfile : AutoMapper.Profile
{
    public FollowUpMapperProfile()
    {
        CreateMap<FollowUpSearchByProfileIdInputModel, FollowUpSearchByProfileIdQuery>();

        CreateMap<Domain.FollowUp.DTO.FollowUp, FollowUpSearchByProfileIdResponse>();

        CreateMap<FollowUpSearchByProfileIdQuery, Domain.FollowUp.DTO.GetFollowUpRepositoryParameters>();
    }
}