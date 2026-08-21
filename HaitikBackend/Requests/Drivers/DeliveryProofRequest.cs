using Microsoft.AspNetCore.Mvc;

namespace HaitikBackend.API.Requests.Drivers;

public sealed record DeliveryProofRequest([FromBody]string ReciverName, [FromBody]string? deliveryNotes);
