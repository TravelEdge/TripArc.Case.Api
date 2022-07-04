using TripArc.Case.Shared.Case.InputModels;
using TripArc.Case.Shared.Case.Queries;

namespace TripArc.Case.Api.Case.Controllers;

[Authorize]
[ApiController]
[Route("api/v{version:apiVersion}/case")]
public class CaseController : BaseAPIController<CaseController>
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IMapper _mapper;

    public CaseController(ILogger<CaseController> logger, IBaseAPIResponse response, IQueryDispatcher queryDispatcher,
        IMapper mapper) : base(logger, response)
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
}