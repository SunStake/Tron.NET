using System;
using Newtonsoft.Json;

namespace Tron.Converters
{
    internal class DateTimeOffsetToUinxTimeMsConverter : JsonConverter<DateTimeOffset>
    {
        public override bool CanRead => true;
        public override bool CanWrite => false;

        public override DateTimeOffset ReadJson(JsonReader reader, Type objectType, DateTimeOffset existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Integer)
            {
                return DateTimeOffset.FromUnixTimeMilliseconds((long)reader.Value);
            }
            else
            {
                throw new JsonSerializationException("Invalid token type");
            }
        }

        public override void WriteJson(JsonWriter writer, DateTimeOffset value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }
    }
}
