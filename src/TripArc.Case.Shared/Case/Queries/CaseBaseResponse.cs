namespace TripArc.Case.Shared.Case.Queries;

public class CaseBaseResponse
{
    public int CaseId { get; set; }
    public int? ParentCaseId { get; set; }
    public string CaseReference { get; set; }
    public string Name { get; set; }
    public int CaseType { get; set; }
    public int CaseStatus { get; set; }
    public int RequestedLocale { get; set; }
    public DateTimeOffset DepartureDate { get; set; }
    public DateTimeOffset DateCreated { get; set; }
    public DateTimeOffset LastModified { get; set; }
    public string TravelerPlaceholder { get; set; }
    public int? TripId { get; set; }
}