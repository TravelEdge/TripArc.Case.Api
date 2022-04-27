using System;
using TripArc.Common.CQRS.Queries;

namespace TripArc.Case.Shared.Case.Queries
{
    public class FollowUpSearchByProfileIdQuery : IQuery
    {
        public int ProfileId { get; set; }
        public DateTime DueDate { get; set; }
        public string CaseStatus { get; set; }
        public string ClientName { get; set; }
        public DateTime TravelDate { get; set; }
        public string FollowUpType { get; set; }
        public bool QuoteExpired { get; set; }
        public bool FlaggedFollowUps { get; set; }
        public string Channel { get; set; }
        public string CaseName { get; set; }
        public bool CompletedFollowUps { get; set; }
        public string SearchByKeyWord { get; set; }
        public string SortColumns { get; set; }        
    }
}