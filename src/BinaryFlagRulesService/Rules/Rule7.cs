using Core.DTOs;
using Core.Enums;
using Core.Models;
using Microsoft.Extensions.Logging;

namespace Rules;

// Sample rule implementations
public class Rule7 : BaseFraudRule<Rule7>
{
    public Rule7(ILogger<Rule7> logger) : base(logger) {}
    public override FraudRuleFlags Flag => FraudRuleFlags.Rule7;

    public override RuleExecutionResult Execute(PaymentDto payment)
    {
        Logger.LogInformation("Executing Rule 7");

        if (payment.Amount > 700)
        {
            Logger.LogWarning("Rule 7 triggered: Amount exceeds 700");

            return new RuleExecutionResult
            {
                RuleName = nameof(Rule7),
                Passed = false,
                Message = "Amount exceeds £700"
            };
        }

        Logger.LogInformation("Rule 7 passed: Amount is within limit");

        return new RuleExecutionResult
        {
            RuleName = nameof(Rule7),
            Passed = true
        };
    }
}
