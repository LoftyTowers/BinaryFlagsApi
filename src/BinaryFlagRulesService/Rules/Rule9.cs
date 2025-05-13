using Core.DTOs;
using Core.Enums;
using Core.Models;
using Microsoft.Extensions.Logging;

namespace Rules;

// Sample rule implementations
public class Rule9 : BaseFraudRule<Rule9>
{
    public Rule9(ILogger<Rule9> logger) : base(logger) {}
    public override FraudRuleFlags Flag => FraudRuleFlags.Rule9;

    public override RuleExecutionResult Execute(PaymentDto payment)
    {
        Logger.LogInformation("Executing Rule 9");

        if (payment.Amount > 900)
        {
            Logger.LogWarning("Rule 9 triggered: Amount exceeds 900");

            return new RuleExecutionResult
            {
                RuleName = nameof(Rule9),
                Passed = false,
                Message = "Amount exceeds £900"
            };
        }

        Logger.LogInformation("Rule 9 passed: Amount is within limit");

        return new RuleExecutionResult
        {
            RuleName = nameof(Rule9),
            Passed = true
        };
    }
}
