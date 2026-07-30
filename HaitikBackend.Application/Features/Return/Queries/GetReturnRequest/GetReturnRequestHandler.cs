using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaitikBackend.Application.Features.Return.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.Return.Queries.GetReturnRequest;

public class GetReturnRequestHandler : IRequestHandler<GetReturnRequestQuery, Result<ReturnRequest>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetReturnRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<ReturnRequest>> Handle(GetReturnRequestQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Returns.Query().Where(r => r.OrderId == request.OrderId);

        var item = await query
            .AsNoTracking()
            .ProjectTo<ReturnRequest>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (item is null)
            return Result<ReturnRequest>.Failed(ReturnErrors.ReturnRequestNotFound(request.OrderId));

        return Result<ReturnRequest>.Success(item);
    }
}
