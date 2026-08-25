using AutoMapper;
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using MediatR;

namespace HaitikBackend.Application.Features.Return.Commands.RequestReturn;

public class RequestReturnHandler : IRequestHandler<RequestReturnCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public RequestReturnHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(RequestReturnCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetOrderAndReturnRequestAsync(request.orderId, cancellationToken);

        if (order is null)
            return Result<int>.Failed(OrderErrors.OrderNotFound(request.orderId));

        var result = order.RequestToReturn(request.reason);

        if (!result.IsSuccess)
            return Result<int>.Failed(result.Error!);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(request.orderId);

    }
}
