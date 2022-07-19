using TripArc.Case.Domain.FollowUp.DTO;

namespace TripArc.Case.Domain.FollowUp.Abstractions;

public interface IFollowUpRepository : IRepository
{
    Task<IEnumerable<DTO.FollowUp>> GetFollowUpsAsync(GetFollowUpRepositoryParameters parameters);
}