using TripArc.Case.Domain.Case.Abstractions;
using Entities = TripArc.Case.Domain.Case.Entities;

namespace TripArc.Case.Data.Case.Repositories;

public class CaseRepository : Repository<Entities.Case>, ICaseRepository
{
    public CaseRepository(CaseContext dbContext) : base(dbContext)
    {
    }
}