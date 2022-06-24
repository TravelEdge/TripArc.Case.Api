namespace TripArc.Case.Domain.Itinerary.Entities;

public record LatestItineraryQuote(int ItineraryQuoteId, long ItineraryId, DateTime? QuotedDate, double? QuotedPrice,
    string MostRecentItinerary);
