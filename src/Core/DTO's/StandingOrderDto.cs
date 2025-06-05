using Core.Enums;

namespace Core.DTOs;

public class StandingOrderDto : PaymentDto
{
    public StandingOrderDto() => PaymentType = PaymentType.StandingOrder;
}
