using Core.DTOs;
using Core.Enums;
using Core.Models;
using Microsoft.Extensions.Logging;

namespace Rules;

// Sample rule implementations
public class Rule4 : BaseFraudRule<Rule4>
{

    public Rule4(ILogger<Rule4> logger) : base(logger) {}
    public override FraudRuleFlags Flag => FraudRuleFlags.Rule4;

    public override RuleExecutionResult Execute(PaymentDto payment)
    {
        Logger.LogInformation("Executing Rule 4");

        if (payment.Amount > 400)
        {
            Logger.LogWarning("Rule 4 triggered: Amount exceeds 400");

            return new RuleExecutionResult
            {
                RuleName = nameof(Rule4),
                Passed = false,
                Message = "Amount exceeds £400"
            };
        }

        Logger.LogInformation("Rule 4 passed: Amount is within limit");

        return new RuleExecutionResult
        {
            RuleName = nameof(Rule4),
            Passed = true
        };
    }
}
