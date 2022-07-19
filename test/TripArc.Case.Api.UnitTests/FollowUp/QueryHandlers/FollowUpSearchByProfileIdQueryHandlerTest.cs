using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TripArc.Case.Api.UnitTests.FollowUp.TestData;
using TripArc.Case.Domain.FollowUp.DTO;
using TripArc.Case.Domain.ItineraryQuote.Entities;
using TripArc.Case.Shared.FollowUp.Queries;
using TripArc.Profile.Shared.Profile.Queries;
using Xunit;

namespace TripArc.Case.Api.UnitTests.FollowUp.QueryHandlers;

public class FollowUpSearchByProfileIdQueryHandlerTest : FollowUpSearchByProfileIdQueryHandlerFixture
{
    [Fact]
    public async Task Should_Return_Null_For_When_An_Invalid_ProfileIs_Is_Requested()
    {
        var query = new FollowUpSearchByProfileIdQuery {ProfileId = -1};

        MockFollowUpRepository
            .Setup(repo => repo.GetFollowUpsAsync(It.IsAny<GetFollowUpRepositoryParameters>()))
            .ReturnsAsync(new List<Domain.FollowUp.DTO.FollowUp>());

        var queryHandlerResult = await Build_Query_Handler().ExecuteQueryAsync(query);

        MockFollowUpRepository.Verify(repo => repo.GetFollowUpsAsync(It.IsAny<GetFollowUpRepositoryParameters>()), Times.Once);
        queryHandlerResult.Should().BeNull();
    }

    [Theory]
    [MemberData(nameof(TestScenariosProvider))]
    public async Task Should_Execute_FollowUp_Search_Successfully_For_Provided_Scenarios(
        FollowUpSearchByProfileIdTestData testData)
    {
        var query = new FollowUpSearchByProfileIdQuery {ProfileId = testData.ProfileId};
        
        Configure_Mocks(testData, query);
        
        var queryHandlerResult = await Build_Query_Handler().ExecuteQueryAsync(query);

        Validate_Mock_Executions(testData, query);

        queryHandlerResult.Should().BeEquivalentTo(testData.HandlerSearchResponse);
    }

    public static IEnumerable<object[]> TestScenariosProvider()
    {
        yield return new object[]
        {
            new FollowUpSearchByProfileIdTestData
            {
                ProfileId = 101,
                FollowUpRepositoryResult = new List<Domain.FollowUp.DTO.FollowUp>
                {
                    new() { ClientName = "Client name test", ItineraryQuoteId = 1001 }
                },
                ItineraryQuoteRepositoryResult = new List<LatestItineraryQuote>
                {
                    new(1001, 2001, DateTime.Now.AddMonths(-5), 100.00, "Most recent itinerary quote")
                },
                HandlerSearchResponse = new List<FollowUpSearchByProfileIdResponse>
                {
                    new() { ClientName = It.IsAny<string>(), ProfileId = It.IsAny<int>() }
                },
                ProfileClientApiResponse = new List<ProfileGetFollowUpInfoResponse> { new() { ClientId = 3001 } },
                ExecutionTimes = Times.Once()
            }
        };
    }
}