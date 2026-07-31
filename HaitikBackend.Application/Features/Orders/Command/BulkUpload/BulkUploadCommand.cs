using HaitikBackend.Application.Common.FileModels;
using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.Orders.Command.BulkUpload;

public sealed record BulkUploadCommand(FileUpload File) : IRequest<Result>;
