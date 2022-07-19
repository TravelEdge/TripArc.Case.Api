

using TripArc.Case.Domain.FollowUp.Enums;

namespace TripArc.Case.Domain.Action.Entities;

public class Action : ISoftDeleteEntity
{
    public int ActionId { get; set; }
    public int? ParentActionId { get; set; }
    public int Department { get; set; }
    public int ActionTypeId { get; set; }
    public string Name { get; set; }
    public int CommunicationType { get; set; }
    public int? GTSentMailId { get; set; }
    public bool FollowUp { get; set; }
    public DateTime? FollowUpDate { get; set; }
    public int? AssignedTo { get; set; }
    public int? ActionedBy { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime LastModified { get; set; }
    public DateTime? CompletedDate { get; set; }
    public string FollowUpNotes { get; set; }
    public bool FollowUpDeleted { get; set; }
    public int? QuoteAgentActionId { get; set; }
    public int? FollowUpActionedBy { get; set; }
    public int? FollowUpTypeId { get; set; }
    public string FollowUpType { get; set; }
    public bool Deleted { get; set; }
    
    public ICollection<ActionQuoteReference.Entities.ActionQuoteReference> ActionQuoteReferences { get; set; }
    public ICollection<CaseAction.Entities.CaseAction> CaseActions { get; set; }    
}