using Core.DTOs;
using Core.Enums;
using Core.Models;
using Microsoft.Extensions.Logging;

namespace Rules;

// Sample rule implementations
public class Rule1 : BaseFraudRule<Rule1>
{
    public Rule1(ILogger<Rule1> logger) : base(logger) {}

    public override FraudRuleFlags Flag => FraudRuleFlags.Rule1;

    public override RuleExecutionResult Execute(PaymentDto payment)
    {
        Logger.LogInformation("Executing Rule 1");

        if (payment.Amount > 100)
        {
            Logger.LogWarning("Rule 1 triggered: Amount exceeds 100");

            return new RuleExecutionResult
            {
                RuleName = nameof(Rule1),
                Passed = false,
                Message = "Amount exceeds £100"
            };
        }

        Logger.LogInformation("Rule 1 passed: Amount is within limit");

        return new RuleExecutionResult
        {
            RuleName = nameof(Rule1),
            Passed = true
        };
    }
}

