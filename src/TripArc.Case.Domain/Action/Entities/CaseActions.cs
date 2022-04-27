using TripArc.Common.Abstractions.Entity;

namespace TripArc.Case.Domain.Action.Entities
{
    public class CaseActions : ISoftDeleteEntity
    {
        public int CaseActionId { get; set; }
        public int CaseId { get; set; }
        public int ActionId { get; set; }
        public bool IsPrimary { get; set; }
        public bool Deleted { get; set; }
    }
}