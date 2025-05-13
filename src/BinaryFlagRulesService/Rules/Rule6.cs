using Core.DTOs;
using Core.Enums;
using Core.Models;
using Microsoft.Extensions.Logging;

namespace Rules;

// Sample rule implementations
public class Rule6 : BaseFraudRule<Rule6>
{
    public Rule6(ILogger<Rule6> logger) : base(logger) {}
    public override FraudRuleFlags Flag => FraudRuleFlags.Rule6;

    public override RuleExecutionResult Execute(PaymentDto payment)
    {
        Logger.LogInformation("Executing Rule 6");

        if (payment.Amount > 600)
        {
            Logger.LogWarning("Rule 6 triggered: Amount exceeds 600");

            return new RuleExecutionResult
            {
                RuleName = nameof(Rule6),
                Passed = false,
                Message = "Amount exceeds £600"
            };
        }

        Logger.LogInformation("Rule 6 passed: Amount is within limit");

        return new RuleExecutionResult
        {
            RuleName = nameof(Rule6),
            Passed = true
        };
    }
}
