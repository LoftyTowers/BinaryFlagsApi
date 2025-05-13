using Core.DTOs;
using Core.Enums;
using Core.Models;
using Microsoft.Extensions.Logging;

namespace Rules;

// Sample rule implementations
public class Rule3 : BaseFraudRule<Rule3>
{

    public Rule3(ILogger<Rule3> logger) : base(logger) { }
    public override FraudRuleFlags Flag => FraudRuleFlags.Rule3;

    public override RuleExecutionResult Execute(PaymentDto payment)
    {
        Logger.LogInformation("Executing Rule 3");

        if (payment.Amount > 300)
        {
            Logger.LogWarning("Rule 3 triggered: Amount exceeds 300");

            return new RuleExecutionResult
            {
                RuleName = nameof(Rule3),
                Passed = false,
                Message = "Amount exceeds £300"
            };
        }

        Logger.LogInformation("Rule 3 passed: Amount is within limit");

        return new RuleExecutionResult
        {
            RuleName = nameof(Rule3),
            Passed = true
        };
    }
}
