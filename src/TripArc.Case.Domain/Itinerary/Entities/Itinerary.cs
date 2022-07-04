namespace TripArc.Case.Domain.Itinerary.Entities;

public class Itinerary : ISoftDeleteEntity
{
    public long ItineraryId { get; set; }
    public string Name { get; set; }
    public bool Deleted { get; set; }
    
    public ItineraryQuote.Entities.ItineraryQuote ItineraryQuote { get; set; }
}