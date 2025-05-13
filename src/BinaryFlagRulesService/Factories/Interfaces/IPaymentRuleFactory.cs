// Engines/Interfaces/IPaymentRuleFactory.cs
using Core.DTOs;

namespace Factories;

public interface IPaymentRuleFactory
{
    PaymentDto AssignRules(PaymentDto payment);
}
