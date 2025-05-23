using Xunit;
using FluentAssertions;
using Core.Enums;

namespace Core.Tests;

public class FraudRuleFlagsTests
{
    [Fact]
    public void Flags_Should_Have_Unique_PowerOfTwo_Values()
    {
        var values = Enum.GetValues<FraudRuleFlags>();

        var numericValues = values
            .Where(v => v != FraudRuleFlags.None)
            .Select(v => (int)v)
            .ToList();

        numericValues.Should().OnlyHaveUniqueItems();
        numericValues.Should().OnlyContain(v => (v & (v - 1)) == 0);
    }

    [Fact]
    public void Can_Combine_Multiple_Flags_With_Or()
    {
        var combined = FraudRuleFlags.Rule1 | FraudRuleFlags.Rule3 | FraudRuleFlags.Rule5;

        combined.HasFlag(FraudRuleFlags.Rule1).Should().BeTrue();
        combined.HasFlag(FraudRuleFlags.Rule3).Should().BeTrue();
        combined.HasFlag(FraudRuleFlags.Rule5).Should().BeTrue();
        combined.HasFlag(FraudRuleFlags.Rule2).Should().BeFalse();
    }

    [Fact]
    public void None_Should_Have_Zero_Value()
    {
        ((int)FraudRuleFlags.None).Should().Be(0);
    }

    [Fact]
    public void HasFlag_Should_Work_Correctly()
    {
        var flags = FraudRuleFlags.Rule2 | FraudRuleFlags.Rule4;

        flags.HasFlag(FraudRuleFlags.Rule2).Should().BeTrue();
        flags.HasFlag(FraudRuleFlags.Rule4).Should().BeTrue();
        flags.HasFlag(FraudRuleFlags.Rule1).Should().BeFalse();
    }
}
