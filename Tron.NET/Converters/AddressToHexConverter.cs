using System;
using Newtonsoft.Json;
using Tron.Utilities;

namespace Tron.Converters
{
    internal class AddressToHexConverter : JsonConverter<Address>
    {
        public override bool CanRead => true;
        public override bool CanWrite => false;

        public override Address ReadJson(JsonReader reader, Type objectType, Address existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                return new Address(HexUtilities.HexToBytes((string)reader.Value));
            }
            else
            {
                throw new JsonSerializationException("Invalid token type");
            }
        }

        public override void WriteJson(JsonWriter writer, Address value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }
    }
}
