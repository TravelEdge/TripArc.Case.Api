using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TripArc.Case.Shared.Case.InputModels;
using TripArc.Case.Shared.Case.Queries;
using TripArc.Common.Base.Controller;
using TripArc.Common.Base.Response;
using TripArc.Common.CQRS.Queries;

namespace TripArc.Case.Api.Case.Controllers;

[Authorize]
[ApiController]
[Route("api/v{version:apiVersion}/case")]
public class CaseController : BaseAPIController<CaseController>
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IMapper _mapper;

    public CaseController(ILogger<CaseController> logger, IBaseAPIResponse response, IQueryDispatcher queryDispatcher,
        IMapper mapper)
        : base(logger, response)
    {
        _queryDispatcher = queryDispatcher;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task GetAsync(CaseSearchByIdInputModel model)
    {
        await ExecuteAsync(async () =>
        {
            var query = _mapper.Map<CaseSearchByIdQuery>(model);
            return await _queryDispatcher.ExecuteQueryAsync<CaseSearchByIdQuery, CaseSearchByIdResponse>(query);
        });
    }

    [HttpGet("followup/profile/{id}")]
    public async Task GetFollowUpsByIdAsync(FollowUpSearchByProfileIdInputModel model)
    {
        await ExecuteAsync(async () =>
        {
            var query = _mapper.Map<FollowUpSearchByProfileIdQuery>(model);
            return await _queryDispatcher
                .ExecuteQueryAsync<FollowUpSearchByProfileIdQuery, IEnumerable<FollowUpSearchByProfileIdResponse>>(query);
        });
    }
}