using HaitikBackend.Application.Features.Users.Command.RegisterUser;
using HaitikBackend.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaitikBackend.Application.Features.Drivers.Commands.RegiesterDriver;

public sealed record RegisterDriverCommand(RegisterUserCommand UserData) : IRequest<Result<int>>;
