using Xunit;
using FluentAssertions;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Configs;
using Core.Enums;
using Core.DTOs;
using Engines;
using Factories;

namespace BinaryFlagRulesService.Tests;

public class RuleFactoryTests
{
    private readonly IOptions<FraudRulesConfig> _configOptions;
    private readonly ILogger<PaymentRuleFactory> _logger;

    public RuleFactoryTests()
    {
        _logger = Mock.Of<ILogger<PaymentRuleFactory>>();

        _configOptions = Options.Create(new FraudRulesConfig
        {
            ImmediatePayment = "Rule1,Rule4,Rule5",
            FuturePayment = "Rule2,Rule3,Rule6",
            StandingOrder = "Rule7,Rule8,Rule9,Rule10"
        });
    }

    [Theory]
    [InlineData(typeof(ImmediatePaymentDto), FraudRuleFlags.Rule1 | FraudRuleFlags.Rule4 | FraudRuleFlags.Rule5)]
    [InlineData(typeof(FuturePaymentDto), FraudRuleFlags.Rule2 | FraudRuleFlags.Rule3 | FraudRuleFlags.Rule6)]
    [InlineData(typeof(StandingOrderDto), FraudRuleFlags.Rule7 | FraudRuleFlags.Rule8 | FraudRuleFlags.Rule9 | FraudRuleFlags.Rule10)]
    public void AssignRules_Should_Set_Correct_RulesToRun(Type dtoType, FraudRuleFlags expectedFlags)
    {
        // Arrange
        var factory = new PaymentRuleFactory(_configOptions, _logger);
        var payment = (PaymentDto)Activator.CreateInstance(dtoType)!;

        // Act
        var result = factory.AssignRules(payment);

        // Assert
        result.Should().NotBeNull();
        result.RulesToRun.Should().Be(expectedFlags);
    }

    [Fact]
    public void AssignRules_Should_Set_NoRules_When_Type_Not_Configured()
    {
        // Arrange
        var factory = new PaymentRuleFactory(_configOptions, _logger);

        var unknown = new UnknownPaymentDto(); // not mapped in config

        // Act
        var result = factory.AssignRules(unknown);

        // Assert
        result.RulesToRun.Should().Be(FraudRuleFlags.None);
    }

    private class UnknownPaymentDto : PaymentDto { }
}
