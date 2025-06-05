using System;
using System.Collections.Generic;
using System.Linq;
using Core.Enums;
using Core.DTOs;

namespace Configs;
public class FraudRulesConfig
{
    public string ImmediatePayment { get; set; }
    public string FuturePayment { get; set; }
    public string StandingOrder { get; set; }

    public Dictionary<string, FraudRuleFlags> ToFlagMap()
    {
        return new Dictionary<string, FraudRuleFlags>
        {
            { nameof(ImmediatePaymentDto), ParseFlags(ImmediatePayment) },
            { nameof(FuturePaymentDto), ParseFlags(FuturePayment) },
            { nameof(StandingOrderDto), ParseFlags(StandingOrder) }
        };
    }

    private FraudRuleFlags ParseFlags(string csv)
    {
        return csv.Split(',', StringSplitOptions.RemoveEmptyEntries)
                  .Select(s => Enum.Parse<FraudRuleFlags>(s.Trim()))
                  .Aggregate(FraudRuleFlags.None, (acc, val) => acc | val);
    }
}
