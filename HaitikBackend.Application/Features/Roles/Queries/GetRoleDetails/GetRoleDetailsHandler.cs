using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Application.Features.Roles.Queries.Responses;
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.Roles.Queries.GetRoleDetails;

public class GetRoleDetailsHandler : IRequestHandler<GetRoleDetailsQuery, Result<RoleDetails>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetRoleDetailsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<RoleDetails>> Handle(GetRoleDetailsQuery request, CancellationToken cancellationToken)
    {
        var item = await _unitOfWork.Roles.Query()
            .AsNoTracking()
            .Where(r => r.Id == request.Id)
            .ProjectTo<RoleDetails>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (item is null)
            return Result<RoleDetails>.Failed(RoleErrors.RoleNotFound(request.Id));

        return Result<RoleDetails>.Success(item);
    }
}
