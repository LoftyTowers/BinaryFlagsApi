using System.ComponentModel.DataAnnotations;

namespace Core.Enums;

public enum PaymentType
{
    [Display(Name = "Unknown")]
    Unknown = 0,

    [Display(Name = "Future Payment")]
    FuturePayemnt = 10,

    [Display(Name = "Immediate Payment")]
    ImediatePayment = 20,

    [Display(Name = "Standing Order")]
    StandingOrder = 30
}
