using TripArc.Common.CQRS.Queries;

namespace TripArc.Case.Shared.Case.Queries;

public class CaseSearchByIdQuery : IQuery
{
    public int Id { get; set; }
}