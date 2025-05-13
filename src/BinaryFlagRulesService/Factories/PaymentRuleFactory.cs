// Engines/PaymentRuleFactory.cs
using Configs;
using Core.Enums;
using Core.DTOs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace Factories;

public class PaymentRuleFactory : IPaymentRuleFactory
{
    private readonly ILogger<PaymentRuleFactory> _logger;
    private readonly Dictionary<string, FraudRuleFlags> _ruleMap;

    public PaymentRuleFactory(IOptions<FraudRulesConfig> config, ILogger<PaymentRuleFactory> logger)
    {
        _logger = logger;
        _ruleMap = config.Value.ToFlagMap();
    }

    public PaymentDto AssignRules(PaymentDto payment)
    {
        var typeName = payment.GetType().Name;

        if (!_ruleMap.TryGetValue(typeName, out var flags))
        {
            _logger.LogWarning("No rules found for payment type: {Type}", typeName);
            return payment;
        }

        payment.RulesToRun = flags;
        return payment;
    }
}
