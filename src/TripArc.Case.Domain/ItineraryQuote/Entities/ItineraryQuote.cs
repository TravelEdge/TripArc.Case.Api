namespace TripArc.Case.Domain.ItineraryQuote.Entities;

public class ItineraryQuote : ISoftDeleteEntity
{
    public int ItineraryQuoteId { get; set; }
    public long ItineraryId { get; set; }
    public DateTime? QuoteDate { get; set; }
    public double? QuotedPrice { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? LastModified { get; set; }
    
    public bool Deleted { get; set; }
    public Itinerary.Entities.Itinerary Itinerary { get; set; }
    public ICollection<ActionQuoteReference.Entities.ActionQuoteReference> ActionQuoteReferences { get; set; }
}