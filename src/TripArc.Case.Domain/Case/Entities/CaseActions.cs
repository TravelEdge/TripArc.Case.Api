using TripArc.Case.Domain.Action.Entities;
using TripArc.Common.Abstractions.Entity;

namespace TripArc.Case.Domain.Case.Entities;

public class CaseActions : ISoftDeleteEntity
{
    public int CaseActionId { get; set; }
    public int CaseId { get; set; }
    public int ActionId { get; set; }
    public bool IsPrimary { get; set; }
    public bool Deleted { get; set; }
    
    public virtual Actions Action { get; set; }
    public virtual Case Case { get; set; }    
}