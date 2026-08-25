using HaitikBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaitikBackend.Application.Common.Models.BulkOrdersModel;

public sealed record BulkUploadResult(List<BulkOrderModel> orders, int rejectedRows);
