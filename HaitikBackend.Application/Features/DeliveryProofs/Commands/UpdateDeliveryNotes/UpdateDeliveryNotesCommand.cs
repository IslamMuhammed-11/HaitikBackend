using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.DeliveryProofs.Commands.UpdateDeliveryNotes;

public sealed record UpdateDeliveryNotesCommand(int OrderId, string DeliveryNotes) : IRequest<Result>;
