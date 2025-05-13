namespace Core.Models;

public class RuleExecutionResult
{
    public string RuleName { get; set; }
    public bool Passed { get; set; }
    public string? Message { get; set; }
}
