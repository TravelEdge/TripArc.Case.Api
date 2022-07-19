using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using TripArc.Case.Domain.Action.Enums;
using TripArc.Case.Domain.Case.Enums;
using TripArc.Case.Domain.FollowUp.Abstractions;
using TripArc.Case.Domain.FollowUp.DTO;
using TripArc.Case.Domain.FollowUp.Enums;
using TripArc.Common.Extensions;

namespace TripArc.Case.Data.FollowUp.Repositories;

public class FollowUpRepository : IFollowUpRepository
{
    private readonly CaseContext _dbContext;

    public FollowUpRepository(CaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Domain.FollowUp.DTO.FollowUp>> GetFollowUpsAsync(GetFollowUpRepositoryParameters parameters)
    {
        var queryActions = CreateQueryActions(parameters);
        var queryProfileActions = CreateQueryProfileActions(parameters);
        queryActions.ToList().AddRange(queryProfileActions);
        
        return await Task.FromResult(queryActions);
    }

    private IQueryable<Domain.FollowUp.DTO.FollowUp> CreateQueryActions(GetFollowUpRepositoryParameters parameters)
    {
        var caseRoles = new List<int> {(int) CaseRoleType.Agent, (int) CaseRoleType.Client};
        var travelerCaseRoles = new List<int>
        {
           (int) CaseRoleType.Traveler, 
           (int) CaseRoleType.Family, 
           (int) CaseRoleType.Assistant, 
           (int) CaseRoleType.Friend
        };
        
        var excludedStatus = new List<int> {(int) CaseStatus.Retired, (int) CaseStatus.Deleted};
        var excludedActionTypes = new List<int>
        {
            (int) ActionTypes.AirFirstQuote,
            (int) ActionTypes.AirReQuote,
            (int) ActionTypes.FirstQuote,
            (int) ActionTypes.Lead,
            (int) ActionTypes.CreateQuote
        };        
    
        var query = 
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
                .Where(cp => cp.CaseId == ca.CaseId && caseRoles.Contains(cp.CaseRole) && !cp.Deleted)
                .Select(cp => new {cp.ProfileId, cp.CaseRole})
                .FirstOrDefault()

            let profileIdFirstTraveler = _dbContext.CaseProfiles.AsNoTracking()
                .Where(cp => cp.CaseId == ca.CaseId && travelerCaseRoles.Contains(cp.CaseRole) && !cp.Deleted)
                .Select(cp => new {cp.ProfileId, cp.CaseRole})
                .FirstOrDefault()            
            
            let caseHasAnAgent = _dbContext.CaseProfiles.Any(x => 
                x.CaseId == ca.CaseId && 
                (x.CaseRole == (int)CaseRoleType.Agent || x.CaseRole == (int)CaseRoleType.SupportingAgent))
            
            where ca.IsPrimary
                  && !excludedStatus.Contains(cas.CaseStatus)
                  && !excludedActionTypes.Contains(a.ActionTypeId)
                  && a.FollowUp
                  && a.CompletedDate == null
                  && a.FollowUpDate <= DateTime.Now
                  && a.FollowUpDate > DateTime.Now.AddYears(-1)
                  && !a.FollowUpDeleted
                  && a.AssignedTo == parameters.ProfileId
            select new Domain.FollowUp.DTO.FollowUp
            {
                ClientName = string.Empty,
                ActionId = a.ActionId,
                CaseId = ca.CaseId,
                CaseName = Convert.ToString(cas.Name),
                CaseRole = profileIdCase != null 
                    ? (CaseRoleType)profileIdCase.CaseRole 
                    : (CaseRoleType)profileIdFirstTraveler.CaseRole,
                CaseStatus = (CaseStatus)cas.CaseStatus,
                ProfileId = profileIdCase != null ? profileIdCase.ProfileId : profileIdFirstTraveler.ProfileId,
                TripId = cas.TripId ?? 0,
                TripName = Convert.ToString(trips.TripName),			
                TripStartDate = trips.TripStartDate,
                FollowUpNotes = a.FollowUpNotes ?? a.Name,
                FollowUpTypeId = a.FollowUpTypeId.Value,
                FollowUpTypeDescription = ((FollowUpType)a.FollowUpTypeId.Value).GetDescription(),
                DueDate = a.FollowUpDate,
                DateCreated = a.DateCreated,
                HasAnAgent = caseHasAnAgent,
                Channel = string.Empty,
                Flagged = false,
                ItineraryQuoteId = itineraryQuoteId,
                MostRecentItinerary = string.Empty,			
                LastQuotedDate = null,
                LastQuotedPrice = 0.0
            };
        
        query = SetActionFilters(query, parameters);
        return query;
    }

    private IQueryable<Domain.FollowUp.DTO.FollowUp> CreateQueryProfileActions(GetFollowUpRepositoryParameters parameters)
    {
        var query = 
            from a in _dbContext.Actions
            where a.FollowUp
                  && !a.Deleted
                  && a.CompletedDate == null
                  && a.FollowUpDate <= DateTime.Now
                  && a.FollowUpDate > DateTime.Now.AddYears(-1) 
                  && !a.FollowUpDeleted
                  && a.AssignedTo == parameters.ProfileId
            join pc in _dbContext.ProfileActions on a.ActionId equals pc.ActionId
            select new Domain.FollowUp.DTO.FollowUp
            {
                ClientName = string.Empty,
                ActionId = a.ActionId,
                CaseId = 0,
                CaseName = string.Empty,
                CaseRole = CaseRoleType.None,
                CaseStatus = CaseStatus.Unassigned,
                ProfileId = 0,
                TripId = 0,
                TripName = string.Empty,
                TripStartDate = null,
                FollowUpNotes = a.FollowUpNotes ?? a.Name,
                FollowUpTypeId = a.FollowUpTypeId.Value,
                FollowUpTypeDescription = ((FollowUpType)a.FollowUpTypeId.Value).GetDescription(),
                DueDate = null,
                DateCreated = a.DateCreated, 
                HasAnAgent = false,
                Channel = string.Empty,
                Flagged = false,
                ItineraryQuoteId = 0,
                MostRecentItinerary = string.Empty,
                LastQuotedDate = null,
                LastQuotedPrice = 0.0
            };

        query = SetActionFilters(query, parameters);
        return query;
    }
    
    private IQueryable<Domain.FollowUp.DTO.FollowUp> SetActionFilters(
        IQueryable<Domain.FollowUp.DTO.FollowUp> query, GetFollowUpRepositoryParameters parameters)
    {
        query = query.Where(GetDueDateFilter(parameters));
        
        if (!parameters.CaseTitle.IsNullOrEmpty())
            query = query.Where(x => x.CaseName.Contains(parameters.CaseTitle));
        
        if (!parameters.NotesContent.IsNullOrEmpty())
            query = query.Where(x => x.FollowUpNotes.Contains(parameters.NotesContent));
        
        query = query.Where(GetStartDateFilter(parameters));
        query = query.Where(GetCreatedDateFilter(parameters));
        
        if (parameters.Flagged)
            query = query.Where(x => x.Flagged == true);
        
        if (parameters.CaseStatus.Any())
            query = query.Where(x => parameters.CaseStatus.Contains((int)x.CaseStatus));
        
        if (parameters.FollowUpType.Any())
            query = query.Where(x => parameters.FollowUpType.Contains((int)x.FollowUpTypeId));
        
        if (parameters.SortColumns.IsNotNullOrWhiteSpace())
            query = query.OrderBy(parameters.SortColumns);
        
        return query;
    }

    private Expression<Func<Domain.FollowUp.DTO.FollowUp, bool>> GetDueDateFilter(GetFollowUpRepositoryParameters parameters)
    {
        parameters.DueDateFrom ??= DateTime.Now.Date;
        parameters.DueDateTo ??= DateTime.Now.Date;
        
        Expression<Func<Domain.FollowUp.DTO.FollowUp, bool>> filter = x => 
            x.DueDate >= parameters.DueDateFrom.Value && x.DueDate <= parameters.DueDateTo.Value;
        return filter;
    }

    private Expression<Func<Domain.FollowUp.DTO.FollowUp, bool>> GetCreatedDateFilter(GetFollowUpRepositoryParameters parameters)
    {
        Expression<Func<Domain.FollowUp.DTO.FollowUp, bool>> filter = x => true;
        if (parameters.DateCreatedFrom.HasValue && parameters.DateCreatedTo.HasValue)
            filter = x => x.DateCreated >= parameters.DateCreatedFrom.Value && x.DateCreated <= parameters.DateCreatedTo.Value;
        return filter;
    }    

    private Expression<Func<Domain.FollowUp.DTO.FollowUp, bool>> GetStartDateFilter(GetFollowUpRepositoryParameters parameters)
    {
        Expression<Func<Domain.FollowUp.DTO.FollowUp, bool>> filter = x => true;
        if (parameters.StartDateFrom.HasValue && parameters.StartDateTo.HasValue)
            filter = x => x.TripStartDate >= parameters.StartDateFrom.Value && x.TripStartDate <= parameters.StartDateTo.Value;
        return filter;
    }
}