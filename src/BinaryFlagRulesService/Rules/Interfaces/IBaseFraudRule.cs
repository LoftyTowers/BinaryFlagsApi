using Core.DTOs;
using Core.Enums;
using Core.Models;

namespace Rules;

public interface IBaseFraudRule
{
    FraudRuleFlags Flag { get; }
    RuleExecutionResult Execute(PaymentDto payment);
}
