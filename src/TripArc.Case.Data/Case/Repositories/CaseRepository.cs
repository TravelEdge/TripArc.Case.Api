using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TripArc.Case.Data.DataContext;
using TripArc.Case.Domain.Action.Enums;
using TripArc.Case.Domain.Case.Abstractions;
using TripArc.Case.Domain.Case.Enums;
using TripArc.Common.Storage.Repositories;
using Entities = TripArc.Case.Domain.Case.Entities;

namespace TripArc.Case.Data.Case.Repositories
{
    public class CaseRepository : Repository<Entities.Case>, ICaseRepository
    {
        private readonly CaseContext _dbContext;
        
        public CaseRepository(CaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Entities.FollowUp>> GetFollowUps(int profileId)
        {
            var excludedStatus = new List<int> {(int)CaseStatus.Retired, (int)CaseStatus.Deleted};
            var excludedActionTypes = new List<int>
            {
                (int)ActionTypes.AirFirstQuote, 
                (int)ActionTypes.AirReQuote, 
                (int)ActionTypes.FirstQuote, 
                (int)ActionTypes.Lead,
                (int)ActionTypes.CreateQuote
            };
            
            var query =
                from a in _dbContext.Actions		
                join ca in _dbContext.CaseActions on a.ActionId equals ca.ActionId		
                join cas in _dbContext.Cases on ca.CaseId  equals cas.CaseId
                //from cp  in CaseProfiles.Where(x => x.CaseId == ca.CaseId && x.CaseRole == 4 && !x.Deleted).DefaultIfEmpty()
		
                join cp  in _dbContext.CaseProfiles on ca.CaseId equals cp.CaseId into tmpCaseProfiles
                from caseProfiles in tmpCaseProfiles.Where(x => x.CaseRole == (int)CaseRoleType.DestinationExpert).DefaultIfEmpty()

                join trip in _dbContext.Trips on cas.TripId equals trip.TripId into tmpTrips
                from trips in tmpTrips.DefaultIfEmpty()                
		
                // join p   in Profiles     on a.AssignedTo equals p.ProfileId
		
                //from p in Profiles where p.ProfileId == (a.AssignedTo != null ? a.AssignedTo : caseProfiles.ProfileId)
                where ca.IsPrimary
                      && !excludedStatus.Contains(cas.CaseStatus)
                      && !excludedActionTypes.Contains(a.ActionTypeId)
                      && a.FollowUp
                      && a.CompletedDate == null
                      && a.FollowUpDate <= DateTime.Now
                      && a.FollowUpDate > DateTime.Now.AddYears(-1) 
                      && !a.FollowUpDeleted
                      && a.AssignedTo == profileId
                orderby a.ActionId
                select new Entities.FollowUp
                {
                    ClientName = string.Empty, // todo: pull it from Profile microservices?
                    ActionId = a.ActionId,
                    CaseId = ca.CaseId,
                    TripId = cas.TripId,
                    FollowUpNotes = a.FollowUpNotes,
                    DueDate = DateTime.Now, // todo
                    Channel = string.Empty, // todo
                    Flagged = false, // todo
                    MostRecentItinerary = string.Empty, // todo
                    CaseName = cas.Name,
                    TripStartDate = trips.TripStartDate,
                    LastQuoteDate = DateTime.Now, // todo
                    LastTripPrice = 0.0m // todo
                };

            //var result = await query.ToListAsync();

            return await query.ToListAsync();
        }

        // public Task GetCaseClient(int caseId)
        // {
        //     var query = from cp in Entities.CaseProfiles
        //         from ppc in Profiles.Where(ppc => cp.ProfileId == ppc.ProfileId).DefaultIfEmpty()
        //         from pc in ProfileClient.Where(pc => ppc.ProfileId == pc.ProfileId).DefaultIfEmpty()
        //         from ppa in Profiles.Where(ppa => cp.ProfileId == ppa.ProfileId).DefaultIfEmpty()
        //         from pa in ProfileAgent.Where(pa => ppa.ProfileId == pa.ProfileId).DefaultIfEmpty()
        //
        //         from c2t in Client2TourAgent.Where(c2t => pa.TourAgentId == c2t.TourAgentId).DefaultIfEmpty()
        //
        //         where roles.Contains(cp.CaseRole) && !cp.Deleted && cp.CaseId == 930895
        //         select new
        //         {
        //             ClientId = pa.ProfileId == 0 ? pc.TmtClientId : c2t.ClientId,
        //             GivenName = pa.ProfileId == 0 ? ppc.GivenName : ppa.GivenName,
        //             FamilyName = pa.ProfileId == 0 ? ppc.FamilyName : ppa.FamilyName,
        //             Email = pa.ProfileId == 0 ? ppc.Email : ppa.Email,
        //             ClientProfileId = pa.ProfileId == 0 ? pc.ProfileId : pa.ProfileId
        //         };
        // }
    }
}