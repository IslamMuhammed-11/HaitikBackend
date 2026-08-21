using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Application.Features.Roles.Queries.Responses;
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.Roles.Queries.GetRolesPage;

public class GetRolesPageHandler : IRequestHandler<GetRolesPageQuery, Result<RolesPageResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetRolesPageHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<RolesPageResponse>> Handle(GetRolesPageQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Roles.Query();

        int totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .AsNoTracking()
            .OrderBy(r => r.Id)
            .ProjectTo<RoleDetails>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var response = new RolesPageResponse(items, totalCount);

        return Result<RolesPageResponse>.Success(response);
    }
}
