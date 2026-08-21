using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.Orders.Command.AssignDriver;

public class AssignDriverHandler : IRequestHandler<AssignDriverCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public AssignDriverHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AssignDriverCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId, cancellationToken);

        if (order is null)
            return Result.Failed(OrderErrors.OrderNotFound(request.OrderId));

        var result = order.AssignDriver(request.DriverId);

        if (!result.IsSuccess)
            return result;

        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            return Result.Failed(OrderErrors.ConcurrecyConflict);
        }


        return Result.Success();
    }
}
