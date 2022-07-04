using TripArc.Case.Domain.ItineraryQuote.Entities;

namespace TripArc.Case.Domain.ItineraryQuote.Abstractions;

public interface IItineraryQuoteRepository : IRepository<Entities.ItineraryQuote>
{
    Task<IEnumerable<LatestItineraryQuote>> GetLatestItineraryQuotes(List<int> itineraryQuoteIds);
}