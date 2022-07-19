namespace TripArc.Case.Domain.FollowUp.Enums;

public enum FollowUpType
{
    [Description("None")]
    None = 0,    
    [Description("To Do")]
    Todo = 1,
    [Description("Pre Sale")]
    PreSale = 2,
    [Description("Post Sale")]
    PostSale = 3,
    [Description("Pre Sale: Follow Up 1")]
    PreSaleFollowUp1 = 4,
    [Description("Pre Sale: Follow Up 2")]
    PreSaleFollowUp2 = 5,
    [Description("Pre Sale: Follow Up 3")]
    PreSaleFollowUp3 = 6,
    [Description("Pre Sale: Follow Up 4")]
    PreSaleFollowUp4 = 7
}