using System.Collections.Generic;
using System.Linq;
using Core.DTOs;
using Rules;
using Core.Models;

namespace Engines;

public class FraudRuleEngine : IFraudRuleEngine
{
    private readonly List<IBaseFraudRule> _rules;

    public FraudRuleEngine(IEnumerable<IBaseFraudRule> rules)
    {
        _rules = rules.ToList();
    }

    public List<RuleExecutionResult> RunRules(PaymentDto payment)
    {
        var results = new List<RuleExecutionResult>();

        foreach (var rule in _rules)
        {
            if ((payment.RulesToRun & rule.Flag) != 0)
            {
                var result = rule.Execute(payment);
                results.Add(result);
            }
        }

        return results;
    }
}
