using System.Net.Http;
using System.Threading.Tasks;
using TripArc.Case.Shared.Case.InputModels;
using TripArc.Case.Shared.Case.Queries;
using TripArc.Common.Base.Service;

namespace TripArc.Case.Client.Case
{
    public class CaseApiClient : ICaseApiClient
    {
        private readonly IServiceClient _serviceClient;
        private const string BasePath = "/api/v1/case";

        public CaseApiClient(HttpClient httpClient)
        {
            _serviceClient = new ServiceClient(httpClient);
        }

        public async Task<CaseSearchByIdResponse> GetAsync(CaseSearchByIdInputModel model) =>
            await _serviceClient.GetAsync<CaseSearchByIdResponse>($"{BasePath}/{model.Id}").ConfigureAwait(false);
    }
}