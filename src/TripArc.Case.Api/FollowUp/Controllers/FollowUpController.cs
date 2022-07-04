using TripArc.Case.Shared.FollowUp.InputModels;
using TripArc.Case.Shared.FollowUp.Queries;

namespace TripArc.Case.Api.FollowUp.Controllers;

[Authorize]
[ApiController]
[Route("api/v{version:apiVersion}/followup")]
public class FollowUpController : BaseAPIController<FollowUpController>
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IMapper _mapper;

    public FollowUpController(ILogger<FollowUpController> logger, IBaseAPIResponse response, IQueryDispatcher queryDispatcher,
        IMapper mapper) : base(logger, response)
    {
        _queryDispatcher = queryDispatcher;
        _mapper = mapper;
    }
    
    [HttpGet("profile/{id}")]
    public async Task GetFollowUpsByProfileIdAsync(FollowUpSearchByProfileIdInputModel model)
    {
        await ExecuteAsync(async () =>
        {
            var query = _mapper.Map<FollowUpSearchByProfileIdQuery>(model);
            return await _queryDispatcher
                .ExecuteQueryAsync<FollowUpSearchByProfileIdQuery, IEnumerable<FollowUpSearchByProfileIdResponse>>(query);
        });
    }    
}