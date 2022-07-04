using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using TripArc.Case.Api.Case.QueryHandlers;
using TripArc.Case.Api.UnitTests.Case.TestData;
using TripArc.Case.Domain.Case.Abstractions;
using TripArc.Case.Shared.Case.Queries;
using Xunit;
using Entities = TripArc.Case.Domain.Case.Entities;

namespace TripArc.Case.Api.UnitTests.Case.QueryHandlers
{
    public class CaseSearchByIdQueryHandlerTest
    {
        private readonly Mock<ICaseRepository> _caseRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public CaseSearchByIdQueryHandlerTest()
        {
            _caseRepositoryMock = new Mock<ICaseRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        [Theory]
        [MemberData(nameof(TestScenariosProvider))]        
        public async Task Should_Execute_Case_Query_Successfully_For_Provided_Scenarios(
            SearchByIdTestData<Entities.Case, CaseSearchByIdResponse> testData)
        {
            var query = new CaseSearchByIdQuery { Id = testData.Id };

            _caseRepositoryMock
                .Setup(repo => repo.GetByIdAsync(testData.Id, testData.IncludeDeleted))
                .ReturnsAsync(testData.RepositoryResult);

            if (testData.HandlerSearchResponse != null)
            {
                _mapperMock.Setup(mapper => mapper.Map<CaseSearchByIdResponse>(testData.RepositoryResult))
                    .Returns(testData.HandlerSearchResponse);

                var handler = new CaseSearchByIdQueryHandler(_caseRepositoryMock.Object, _mapperMock.Object);

                var queryHandlerResult = await handler.ExecuteQueryAsync(query);

                _caseRepositoryMock.Verify(x => x.GetByIdAsync(testData.Id, testData.IncludeDeleted), Times.Once);
                _mapperMock.Verify(x => x.Map<CaseSearchByIdResponse>(testData.RepositoryResult), Times.Once);
                queryHandlerResult.Should().BeEquivalentTo(testData.HandlerSearchResponse);
            }            
        }
        
        public static IEnumerable<object[]> TestScenariosProvider()
        {
            yield return new object[] { new SearchByIdTestData<Entities.Case, CaseSearchByIdResponse>() };
            yield return new object[]
            {
                new SearchByIdTestData<Entities.Case, CaseSearchByIdResponse>
                {
                    Id = 321,
                    RepositoryResult = new Entities.Case {CaseId = It.IsAny<int>()},
                    HandlerSearchResponse = new CaseSearchByIdResponse { CaseId = 321 }
                }
            };
        }        
    }
}