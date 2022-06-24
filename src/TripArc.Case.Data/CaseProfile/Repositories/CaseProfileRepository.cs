using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TripArc.Case.Data.DataContext;
using TripArc.Case.Domain.CaseProfile.Abstractions;
using TripArc.Common.Storage.Repositories;
using Entities = TripArc.Case.Domain.CaseProfile.Entities;

namespace TripArc.Case.Data.CaseProfile.Repositories;

// TODO: to remove?
// public class CaseProfileRepository : Repository<Entities.CaseProfile>, ICaseProfileRepository
// {
//     public CaseProfileRepository(CaseContext dbContext) : base(dbContext)
//     {
//     }
//
//     public async Task<IEnumerable<Entities.LinkedCaseProfile>> GetCaseProfilesByCaseIdsAsync(List<int> cases)
//     {
//         return await Task.FromResult(Entity
//             .GroupBy(x => x.CaseId)
//             .Where(g => cases.Contains(g.Key))
//             .OrderBy(g => g.Key)
//             .Select(g => new Entities.LinkedCaseProfile(g.Key, g.First().ProfileId))
//             .AsNoTracking());
//     }
// }