using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Application.Features.GovernmentEmployees.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.GovernmentEmployees.Queries.GetEmployeeDetails;

public class GetEmployeeDetailsHandler : IRequestHandler<GetEmployeeDetailsQuery, Result<EmployeeDetails>>
{
    private readonly IGovernmentEmployeeRepository _governmentEmployeeRepository;
    private readonly IMapper _mapper;

    public GetEmployeeDetailsHandler(IGovernmentEmployeeRepository governmentEmployeeRepository, IMapper mapper)
    {
        _governmentEmployeeRepository = governmentEmployeeRepository;
        _mapper = mapper;
    }

    public async Task<Result<EmployeeDetails>> Handle(GetEmployeeDetailsQuery request, CancellationToken cancellationToken)
    {
        var query = _governmentEmployeeRepository.Query();

        var employee = await query
            .AsNoTracking()
            .Where(e => e.Id == request.Id)
            .ProjectTo<EmployeeDetails>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (employee is null)
            return Result<EmployeeDetails>.Failed(UserErrors.UserNotFound(request.Id));

        return Result<EmployeeDetails>.Success(employee);
    }
}
