using System;

namespace TripArc.Case.Domain.Case.Entities
{
    public class FollowUp
    {
        public string ClientName { get; set; }
        public int ActionId { get; set; }
        public int CaseId { get; set; }
        public int? TripId { get; set; }
        public string FollowUpNotes { get; set; }
        public DateTime DueDate { get; set; }
        public string Channel { get; set; }
        public bool Flagged { get; set; }
        public string MostRecentItinerary { get; set; }
        public string CaseName { get; set; }
        public DateTime TripStartDate { get; set; }
        public DateTime LastQuoteDate { get; set; }
        public decimal LastTripPrice { get; set; }
    }
}