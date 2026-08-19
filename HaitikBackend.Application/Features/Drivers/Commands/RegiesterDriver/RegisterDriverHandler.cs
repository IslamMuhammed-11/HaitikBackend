using HaitikBackend.Application.Features.Drivers.Commands.AssignUserAsDriver;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace HaitikBackend.Application.Features.Drivers.Commands.RegiesterDriver;

public class RegisterDriverHandler : IRequestHandler<RegisterDriverCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public RegisterDriverHandler(IUnitOfWork unitOfWork, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<Result<int>> Handle(RegisterDriverCommand request, CancellationToken cancellationToken)
    {

        var userCmd = request.UserData with { RoleId = 2 };

        var userResullt = await _mediator.Send(userCmd);

        if (!userResullt.IsSuccess)
            return userResullt;

        var assignmentCMD = new AssignUserAsDriverCommand(userResullt.Value, null);

        var result = await _mediator.Send(assignmentCMD);

        return result;
    }
}
