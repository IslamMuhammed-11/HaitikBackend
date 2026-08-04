using Microsoft.AspNetCore.Mvc;

namespace HaitikBackend.API.Requests.Drivers;

public sealed record DeliveryProofRequest(IFormFile File, string ReciverName, string? deliveryNotes);
