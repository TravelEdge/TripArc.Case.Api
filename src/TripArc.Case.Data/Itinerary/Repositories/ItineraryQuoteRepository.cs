using TripArc.Case.Domain.ItineraryQuote.Abstractions;
using TripArc.Case.Domain.ItineraryQuote.Entities;

namespace TripArc.Case.Data.Itinerary.Repositories;

public class ItineraryQuoteRepository : Repository<ItineraryQuote>, IItineraryQuoteRepository
{
    private readonly CaseContext _dbContext;

    public ItineraryQuoteRepository(CaseContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<LatestItineraryQuote>> GetLatestItineraryQuotes(List<int> itineraryQuoteIds)
    {
        var query =
            from iq in _dbContext.ItineraryQuotes
                .AsNoTracking()
                .Where(x => itineraryQuoteIds.Contains(x.ItineraryQuoteId) && !x.Deleted)
            from it in _dbContext.Itineraries
                .AsNoTracking()
                .Where(x => x.ItineraryId == iq.ItineraryId).DefaultIfEmpty()
            select new LatestItineraryQuote
            (
                iq.ItineraryQuoteId,
                iq.ItineraryId,
                iq.QuoteDate,
                iq.QuotedPrice,
                it.Name
            );

        return await Task.FromResult(query);
    }
}