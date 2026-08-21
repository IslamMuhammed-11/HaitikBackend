using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.Users.Queries.GetUsersPage;

public class GetUsersPageHandler : IRequestHandler<GetUsersPageQuery, Result<GetUsersPageResponse>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUsersPageHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<GetUsersPageResponse>> Handle(GetUsersPageQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Users.Query();

        int skip = (request.pageNumber - 1) * request.pageSize;

        int totalCount = await query.CountAsync();

        List<UserDetails> page = await query
            .AsNoTracking()
            .ProjectTo<UserDetails>(_mapper.ConfigurationProvider)
            .Skip(skip)
            .Take(request.pageSize)
            .ToListAsync(cancellationToken);

        var response = new GetUsersPageResponse(page, request.pageSize, request.pageNumber, totalCount);

        return Result<GetUsersPageResponse>.Success(response);
    }
}
