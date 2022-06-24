using System.ComponentModel;

namespace TripArc.Case.Domain.Case.Enums;

public enum CaseStatus
{
    Unassigned = 0,
    Assigned = 1,
    Quoting = 10,
    Booked = 20,
    [Description("In Travel")]        
    InTravel = 30,
    Traveled = 40,
    Retired = 50,
    Deleted = 60,
    Cancelled = 99,
    [Description("Lad Before Time")]
    LandBeforeTime = 100        
}