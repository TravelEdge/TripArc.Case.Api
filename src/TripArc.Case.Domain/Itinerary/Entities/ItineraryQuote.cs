using TripArc.Case.Domain.Action.Entities;

namespace TripArc.Case.Domain.Itinerary.Entities;

public class ItineraryQuote //: ISoftDeleteEntity
{
    public ItineraryQuote()
    {
        ActionQuoteReferences = new HashSet<ActionQuoteReference>();
    }
    
    public int ItineraryQuoteId { get; set; }
    public long ItineraryId { get; set; }
    public DateTime? QuoteDate { get; set; }
    public double? QuotedPrice { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? LastModified { get; set; }
    
    public bool Deleted { get; set; }
    public virtual Itinerary Itinerary { get; set; }
    public virtual ICollection<ActionQuoteReference> ActionQuoteReferences { get; set; }
}