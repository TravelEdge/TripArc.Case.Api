namespace TripArc.Case.Domain.Action.Enums;

public enum ActionTypes
{
    [Description("Air - First Quote")]
    AirFirstQuote = 13,
    [Description("Air - Requote")]
    AirReQuote = 15,
    [Description("First Quote")]
    FirstQuote = 22,
    [Description("Lead")]
    Lead = 24,
    [Description("Create Quote")]
    CreateQuote = 36
}