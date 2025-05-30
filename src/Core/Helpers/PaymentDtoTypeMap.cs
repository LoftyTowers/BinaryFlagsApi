using System;
using System.Collections.Generic;
using Core.DTOs;
using Core.Enums;

public static class PaymentDtoTypeMap
{
    private static readonly Dictionary<PaymentType, Type> Map = new()
    {
        { PaymentType.ImediatePayment, typeof(ImmediatePaymentDto) },
        { PaymentType.FuturePayemnt, typeof(FuturePaymentDto) },
        { PaymentType.StandingOrder, typeof(StandardOrderDto) }
    };

    public static Type? GetType(PaymentType paymentType)
    {
        Map.TryGetValue(paymentType, out var type);
        return type;
    }
}
