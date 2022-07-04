namespace TripArc.Case.Domain.CaseAction.Entities;

public class CaseAction : ISoftDeleteEntity
{
    public int CaseActionId { get; set; }
    public int CaseId { get; set; }
    public int ActionId { get; set; }
    public bool IsPrimary { get; set; }
    public bool Deleted { get; set; }
    
    public Action.Entities.Action Action { get; set; }
    public Case.Entities.Case Case { get; set; }    
}