using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.Users.Queries.GetUsersPage;

public class GetUsersPageHandler : IRequestHandler<GetUsersPageQuery, Result<GetUsersPageResponse>>
{

    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUsersPageHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetUsersPageResponse>> Handle(GetUsersPageQuery request, CancellationToken cancellationToken)
    {
        var query = _userRepository.Query();

        int skip = (request.pageNumber - 1) * request.pageSize;

        List<UserDetails> page = await query
            .AsNoTracking()
            .ProjectTo<UserDetails>(_mapper.ConfigurationProvider)
            .Skip(skip)
            .Take(request.pageSize)
            .ToListAsync(cancellationToken);

        var response = new GetUsersPageResponse(page, request.pageSize, request.pageNumber);

        return Result<GetUsersPageResponse>.Success(response);
    }
}
