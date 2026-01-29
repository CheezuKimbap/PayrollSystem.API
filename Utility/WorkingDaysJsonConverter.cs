using PayrollSystem.API.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PayrollSystem.API.Utility
{
    public class WorkingDaysJsonConverter : JsonConverter<WorkingDays>
    {
        public override WorkingDays Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            var value = reader.GetString();

            if (Enum.TryParse<WorkingDays>(value, true, out var result))
            {
                return result;
            }

            throw new JsonException(
                "WorkingDays must be either 'MWF' or 'TTHS'.");
        }

        public override void Write(
            Utf8JsonWriter writer,
            WorkingDays value,
            JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
