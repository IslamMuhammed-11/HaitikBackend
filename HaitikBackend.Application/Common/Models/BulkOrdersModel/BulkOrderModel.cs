using HaitikBackend.Domain.ValueObjects;

namespace HaitikBackend.Application.Common.Models.BulkOrdersModel;

public sealed record BulkOrderModel(GeoLocation DeliveryLocation, string CustomerPhoneNumber, int AgencyId);
