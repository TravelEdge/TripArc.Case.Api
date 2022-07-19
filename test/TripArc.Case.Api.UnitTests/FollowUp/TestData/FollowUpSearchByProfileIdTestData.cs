using System.Collections.Generic;
using Moq;
using TripArc.Case.Domain.ItineraryQuote.Entities;
using TripArc.Case.Shared.FollowUp.Queries;
using TripArc.Profile.Shared.Profile.Queries;

namespace TripArc.Case.Api.UnitTests.FollowUp.TestData;

public class FollowUpSearchByProfileIdTestData
{
    public int ProfileId { get; init; }
    public List<Domain.FollowUp.DTO.FollowUp> FollowUpRepositoryResult { get; init; } = new();
    public List<LatestItineraryQuote> ItineraryQuoteRepositoryResult { get; init; } = new();
    public List<FollowUpSearchByProfileIdResponse> HandlerSearchResponse { get; init; } = new();
    public List<ProfileGetFollowUpInfoResponse> ProfileClientApiResponse { get; init; } = new();
    public Times ExecutionTimes { get; init; } = Times.Never();
}