using TripArc.Common.Abstractions.Repository;

namespace TripArc.Case.Domain.CaseProfile.Abstractions;

// TODO: to remove?
public interface ICaseProfileRepository : IRepository<Entities.CaseProfile>
{
    Task<IEnumerable<Entities.LinkedCaseProfile>> GetCaseProfilesByCaseIdsAsync(List<int> cases);
}