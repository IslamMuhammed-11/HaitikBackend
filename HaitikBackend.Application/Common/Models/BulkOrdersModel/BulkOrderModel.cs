using HaitikBackend.Domain.ValueObjects;

namespace HaitikBackend.Application.Common.Models.BulkOrdersModel;

public sealed record BulkOrderModel(double Latitude , double Longitude, string CustomerPhoneNumber);
