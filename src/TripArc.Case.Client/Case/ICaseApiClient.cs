using System.Threading.Tasks;
using TripArc.Case.Shared.Case.InputModels;
using TripArc.Case.Shared.Case.Queries;

namespace TripArc.Case.Client.Case
{
    public interface ICaseApiClient
    {
        Task<CaseSearchByIdResponse> GetAsync(CaseSearchByIdInputModel model);
    }
}