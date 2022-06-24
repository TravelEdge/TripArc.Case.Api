using TripArc.Case.Domain.Itinerary.Entities;

namespace TripArc.Case.Domain.Action.Entities;

public class ActionQuoteReference
{
    public int ActionQuoteReferenceId { get; set; }
    public int ActionId { get; set; }
    public int ItineraryQuoteId { get; set; }
    public bool Deleted { get; set; }
    public DateTimeOffset DateCreated { get; set; }
    public DateTimeOffset LastModified { get; set; }

    public Actions Action { get; set; }
    public ItineraryQuote ItineraryQuote { get; set; }
}