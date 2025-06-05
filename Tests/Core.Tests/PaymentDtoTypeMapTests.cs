using Xunit;
using FluentAssertions;
using Core.DTOs;
using Core.Enums;

namespace Core.Tests;

public class PaymentDtoTypeMapTests
{
    [Theory]
    [InlineData(PaymentType.ImmediatePayment, typeof(ImmediatePaymentDto))]
    [InlineData(PaymentType.FuturePayment, typeof(FuturePaymentDto))]
    [InlineData(PaymentType.StandingOrder, typeof(StandardOrderDto))]
    public void Should_Return_Correct_Type_For_Known_Enum(PaymentType type, Type expectedDtoType)
    {
        // Act
        var result = PaymentDtoTypeMap.GetType(type);

        // Assert
        result.Should().Be(expectedDtoType);
    }

    [Fact]
    public void Should_Return_Null_For_Unknown_Enum()
    {
        // Act
        var result = PaymentDtoTypeMap.GetType(PaymentType.Unknown);

        // Assert
        result.Should().BeNull();
    }
}
