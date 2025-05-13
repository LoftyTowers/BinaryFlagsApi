using Core.DTOs;
using Core.Enums;
using Core.Models;
using Microsoft.Extensions.Logging;

namespace Rules;

// Sample rule implementations
public class Rule8 : BaseFraudRule<Rule8>
{

    public Rule8(ILogger<Rule8> logger) : base(logger) {}
    public override FraudRuleFlags Flag => FraudRuleFlags.Rule8;

    public override RuleExecutionResult Execute(PaymentDto payment)
    {
        Logger.LogInformation("Executing Rule 8");

        if (payment.Amount > 800)
        {
            Logger.LogWarning("Rule 8 triggered: Amount exceeds 800");

            return new RuleExecutionResult
            {
                RuleName = nameof(Rule8),
                Passed = false,
                Message = "Amount exceeds £800"
            };
        }

        Logger.LogInformation("Rule 8 passed: Amount is within limit");

        return new RuleExecutionResult
        {
            RuleName = nameof(Rule8),
            Passed = true
        };
    }
}
