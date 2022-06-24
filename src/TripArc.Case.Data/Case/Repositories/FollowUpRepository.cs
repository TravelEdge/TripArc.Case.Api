using System;
using TripArc.Case.Domain.Action.Enums;
using TripArc.Case.Domain.Case.Abstractions;
using TripArc.Case.Domain.Case.Entities;
using TripArc.Case.Domain.Case.Enums;

namespace TripArc.Case.Data.Case.Repositories;

public class FollowUpRepository : IFollowUpRepository
{
    private readonly List<int> _caseRoles = new() {(int) CaseRoleType.Agent, (int) CaseRoleType.Client};
    private readonly List<int> _travelerCaseRoles = new()
    {
        (int) CaseRoleType.Traveler, 
        (int) CaseRoleType.Family, 
        (int) CaseRoleType.Assistant, 
        (int) CaseRoleType.Friend
    };
    private readonly List<int> _excludedStatus = new() {(int) CaseStatus.Retired, (int) CaseStatus.Deleted};
    private readonly List<int> _excludedActionTypes = new()
    {
        (int) ActionTypes.AirFirstQuote,
        (int) ActionTypes.AirReQuote,
        (int) ActionTypes.FirstQuote,
        (int) ActionTypes.Lead,
        (int) ActionTypes.CreateQuote
    };
    
    private readonly CaseContext _dbContext;

    public FollowUpRepository(CaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<FollowUp>> GetFollowUpsAsync(int profileId)
    {
        var tmtxAction =
            from a in _dbContext.Actions.AsNoTracking()
            join ca in _dbContext.CaseActions.AsNoTracking() on a.ActionId equals ca.ActionId
            join cas in _dbContext.Cases.AsNoTracking() on ca.CaseId equals cas.CaseId
            join trip in _dbContext.Trips.AsNoTracking() on cas.TripId equals trip.TripId into tmpTrips
            from trips in tmpTrips.DefaultIfEmpty()

            let itineraryQuoteId = (
                from ca in _dbContext.CaseActions.AsNoTracking().Where(x => x.CaseId == ca.CaseId && !x.Deleted)
                join aqr in _dbContext.ActionQuoteReferences.AsNoTracking() on ca.ActionId equals aqr.ActionId
                orderby aqr.LastModified descending
                select aqr.ItineraryQuoteId
            ).FirstOrDefault()
            
            let profileIdCase = _dbContext.CaseProfiles.AsNoTracking()
                .Where(cp => cp.CaseId == ca.CaseId && _caseRoles.Contains(cp.CaseRole) && !cp.Deleted)
                .Select(cp => new {cp.ProfileId, cp.CaseRole})
                .FirstOrDefault()

            let profileIdFirstTraveler = _dbContext.CaseProfiles.AsNoTracking()
                .Where(cp => cp.CaseId == ca.CaseId && _travelerCaseRoles.Contains(cp.CaseRole) && !cp.Deleted)
                .Select(cp => new {cp.ProfileId, cp.CaseRole})
                .FirstOrDefault()            
            
            let caseHasAnAgent = _dbContext.CaseProfiles
                .Any(x => x.CaseId == ca.CaseId && (x.CaseRole == (int)CaseRoleType.Agent || x.CaseRole == (int)CaseRoleType.SupportingAgent))
            
            where ca.IsPrimary
                  && !_excludedStatus.Contains(cas.CaseStatus)
                  && !_excludedActionTypes.Contains(a.ActionTypeId)
                  && a.FollowUp
                  && a.CompletedDate == null
                  && a.FollowUpDate <= DateTime.Now
                  && a.FollowUpDate > DateTime.Now.AddYears(-1)
                  && !a.FollowUpDeleted
                  && a.AssignedTo == profileId
            orderby ca.CaseId
            select new FollowUp
            {
                ClientName = string.Empty,
                ActionId = a.ActionId,
                CaseId = ca.CaseId,
                CaseName = Convert.ToString(cas.Name),
                CaseRole = profileIdCase != null ? (CaseRoleType)profileIdCase.CaseRole : (CaseRoleType)profileIdFirstTraveler.CaseRole,
                ProfileId = profileIdCase != null ? profileIdCase.ProfileId : profileIdFirstTraveler.ProfileId,
                TripId = cas.TripId,
                TripName = Convert.ToString(trips.TripName),			
                TripStartDate = trips.TripStartDate,
                FollowUpNotes = a.FollowUpNotes ?? a.Action,
                DueDate = a.FollowUpDate,
                HasAnAgent = caseHasAnAgent,
                Channel = string.Empty,
                Flagged = false,
                ItineraryQuoteId = itineraryQuoteId,
                MostRecentItinerary = string.Empty,			
                LastQuotedDate = null,
                LastQuotedPrice = 0.0,
            };

        var tmtxProfileActions =
            from a in _dbContext.Actions
            where a.FollowUp
                  && !a.Deleted
                  && a.CompletedDate == null
                  && a.FollowUpDate <= DateTime.Now
                  && a.FollowUpDate > DateTime.Now.AddYears(-1)
                  && !a.FollowUpDeleted
                  && a.AssignedTo == profileId
            join pc in _dbContext.ProfileActions on a.ActionId equals pc.ActionId
            select new FollowUp
            {
                ClientName = string.Empty,
                ActionId = a.ActionId,
                CaseId = 0,
                CaseName = string.Empty,
                CaseRole = CaseRoleType.None,
                ProfileId = 0,
                TripId = 0,
                TripName = string.Empty,
                TripStartDate = null,
                FollowUpNotes = a.FollowUpNotes ?? a.Action,
                DueDate = null,
                HasAnAgent = false,
                Channel = string.Empty,
                Flagged = false,
                ItineraryQuoteId = 0,
                MostRecentItinerary = string.Empty,
                LastQuotedDate = null,
                LastQuotedPrice = 0.0
            };            

        var followUps = tmtxAction.Union(tmtxProfileActions);

        return await Task.FromResult(followUps);
    }
}