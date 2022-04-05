using TripArc.Case.Api.IntegrationTests.CustomStartup;
using TripArc.Case.Client.Case;
using TripArc.Common.Test.IntegrationTest.Base;
using TripArc.Common.Test.IntegrationTest.Extensions;

namespace TripArc.Case.Api.IntegrationTests.Base
{
    public abstract class BaseEndpointTests
    {
        protected ICaseApiClient CaseApi { get; }
        protected BaseEndpointTests(BaseWebApplicationFactory<TestStartup> factory)
        {
            var client = factory.CreateClientWithFakeAuthentication();
            CaseApi = new CaseApiClient(client);
        }
    }
}