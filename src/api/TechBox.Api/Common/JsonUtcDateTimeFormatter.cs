using System.Text.Json;
using System.Text.Json.Serialization;

namespace TechBox.Api.Common;

public class JsonUtcDateTimeFormatter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateText = reader.GetString();

        if (dateText is null) return DateTime.MinValue;

        return DateTime.Parse(dateText);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-ddTHH:mm:ss.fff'Z'"));
    }
}
