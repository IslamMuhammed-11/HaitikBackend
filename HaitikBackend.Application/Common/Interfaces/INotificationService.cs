using HaitikBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaitikBackend.Application.Common.Interfaces;

public interface INotificationService
{

    Task SendOrderOfferNotificationAsync(int driverId , int orderId , TimeSpan acceptanceWindow, CancellationToken ct = default);

}
