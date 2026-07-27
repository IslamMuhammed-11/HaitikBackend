using HaitikBackend.Application.Features.GovernmentEmployees.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.GovernmentEmployees.Queries.GetEmployeeDetails;

public sealed record GetEmployeeDetailsQuery(int Id) : IRequest<Result<EmployeeDetails>>;
