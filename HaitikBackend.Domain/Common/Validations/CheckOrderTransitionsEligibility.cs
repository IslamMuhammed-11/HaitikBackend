using HaitikBackend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaitikBackend.Domain.Common.Validations;

internal static class CheckOrderTransitionsEligibility
{
    private static Dictionary<enOrderStatus, enOrderStatus> ValidTransitions = new()
    {
        {enOrderStatus.Pending , enOrderStatus.ReceivedPackage },
        {enOrderStatus.ReceivedPackage , enOrderStatus.Delivering },
        {enOrderStatus.Delivering , enOrderStatus.Delivered }
    };

    internal static bool Check(enOrderStatus lastStatus, enOrderStatus currentStatus)
    {

        return ValidTransitions.TryGetValue(lastStatus, out var nextStatus) && currentStatus == nextStatus;

    }
}
