using Core.DTOs;
using Core.Enums;
using Core.Models;
using Microsoft.Extensions.Logging;

namespace Rules;

// Sample rule implementations
public class Rule5 : BaseFraudRule<Rule5>
{

    public Rule5(ILogger<Rule5> logger) : base(logger) {}
    public override FraudRuleFlags Flag => FraudRuleFlags.Rule5;

    public override RuleExecutionResult Execute(PaymentDto payment)
    {
        Logger.LogInformation("Executing Rule 5");

        if (payment.Amount > 500)
        {
            Logger.LogWarning("Rule 5 triggered: Amount exceeds 500");

            return new RuleExecutionResult
            {
                RuleName = nameof(Rule5),
                Passed = false,
                Message = "Amount exceeds £500"
            };
        }

        Logger.LogInformation("Rule 5 passed: Amount is within limit");

        return new RuleExecutionResult
        {
            RuleName = nameof(Rule5),
            Passed = true
        };
    }
}
