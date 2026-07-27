using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Application.Features.GovernmentEmployees.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.GovernmentEmployees.Queries.GetEmployeesPage;

public class GetEmployeesPageHandler : IRequestHandler<GetEmployeesPageQuery, Result<EmployeesPageResponse>>
{
    private readonly IGovernmentEmployeeRepository _governmentEmployeeRepository;
    private readonly IMapper _mapper;

    public GetEmployeesPageHandler(IGovernmentEmployeeRepository governmentEmployeeRepository, IMapper mapper)
    {
        _governmentEmployeeRepository = governmentEmployeeRepository;
        _mapper = mapper;
    }

    public async Task<Result<EmployeesPageResponse>> Handle(GetEmployeesPageQuery request, CancellationToken cancellationToken)
    {
        var skip = (request.PageNumber - 1) * request.PageSize;

        var query = _governmentEmployeeRepository.Query();

        int totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .AsNoTracking()
            .OrderByDescending(e => e.Id)
            .ProjectTo<EmployeeDetails>(_mapper.ConfigurationProvider)
            .Skip(skip)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var response = new EmployeesPageResponse(items, request.PageSize, request.PageNumber, totalCount);

        return Result<EmployeesPageResponse>.Success(response);
    }
}
