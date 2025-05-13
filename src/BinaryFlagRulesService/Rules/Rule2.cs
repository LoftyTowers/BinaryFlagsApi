using Core.DTOs;
using Core.Enums;
using Core.Models;
using Microsoft.Extensions.Logging;

namespace Rules;

// Sample rule implementations
public class Rule2 : BaseFraudRule<Rule2>
{
    public Rule2(ILogger<Rule2> logger) : base(logger) {}
    public override FraudRuleFlags Flag => FraudRuleFlags.Rule2;

    public override RuleExecutionResult Execute(PaymentDto payment)
    {
        Logger.LogInformation("Executing Rule 2");

        if (payment.Amount > 200)
        {
            Logger.LogWarning("Rule 2 triggered: Amount exceeds 200");

            return new RuleExecutionResult
            {
                RuleName = nameof(Rule2),
                Passed = false,
                Message = "Amount exceeds £200"
            };
        }

        Logger.LogInformation("Rule 2 passed: Amount is within limit");

        return new RuleExecutionResult
        {
            RuleName = nameof(Rule2),
            Passed = true
        };
    }
}
