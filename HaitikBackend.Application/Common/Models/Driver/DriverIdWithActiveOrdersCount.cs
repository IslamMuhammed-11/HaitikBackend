using HaitikBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaitikBackend.Application.Common.Models.Driver;

public sealed record DriverIdWithActiveOrdersCount(int DriverId, int ActiveOrdersCount);
