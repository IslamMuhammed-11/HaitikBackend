using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace HaitikBackend.Application.Features.Return.Commands.RejectReturn;

public class RejectReturnHandler : IRequestHandler<RejectReturnCommand, Result>
{

    private readonly IUnitOfWork _unitOfWork;

    public RejectReturnHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RejectReturnCommand request, CancellationToken cancellationToken)
    {
        var @return = await _unitOfWork.Returns.GetByIdAsync(request.orderId, cancellationToken);


        if (@return is null)
            return Result.Failed(ReturnErrors.ReturnRequestNotFound(request.orderId));

        @return.RejectReturn(request.userId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
