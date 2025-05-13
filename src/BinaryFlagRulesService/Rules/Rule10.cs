using Core.DTOs;
using Core.Enums;
using Core.Models;
using Microsoft.Extensions.Logging;

namespace Rules;

// Sample rule implementations
public class Rule10 : BaseFraudRule<Rule10>
{
    public Rule10(ILogger<Rule10> logger) : base(logger) {}
    public override FraudRuleFlags Flag => FraudRuleFlags.Rule10;

    public override RuleExecutionResult Execute(PaymentDto payment)
    {
        Logger.LogInformation("Executing Rule 10");

        if (payment.Amount > 1000)
        {
            Logger.LogWarning("Rule 10 triggered: Amount exceeds 1000");

            return new RuleExecutionResult
            {
                RuleName = nameof(Rule10),
                Passed = false,
                Message = "Amount exceeds £1000"
            };
        }

        Logger.LogInformation("Rule 10 passed: Amount is within limit");

        return new RuleExecutionResult
        {
            RuleName = nameof(Rule10),
            Passed = true
        };
    }
}
