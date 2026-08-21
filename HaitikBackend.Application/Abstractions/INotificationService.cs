using HaitikBackend.Application.Common.Models;
using HaitikBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaitikBackend.Application.Abstractions;

public interface INotificationService
{

    Task SendOrderOfferNotificationAsync(OrderNotificationModel model , TimeSpan acceptanceWindow, CancellationToken ct = default);

}
