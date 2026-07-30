using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace HaitikBackend.Application.Features.Return.Commands.AcceptReturn;

public class AcceptReturnHandler : IRequestHandler<AcceptReturnCommand, Result>
{
    private readonly IUnitOfWork _unitOfwork;

    public AcceptReturnHandler(IUnitOfWork unitOfWork)
    {
        _unitOfwork = unitOfWork;
    }


    public async Task<Result> Handle(AcceptReturnCommand request, CancellationToken cancellationToken)
    {
        var @return = await _unitOfwork.Returns.GetByIdAsync(request.orderId, cancellationToken);


        if (@return is null)
            return Result.Failed(ReturnErrors.ReturnRequestNotFound(request.orderId));

        @return.AcceptReturn(request.userId);

        await _unitOfwork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
