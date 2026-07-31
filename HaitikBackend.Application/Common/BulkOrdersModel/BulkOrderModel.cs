using HaitikBackend.Domain.ValueObjects;

namespace HaitikBackend.Application.Common.BulkOrdersModel;

public sealed record BulkOrderModel(GeoLocation DeliveryLocation, string CustomerPhoneNumber, int AgencyId);
