using TripArc.Case.Domain.Case.Abstractions;
using TripArc.Case.Shared.Case.Queries;

namespace TripArc.Case.Api.Case.QueryHandlers;

public class CaseSearchByIdQueryHandler : IQueryHandler<CaseSearchByIdQuery, CaseSearchByIdResponse>
{
    private readonly ICaseRepository _caseRepository;
    private readonly IMapper _mapper; 

    public CaseSearchByIdQueryHandler(ICaseRepository caseRepository, IMapper mapper)
    {
        _caseRepository = caseRepository;
        _mapper = mapper;
    }
        
    public async Task<CaseSearchByIdResponse> ExecuteQueryAsync(CaseSearchByIdQuery query)
    {
        var @case = await _caseRepository.GetByIdAsync(query.Id);
        return _mapper.Map<CaseSearchByIdResponse>(@case);
    }
}