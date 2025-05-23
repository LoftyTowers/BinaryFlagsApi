using System.Collections.Generic;
using System.Linq;
using Core.DTOs;
using Core.Enums;
using Core.Models;
using Engines;
using FluentAssertions;
using Moq;
using Rules;
using Xunit;

namespace BinaryFlagRulesService.Tests;

public class FraudRuleEngineTests
{
    private static RuleExecutionResult Pass(string ruleName) => new RuleExecutionResult
    {
        RuleName = ruleName,
        Passed = true
    };

    private static RuleExecutionResult Fail(string ruleName, string message) => new RuleExecutionResult
    {
        RuleName = ruleName,
        Passed = false,
        Message = message
    };

    [Fact]
    public void RunRules_Should_Return_Only_Triggered_RuleResults()
    {
        // Arrange
        var rule1 = new Mock<IBaseFraudRule>();
        rule1.Setup(r => r.Flag).Returns(FraudRuleFlags.Rule1);
        rule1.Setup(r => r.Execute(It.IsAny<PaymentDto>())).Returns(Fail("Rule1", "Amount too high"));

        var rule2 = new Mock<IBaseFraudRule>();
        rule2.Setup(r => r.Flag).Returns(FraudRuleFlags.Rule2);
        rule2.Setup(r => r.Execute(It.IsAny<PaymentDto>())).Returns(Pass("Rule2"));

        var engine = new FraudRuleEngine(new[] { rule1.Object, rule2.Object });

        var payment = new ImmediatePaymentDto
        {
            Amount = 1500,
            RulesToRun = FraudRuleFlags.Rule1 // Only Rule1 should be executed
        };

        // Act
        var results = engine.RunRules(payment);

        // Assert
        results.Should().HaveCount(1);
        results[0].RuleName.Should().Be("Rule1");
        results[0].Passed.Should().BeFalse();
        results[0].Message.Should().Be("Amount too high");

        rule2.Verify(r => r.Execute(It.IsAny<PaymentDto>()), Times.Never);
    }

    [Fact]
    public void RunRules_Should_Return_All_Passing_Results()
    {
        var rule1 = new Mock<IBaseFraudRule>();
        rule1.Setup(r => r.Flag).Returns(FraudRuleFlags.Rule1);
        rule1.Setup(r => r.Execute(It.IsAny<PaymentDto>())).Returns(Pass("Rule1"));

        var rule2 = new Mock<IBaseFraudRule>();
        rule2.Setup(r => r.Flag).Returns(FraudRuleFlags.Rule2);
        rule2.Setup(r => r.Execute(It.IsAny<PaymentDto>())).Returns(Pass("Rule2"));

        var engine = new FraudRuleEngine(new[] { rule1.Object, rule2.Object });

        var payment = new ImmediatePaymentDto
        {
            Amount = 500,
            RulesToRun = FraudRuleFlags.Rule1 | FraudRuleFlags.Rule2
        };

        var results = engine.RunRules(payment);

        results.Should().HaveCount(2);
        results.All(r => r.Passed).Should().BeTrue();
    }

    [Fact]
    public void RunRules_Should_Return_Empty_If_No_Rules_Match()
    {
        var rule1 = new Mock<IBaseFraudRule>();
        rule1.Setup(r => r.Flag).Returns(FraudRuleFlags.Rule1);

        var engine = new FraudRuleEngine(new[] { rule1.Object });

        var payment = new ImmediatePaymentDto
        {
            RulesToRun = FraudRuleFlags.Rule2 // No match
        };

        var results = engine.RunRules(payment);

        results.Should().BeEmpty();
        rule1.Verify(r => r.Execute(It.IsAny<PaymentDto>()), Times.Never);
    }
}
