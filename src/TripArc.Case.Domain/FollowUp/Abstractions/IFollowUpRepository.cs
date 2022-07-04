namespace TripArc.Case.Domain.FollowUp.Abstractions;

public interface IFollowUpRepository : IRepository
{
    Task<IEnumerable<Entities.FollowUp>> GetFollowUpsAsync(int profileId);
}