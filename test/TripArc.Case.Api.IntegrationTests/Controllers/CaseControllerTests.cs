using System.Threading.Tasks;
using FluentAssertions;
using TripArc.Case.Api.IntegrationTests.Base;
using TripArc.Case.Shared.Case.InputModels;
using Xunit;

namespace TripArc.Case.Api.IntegrationTests.Controllers
{
    public class CaseControllerTests : BaseEndpointTests, IClassFixture<WebApplicationFactoryWithInMemory>
    {
        public CaseControllerTests(WebApplicationFactoryWithInMemory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Get_With_An_Invalid_Id_Should_Returns_Null()
        {
            var searchCaseResponse = await CaseApi.GetAsync(new CaseSearchByIdInputModel {Id = 9999999});

            searchCaseResponse.Should().BeNull();
        }
    }
}