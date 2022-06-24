using TripArc.Common.Abstractions.Repository;

namespace TripArc.Case.Domain.Case.Abstractions;

public interface IFollowUpRepository : IRepository
{
    Task<IEnumerable<Entities.FollowUp>> GetFollowUpsAsync(int profileId);
}