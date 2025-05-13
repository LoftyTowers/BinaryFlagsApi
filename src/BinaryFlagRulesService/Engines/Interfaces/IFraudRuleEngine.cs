using System.Collections.Generic;
using Core.DTOs;
using Core.Models;

namespace Engines;
public interface IFraudRuleEngine
{
    List<RuleExecutionResult> RunRules(PaymentDto payment);
}
