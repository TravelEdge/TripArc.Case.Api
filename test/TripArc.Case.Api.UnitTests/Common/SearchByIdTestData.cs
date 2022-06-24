﻿namespace TripArc.Case.Api.UnitTests.Common
{
    public class SearchByIdTestData<TRepositoryResult, THandlerResult> 
        where TRepositoryResult: class 
        where THandlerResult: class
    {
        public int Id { get; set; }
        public bool IncludeDeleted { get; init; } = false;
        public TRepositoryResult? RepositoryResult { get; init; }
        public THandlerResult? HandlerSearchResponse { get; init; }
    }
}