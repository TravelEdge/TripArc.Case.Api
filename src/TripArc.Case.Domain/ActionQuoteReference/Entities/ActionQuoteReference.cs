namespace TripArc.Case.Domain.ActionQuoteReference.Entities;

public class ActionQuoteReference
{
    public int ActionQuoteReferenceId { get; set; }
    public int ActionId { get; set; }
    public int ItineraryQuoteId { get; set; }
    public bool Deleted { get; set; }
    public DateTimeOffset DateCreated { get; set; }
    public DateTimeOffset LastModified { get; set; }

    public Action.Entities.Action Action { get; set; }
    public ItineraryQuote.Entities.ItineraryQuote ItineraryQuote { get; set; }
}