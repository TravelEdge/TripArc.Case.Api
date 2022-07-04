using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using TripArc.Case.Api.FollowUp.QueryHandlers;
using TripArc.Case.Api.UnitTests.FollowUp.TestData;
using TripArc.Case.Domain.FollowUp.Abstractions;
using TripArc.Case.Domain.ItineraryQuote.Abstractions;
using TripArc.Case.Shared.FollowUp.Queries;
using TripArc.Common.CQRS.Queries;
using TripArc.Profile.Client.Profile;
using TripArc.Profile.Shared.Profile.InputModels.Queries;

namespace TripArc.Case.Api.UnitTests.FollowUp.QueryHandlers;

public abstract class FollowUpSearchByProfileIdQueryHandlerFixture
{
    private readonly Mock<ILogger<FollowUpSearchByProfileIdQueryHandler>> _loggerMock = new();
    protected readonly Mock<IMapper> MockMapper = new();
    protected readonly Mock<IProfileApiClient> MockProfileApiClient = new();
    protected readonly Mock<IFollowUpRepository> MockFollowUpRepository = new();
    protected readonly Mock<IItineraryQuoteRepository> MockItineraryQuoteRepository = new();

    protected IQueryHandler<FollowUpSearchByProfileIdQuery, IEnumerable<FollowUpSearchByProfileIdResponse>> Build_Query_Handler()
    {
        return new FollowUpSearchByProfileIdQueryHandler(
            _loggerMock.Object,
            MockMapper.Object,
            MockProfileApiClient.Object,
            MockFollowUpRepository.Object,
            MockItineraryQuoteRepository.Object);
    }
    
    protected void Configure_Mocks(FollowUpSearchByProfileIdTestData testData, FollowUpSearchByProfileIdQuery query)
    {
        MockFollowUpRepository
            .Setup(repo => repo.GetFollowUpsAsync(query.ProfileId))
            .ReturnsAsync(testData.FollowUpRepositoryResult);
        MockItineraryQuoteRepository
            .Setup(repo => repo.GetLatestItineraryQuotes(It.IsAny<List<int>>()))
            .ReturnsAsync(testData.ItineraryQuoteRepositoryResult);
        MockProfileApiClient.Setup(client => client.GetProfileFollowUpInfoAsync(It.IsAny<ProfileGetFollowUpInfoInputModel>()))
            .ReturnsAsync(testData.ProfileClientApiResponse);
        MockMapper.Setup(mapper =>
                mapper.Map<IEnumerable<FollowUpSearchByProfileIdResponse>>(It.IsAny<List<Domain.FollowUp.Entities.FollowUp>>()))
            .Returns(testData.HandlerSearchResponse);        
    }
    
    protected void Validate_Mock_Executions(FollowUpSearchByProfileIdTestData testData, FollowUpSearchByProfileIdQuery query)
    {
        MockFollowUpRepository.Verify(repo => repo.GetFollowUpsAsync(query.ProfileId), testData.ExecutionTimes);
        MockItineraryQuoteRepository.Verify(repo =>
            repo.GetLatestItineraryQuotes(It.IsAny<List<int>>()), testData.ExecutionTimes);
        MockProfileApiClient.Verify(client => client.GetProfileFollowUpInfoAsync(
            It.IsAny<ProfileGetFollowUpInfoInputModel>()), testData.ExecutionTimes);
        MockMapper.Verify(map => map.Map<IEnumerable<FollowUpSearchByProfileIdResponse>>(
            It.IsAny<List<Domain.FollowUp.Entities.FollowUp>>()), testData.ExecutionTimes);
    }    
}