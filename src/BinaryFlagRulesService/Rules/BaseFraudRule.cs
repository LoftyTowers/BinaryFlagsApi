using Microsoft.Extensions.Logging;
using Core.DTOs;
using Core.Enums;
using Core.Models;

namespace Rules;

public abstract class BaseFraudRule<TSelf> : IBaseFraudRule where TSelf : BaseFraudRule<TSelf>
{
    protected readonly ILogger<TSelf> Logger;

    protected BaseFraudRule(ILogger<TSelf> logger)
    {
        Logger = logger;
    }

    public abstract FraudRuleFlags Flag { get; }

    public abstract RuleExecutionResult Execute(PaymentDto payment);

}
