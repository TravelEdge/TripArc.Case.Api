using System.ComponentModel;

namespace TripArc.Case.Domain.Case.Enums
{
    public enum CaseRoleType
    {
        [Description("Primary Agent")]
        Agent = 1,
        [Description("Client")]
        Client = 2,
        [Description("Traveler")]
        Traveler = 3,
        [Description("Destination Expert")]
        DestinationExpert = 4,
        [Description("Supporting Agent")]
        SupportingAgent = 11,
        [Description("Family")]
        Family = 101,
        [Description("Assistant")]
        Assistant = 102,
        [Description("Friend")]
        Friend = 103        
    }
}