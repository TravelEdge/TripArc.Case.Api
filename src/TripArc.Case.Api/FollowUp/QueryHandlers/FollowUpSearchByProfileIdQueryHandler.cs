using TripArc.Case.Domain.FollowUp.Abstractions;
using TripArc.Case.Domain.ItineraryQuote.Abstractions;
using TripArc.Case.Domain.ItineraryQuote.Entities;
using TripArc.Case.Shared.FollowUp.Queries;
using TripArc.Profile.Client.Profile;
using TripArc.Profile.Shared.Profile.InputModels.Queries;
using TripArc.Profile.Shared.Profile.Queries;

namespace TripArc.Case.Api.FollowUp.QueryHandlers;

public class FollowUpSearchByProfileIdQueryHandler : IQueryHandler<FollowUpSearchByProfileIdQuery,
    IEnumerable<FollowUpSearchByProfileIdResponse>>
{
    private readonly ILogger<FollowUpSearchByProfileIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IProfileApiClient _profileApiClient;
    private readonly IFollowUpRepository _followUpRepository;
    private readonly IItineraryQuoteRepository _itineraryQuoteRepository;

    public FollowUpSearchByProfileIdQueryHandler(ILogger<FollowUpSearchByProfileIdQueryHandler> logger, IMapper mapper,
        IProfileApiClient profileApiClient, IFollowUpRepository followUpRepository, 
        IItineraryQuoteRepository itineraryQuoteRepository)
    {
        
        _logger = logger;
        _mapper = mapper;
        _profileApiClient = profileApiClient;
        _followUpRepository = followUpRepository;
        _itineraryQuoteRepository = itineraryQuoteRepository;
    }

    public async Task<IEnumerable<FollowUpSearchByProfileIdResponse>> ExecuteQueryAsync(FollowUpSearchByProfileIdQuery query)
    {
        _logger.LogInformation($"Searching for the follow-ups");
        var followUps = (await _followUpRepository.GetFollowUpsAsync(query.ProfileId)).ToList();
        if (!followUps.Any())
            return null;

        var profiles = await GetProfilesAsync(followUps.Select(x => x.ProfileId).Distinct());
        
        _logger.LogInformation($"Searching for the latest quotes for {followUps.Count} follow-ups");
        var itineraryQuoteIdList = followUps.Select(x => x.ItineraryQuoteId).Distinct().ToList();
        var latestQuotes = await _itineraryQuoteRepository.GetLatestItineraryQuotes(itineraryQuoteIdList);
        
        followUps = JoinFollowUpWithProfileAndLatestQuotes(followUps, profiles, latestQuotes).ToList(); 

        return _mapper.Map<IEnumerable<FollowUpSearchByProfileIdResponse>>(followUps);
    }

    private IEnumerable<Domain.FollowUp.Entities.FollowUp> JoinFollowUpWithProfileAndLatestQuotes(IEnumerable<Domain.FollowUp.Entities.FollowUp> followUps,
        IEnumerable<ProfileGetFollowUpInfoResponse> profileInfo, IEnumerable<LatestItineraryQuote> latestItineraryQuotes)
    {
        _logger.LogInformation($"Joining the Follow-ups with Profile and Latest Quotes info");
        
        var query = 
            from f in followUps
            join p in profileInfo on f.ProfileId equals p.ClientProfileId into profileGroup
            from prf in profileGroup.DefaultIfEmpty()
            join q in latestItineraryQuotes on f.ItineraryQuoteId equals q.ItineraryQuoteId into latestQuotesGroup
            from lq in latestQuotesGroup.DefaultIfEmpty()
            select new Domain.FollowUp.Entities.FollowUp
            {
                ClientName = prf?.Name,
                ActionId = f.ActionId,
                CaseId = f.CaseId,
                CaseName = f.CaseName,
                ProfileId = f.ProfileId,
                TripId = f.TripId,
                TripName = f.TripName,
                TripStartDate = f.TripStartDate,
                FollowUpNotes = f.FollowUpNotes,
                DueDate = f.DueDate,
                Channel = f.HasAnAgent && prf?.ProfileAgentId > 0 ? "Agent" : prf is {IsRepeat: true} ? "Repeat" : "Direct",
                Flagged = f.Flagged,
                ItineraryQuoteId = f.ItineraryQuoteId,
                MostRecentItinerary = lq.MostRecentItinerary,
                LastQuotedDate = lq.QuotedDate,
                LastQuotedPrice = lq.QuotedPrice
            };
        return query;
    }
    
    private async Task<IEnumerable<ProfileGetFollowUpInfoResponse>> GetProfilesAsync(IEnumerable<int> profileIds)
    {
        var model = new ProfileGetFollowUpInfoInputModel { Ids = string.Join(',', profileIds) };
        
        _logger.LogInformation($"Requesting Profile info to Profile service - IDs: {model.Ids}");
        var profiles = await _profileApiClient.GetProfileFollowUpInfoAsync(model);
        return profiles;
    }
}