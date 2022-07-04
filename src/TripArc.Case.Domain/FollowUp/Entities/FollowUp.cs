using TripArc.Case.Domain.Case.Enums;

namespace TripArc.Case.Domain.FollowUp.Entities;

public class FollowUp
{
    public string ClientName { get; set; }
    public int ActionId { get; set; }
    public int? CaseId { get; set; }
    public CaseRoleType CaseRole { get; set; }
    public int ProfileId { get; set; }
    public int? TripId { get; set; }
    public string TripName { get; set; }
    public string FollowUpNotes { get; set; }
    public DateTime? DueDate { get; set; }
    public bool HasAnAgent { get; set; }
    public string Channel { get; set; }
    public bool Flagged { get; set; }
    public string MostRecentItinerary { get; set; }
    public string CaseName { get; set; }
    public int ItineraryQuoteId {get; set; }
    public DateTime? TripStartDate { get; set; }
    public DateTime? LastQuotedDate { get; set; }
    public double? LastQuotedPrice { get; set; }
}