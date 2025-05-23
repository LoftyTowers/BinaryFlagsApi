using Xunit;
using Moq;
using FluentAssertions;
using Core.DTOs;
using Core.Enums;
using Core.Models;
using Microsoft.Extensions.Logging;
using Rules;

namespace BinaryFlagRulesService.Tests;

public class BaseFraudRuleTests
{
    // Mock subclass to test the abstract base class
    public class TestFraudRule : BaseFraudRule<TestFraudRule>
    {
        public TestFraudRule(ILogger<TestFraudRule> logger) : base(logger) { }

        public override FraudRuleFlags Flag => FraudRuleFlags.Rule1;

        public override RuleExecutionResult Execute(PaymentDto payment)
        {
            Logger.LogInformation("Executing test fraud rule");
            return new RuleExecutionResult
            {
                RuleName = nameof(TestFraudRule),
                Passed = true,
                Message = "Always passes"
            };
        }
    }

    [Fact]
    public void Logger_Should_Log_When_Execute_Is_Called()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<TestFraudRule>>();
        var rule = new TestFraudRule(loggerMock.Object);
        var payment = new ImmediatePaymentDto { Amount = 123 };

        // Act
        var result = rule.Execute(payment);

        // Assert
        result.Should().NotBeNull();
        result.Passed.Should().BeTrue();
        result.RuleName.Should().Be(nameof(TestFraudRule));
        result.Message.Should().Be("Always passes");

        loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Executing test fraud rule")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public void Flag_Should_Return_Correct_Enum_Value()
    {
        var rule = new TestFraudRule(Mock.Of<ILogger<TestFraudRule>>());

        rule.Flag.Should().Be(FraudRuleFlags.Rule1);
    }
}
