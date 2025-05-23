using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Core.DTOs;
using Core.Enums;

public class PaymentDtoConverter : JsonConverter<PaymentDto>
{
    public override PaymentDto? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        if (!root.TryGetProperty("paymentType", out var typeProp))
        {
            throw new JsonException("Missing 'paymentType' field.");
        }

        var paymentType = (PaymentType)typeProp.GetInt32();
        var dtoType = PaymentDtoTypeMap.GetType(paymentType);

        if (dtoType == null)
        {
            throw new JsonException($"Unknown PaymentType value: {paymentType}");
        }

        var json = root.GetRawText();
        return (PaymentDto?)JsonSerializer.Deserialize(json, dtoType, options);
    }

    public override void Write(Utf8JsonWriter writer, PaymentDto value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
    }
}
