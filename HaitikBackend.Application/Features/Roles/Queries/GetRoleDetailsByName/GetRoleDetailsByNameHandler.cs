using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Application.Features.Roles.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.Roles.Queries.GetRoleDetailsByName;

public class GetRoleDetailsByNameHandler : IRequestHandler<GetRoleDetailsByNameQuery, Result<RoleDetails>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetRoleDetailsByNameHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<RoleDetails>> Handle(GetRoleDetailsByNameQuery request, CancellationToken cancellationToken)
    {
        var item = await _unitOfWork.Roles.Query()
            .AsNoTracking()
            .Where(r => r.Name == request.Name)
            .ProjectTo<RoleDetails>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (item is null)
            return Result<RoleDetails>.Failed(RoleErrors.RoleNotFound(request.Name));

        return Result<RoleDetails>.Success(item);
    }
}
