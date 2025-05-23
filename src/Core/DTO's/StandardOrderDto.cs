using Core.Enums;

namespace Core.DTOs;

public class StandardOrderDto : PaymentDto
{
    public StandardOrderDto() => PaymentType = PaymentType.StandingOrder;
}
