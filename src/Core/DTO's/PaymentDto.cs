namespace Core.DTOs;

using System.Text.Json.Serialization;
using Core.Enums;

public abstract class PaymentDto
{
    [JsonIgnore]
    public FraudRuleFlags RulesToRun { get; set; }
    public int PaymentId { get; set; }
    public decimal Amount { get; set; }
}
