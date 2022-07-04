namespace TripArc.Case.Domain.ItineraryQuote.Entities;

public readonly record struct LatestItineraryQuote(
    int ItineraryQuoteId, 
    long ItineraryId, 
    DateTime? QuotedDate, 
    double? QuotedPrice,
    string MostRecentItinerary);
