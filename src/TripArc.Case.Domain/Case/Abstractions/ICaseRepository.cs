using System.Collections.Generic;
using System.Threading.Tasks;
using TripArc.Common.Abstractions.Repository;

namespace TripArc.Case.Domain.Case.Abstractions
{
    public interface ICaseRepository : IRepository<Entities.Case>
    {
        Task<IEnumerable<Entities.FollowUp>> GetFollowUps(int profileId);
    }
}