using Core.Enums;

namespace Core.DTOs;

public class ImmediatePaymentDto : PaymentDto
{
    public ImmediatePaymentDto() => PaymentType = PaymentType.ImmediatePayment;
}
