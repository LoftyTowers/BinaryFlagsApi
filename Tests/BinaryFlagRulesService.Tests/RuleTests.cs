using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using Moq;
using Core.DTOs;
using Rules;
using Microsoft.Extensions.Logging;

namespace BinaryFlagRulesService.Tests
{
    public class RuleTests
    {
        public static IEnumerable<object[]> RuleTestData =>
            new List<object[]>
            {
                // Rule1: Fail if Amount > 100
                new object[] { new Rule1(Mock.Of<ILogger<Rule1>>()), new ImmediatePaymentDto { Amount = 150 }, false },
                new object[] { new Rule1(Mock.Of<ILogger<Rule1>>()), new ImmediatePaymentDto { Amount = 99 }, true },

                // Rule2: Fail if Amount > 200
                new object[] { new Rule2(Mock.Of<ILogger<Rule2>>()), new FuturePaymentDto { Amount = 250 }, false },
                new object[] { new Rule2(Mock.Of<ILogger<Rule2>>()), new FuturePaymentDto { Amount = 199 }, true },

                // Rule3: Fail if Amount > 300
                new object[] { new Rule3(Mock.Of<ILogger<Rule3>>()), new StandingOrderDto { Amount = 350 }, false },
                new object[] { new Rule3(Mock.Of<ILogger<Rule3>>()), new StandingOrderDto { Amount = 250 }, true },

                // Rule4: Fail if Amount > 400
                new object[] { new Rule4(Mock.Of<ILogger<Rule4>>()), new ImmediatePaymentDto { Amount = 450 }, false },
                new object[] { new Rule4(Mock.Of<ILogger<Rule4>>()), new ImmediatePaymentDto { Amount = 350 }, true },

                // Rule5: Fail if Amount > 500
                new object[] { new Rule5(Mock.Of<ILogger<Rule5>>()), new FuturePaymentDto { Amount = 550 }, false },
                new object[] { new Rule5(Mock.Of<ILogger<Rule5>>()), new FuturePaymentDto { Amount = 450 }, true },

                // Rule6: Fail if Amount > 600
                new object[] { new Rule6(Mock.Of<ILogger<Rule6>>()), new ImmediatePaymentDto { Amount = 650 }, false },
                new object[] { new Rule6(Mock.Of<ILogger<Rule6>>()), new ImmediatePaymentDto { Amount = 550 }, true },

                // Rule7: Fail if Amount > 700
                new object[] { new Rule7(Mock.Of<ILogger<Rule7>>()), new StandingOrderDto { Amount = 750 }, false },
                new object[] { new Rule7(Mock.Of<ILogger<Rule7>>()), new StandingOrderDto { Amount = 650 }, true },

                // Rule8: Fail if Amount > 800
                new object[] { new Rule8(Mock.Of<ILogger<Rule8>>()), new ImmediatePaymentDto { Amount = 850 }, false },
                new object[] { new Rule8(Mock.Of<ILogger<Rule8>>()), new ImmediatePaymentDto { Amount = 750 }, true },

                // Rule9: Fail if Amount > 900
                new object[] { new Rule9(Mock.Of<ILogger<Rule9>>()), new ImmediatePaymentDto { Amount = 950 }, false },
                new object[] { new Rule9(Mock.Of<ILogger<Rule9>>()), new ImmediatePaymentDto { Amount = 850 }, true },

                // Rule10: Fail if Amount > 1000
                new object[] { new Rule10(Mock.Of<ILogger<Rule10>>()), new ImmediatePaymentDto { Amount = 1050 }, false },
                new object[] { new Rule10(Mock.Of<ILogger<Rule10>>()), new ImmediatePaymentDto { Amount = 950 }, true },
            };


        [Theory]
        [MemberData(nameof(RuleTestData))]
        public void Rule_Should_PassOrFail_AsExpected(IBaseFraudRule rule, PaymentDto payment, bool expectedPass)
        {
            // Act
            var result = rule.Execute(payment);

            // Assert
            result.Passed.Should().Be(expectedPass);
            result.RuleName.Should().NotBeNullOrWhiteSpace();
        }
    }
}
