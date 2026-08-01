using HaitikBackend.Application.Common.Models.FileModels;
using HaitikBackend.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaitikBackend.Application.Features.DeliveryProofs.Commands.ProofDelivery;

public sealed record ProofDeliveryCommand(int orderId,
    FileUpload file, string reciverName, string? deliveryNotes) : IRequest<Result>;
