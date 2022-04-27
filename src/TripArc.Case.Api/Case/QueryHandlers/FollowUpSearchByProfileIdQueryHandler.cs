using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripArc.Case.Domain.Case.Abstractions;
using TripArc.Case.Shared.Case.Queries;
using TripArc.Common.CQRS.Queries;
using TripArc.Profile.Client.Profile;
using TripArc.Profile.Shared.Profile.InputModels.Queries;

namespace TripArc.Case.Api.Case.QueryHandlers
{
    public class FollowUpSearchByProfileIdQueryHandler : IQueryHandler<FollowUpSearchByProfileIdQuery,
        IEnumerable<FollowUpSearchByProfileIdResponse>>
    {
        private readonly ICaseRepository _caseRepository;
        private readonly IProfileApiClient _profileApiClient; 

        public FollowUpSearchByProfileIdQueryHandler(ICaseRepository caseRepository, IProfileApiClient profileApiClient)
        {
            _caseRepository = caseRepository;
            _profileApiClient = profileApiClient;
        }

        public async Task<IEnumerable<FollowUpSearchByProfileIdResponse>> ExecuteQueryAsync(FollowUpSearchByProfileIdQuery query)
        {
            var result = await _caseRepository.GetFollowUps(query.ProfileId);

            var model = new ProfileGetByIdsInputModel
            {
                Ids =
                    "71611,242826,61247,257044,68834,42719,304866,28154,307041,316827,247297,260888,317287,317467,317594,317622,319013"
            };
            var profiles = await _profileApiClient.GetByProfileIdsAsync(model);
            
            return Enumerable.Empty<FollowUpSearchByProfileIdResponse>();
        }
    }
}