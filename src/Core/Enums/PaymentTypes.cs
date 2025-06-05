using System.ComponentModel.DataAnnotations;

namespace Core.Enums;

public enum PaymentType
{
    [Display(Name = "Unknown")]
    Unknown = 0,

    [Display(Name = "Future Payment")]
    FuturePayment = 10,

    [Display(Name = "Immediate Payment")]
    ImmediatePayment = 20,

    [Display(Name = "Standing Order")]
    StandingOrder = 30
}
