using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.Users.Queries.GetUserDetails;

public class GetUserDetailsHandler : IRequestHandler<GetUserDetailsQuery, Result<GetUserDetailsResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetUserDetailsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<GetUserDetailsResponse>> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
    {

        var query = _unitOfWork.Users.Query();

        var user = await query
                        .AsNoTracking()
                        .Where(e => e.Id == request.Id)
                        .ProjectTo<GetUserDetailsResponse>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
            return Result<GetUserDetailsResponse>.Failed(UserErrors.UserNotFound(request.Id));

        return Result<GetUserDetailsResponse>.Success(user);
    }
}
