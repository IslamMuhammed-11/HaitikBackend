using AutoMapper;
using HaitikBackend.Application.Features.Return.Queries.Responses;
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using MediatR;

namespace HaitikBackend.Application.Features.Return.Commands.RequestReturn;

public class RequestReturnHandler : IRequestHandler<RequestReturnCommand, Result<ReturnRequest>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public RequestReturnHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<ReturnRequest>> Handle(RequestReturnCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetOrderAndReturnRequestAsync(request.orderId, cancellationToken);

        if (order is null)
            return Result<ReturnRequest>.Failed(OrderErrors.OrderNotFound(request.orderId));

        var result = order.RequestToReturn(request.reason);

        if (!result.IsSuccess)
            return Result<ReturnRequest>.Failed(result.Error!);

        var @return = _mapper.Map<ReturnRequest>(result.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<ReturnRequest>.Success(@return);

    }
}
