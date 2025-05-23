using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;
using FluentAssertions;
using Core.DTOs;
using Engines;

namespace Core.Tests;

public class PaymentDtoConverterTests
{
    private readonly JsonSerializerOptions _options;

    public PaymentDtoConverterTests()
    {
        _options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        _options.Converters.Add(new PaymentDtoConverter());
    }

    [Theory]
    [InlineData("{\"paymentType\":10,\"amount\":100}", typeof(FuturePaymentDto))]
    [InlineData("{\"paymentType\":20,\"amount\":200}", typeof(ImmediatePaymentDto))]
    [InlineData("{\"paymentType\":30,\"amount\":300}", typeof(StandardOrderDto))]
    public void Should_Deserialize_To_Correct_Derived_Type(string json, Type expectedType)
    {
        // Act
        var result = JsonSerializer.Deserialize<PaymentDto>(json, _options);

        // Assert
        result.Should().NotBeNull();
        result!.GetType().Should().Be(expectedType);
    }

    [Fact]
    public void Should_Throw_When_PaymentType_Is_Missing()
    {
        var json = "{\"amount\":100}";

        var act = () => JsonSerializer.Deserialize<PaymentDto>(json, _options);

        act.Should().Throw<JsonException>()
           .WithMessage("*Missing 'paymentType'*");
    }

    [Fact]
    public void Should_Throw_When_PaymentType_Is_Invalid()
    {
        var json = "{\"paymentType\":99,\"amount\":100}";

        var act = () => JsonSerializer.Deserialize<PaymentDto>(json, _options);

        act.Should().Throw<JsonException>()
           .WithMessage("*Unknown PaymentType*");
    }

    [Fact]
    public void Should_Serialize_Concrete_Type_Correctly()
    {
        var dto = new ImmediatePaymentDto
        {
            Amount = 123.45m,
            PaymentId = 42
        };

        var json = JsonSerializer.Serialize<PaymentDto>(dto, _options);

        json.Should().Contain("\"paymentType\":20");
        json.Should().Contain("\"amount\":123.45");
    }
}
