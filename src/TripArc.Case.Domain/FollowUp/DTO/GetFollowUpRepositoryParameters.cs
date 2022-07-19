namespace TripArc.Case.Domain.FollowUp.DTO;

public class GetFollowUpRepositoryParameters
{
    public int ProfileId { get; set; }
    public DateTime? DueDateFrom { get; set; }
    public DateTime? DueDateTo { get; set; }
    public string ClientName { get; set; }
    public string CaseTitle { get; set; }
    public string NotesContent { get; set; }
    public DateTime? DateCreatedFrom { get; set; }
    public DateTime? DateCreatedTo { get; set; }
    public DateTime? StartDateFrom { get; set; }
    public DateTime? StartDateTo { get; set; }
    public List<string> Channel { get; set; }
    public List<int> CaseStatus { get; set; }
    public List<int> FollowUpType { get; set; }
    public bool Flagged { get; set; }
    public string SortColumns { get; set; }    
}