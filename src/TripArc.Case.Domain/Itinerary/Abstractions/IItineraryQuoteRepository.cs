using TripArc.Case.Domain.Itinerary.Entities;
using TripArc.Common.Abstractions.Repository;

namespace TripArc.Case.Domain.Itinerary.Abstractions;

public interface IItineraryQuoteRepository : IRepository<ItineraryQuote>
{
    Task<IEnumerable<LatestItineraryQuote>> GetLatestItineraryQuotes(IEnumerable<int> itineraryQuoteIds);
}