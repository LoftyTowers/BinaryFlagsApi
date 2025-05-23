using Xunit;
using FluentAssertions;
using Core.Enums;
using Core.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Core.Tests;

public class EnumExtensionsTests
{
    [Theory]
    [InlineData(PaymentType.Unknown, "Unknown")]
    [InlineData(PaymentType.FuturePayemnt, "Future Payment")]
    [InlineData(PaymentType.ImediatePayment, "Immediate Payment")]
    [InlineData(PaymentType.StandingOrder, "Standing Order")]
    public void GetDisplayName_Should_Return_DisplayNameAttribute(PaymentType input, string expected)
    {
        // Act
        var display = input.GetDisplayName();

        // Assert
        display.Should().Be(expected);
    }

    public enum CustomTestEnum
    {
        [Display(Name = "Custom Value")]
        WithDisplay,

        WithoutDisplay
    }

    [Fact]
    public void GetDisplayName_Should_Fallback_To_EnumName_If_No_Attribute()
    {
        CustomTestEnum.WithoutDisplay.GetDisplayName().Should().Be(nameof(CustomTestEnum.WithoutDisplay));
    }

    [Fact]
    public void GetDisplayName_Should_Use_Display_Attribute_If_Present()
    {
        CustomTestEnum.WithDisplay.GetDisplayName().Should().Be("Custom Value");
    }
}
