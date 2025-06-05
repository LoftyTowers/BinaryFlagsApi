using System;
using System.Collections.Generic;
using Core.DTOs;
using Core.Enums;

public static class PaymentDtoTypeMap
{
    private static readonly Dictionary<PaymentType, Type> Map = new()
    {
      [InlineData(PaymentType.ImmediatePayment, typeof(ImmediatePaymentDto))]
      [InlineData(PaymentType.FuturePayemnt, typeof(FuturePaymentDto))]
      [InlineData(PaymentType.StandingOrder, typeof(StandingOrderDto))]
    };

    public static Type? GetType(PaymentType paymentType)
    {
        Map.TryGetValue(paymentType, out var type);
        return type;
    }
}
