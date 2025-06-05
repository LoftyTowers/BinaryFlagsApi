using Xunit;
using FluentAssertions;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Controllers;
using Engines;
using Core.DTOs;
using Core.Models;
using Core.Enums;
using System.Collections.Generic;
using Factories;

namespace BinaryFlagsApi.Tests;

public class PaymentsControllerTests
{
    private readonly Mock<ILogger<PaymentsController>> _logger = new();
    private readonly Mock<IFraudRuleEngine> _fraudEngine = new();
    private readonly Mock<IPaymentRuleFactory> _ruleFactory = new();
    private readonly PaymentsController _controller;

    public PaymentsControllerTests()
    {
        _controller = new PaymentsController(_logger.Object, _fraudEngine.Object, _ruleFactory.Object);
    }

    [Fact]
    public void ProcessPayment_Should_Return_BadRequest_If_Payment_Is_Null()
    {
        // Act
        var result = _controller.ProcessPayment(null);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public void ProcessPayment_Should_Return_Ok_When_All_Rules_Pass()
    {
        // Arrange
        var payment = new ImmediatePaymentDto { Amount = 100, PaymentType = PaymentType.ImmediatePayment };

        var enriched = new ImmediatePaymentDto
        {
            Amount = 100,
            PaymentType = PaymentType.ImmediatePayment,
            RulesToRun = FraudRuleFlags.Rule1 | FraudRuleFlags.Rule2
        };

        _ruleFactory.Setup(f => f.AssignRules(payment)).Returns(enriched);

        _fraudEngine.Setup(e => e.RunRules(enriched)).Returns(new List<RuleExecutionResult>
        {
            new() { RuleName = "Rule1", Passed = true },
            new() { RuleName = "Rule2", Passed = true }
        });

        // Act
        var result = _controller.ProcessPayment(payment) as OkObjectResult;

        // Assert
        result.Should().NotBeNull();
        var response = result!.Value!.ToString();
        response.Should().Contain("Payment passed fraud checks");
    }

    [Fact]
    public void ProcessPayment_Should_Return_BadRequest_When_Any_Rule_Fails()
    {
        var payment = new FuturePaymentDto { Amount = 999, PaymentType = PaymentType.FuturePayment };

        var enriched = new FuturePaymentDto
        {
            Amount = 999,
            PaymentType = PaymentType.FuturePayment,
            RulesToRun = FraudRuleFlags.Rule3
        };

        _ruleFactory.Setup(f => f.AssignRules(payment)).Returns(enriched);

        _fraudEngine.Setup(e => e.RunRules(enriched)).Returns(new List<RuleExecutionResult>
        {
            new() { RuleName = "Rule3", Passed = false, Message = "Amount too high" }
        });

        var result = _controller.ProcessPayment(payment) as BadRequestObjectResult;

        result.Should().NotBeNull();
        var response = result!.Value!.ToString();
        response.Should().Contain("Payment failed fraud checks");
    }
}
