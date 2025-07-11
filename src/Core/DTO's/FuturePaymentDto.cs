using Core.Enums;

namespace Core.DTOs;

public class FuturePaymentDto : PaymentDto
{
    public FuturePaymentDto() => PaymentType = PaymentType.FuturePayment;
}
