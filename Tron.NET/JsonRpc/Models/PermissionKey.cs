using Newtonsoft.Json;
using Tron.Converters;

namespace Tron.JsonRpc.Models
{
    public class PermissionKey
    {
        [JsonProperty("address")]
        [JsonConverter(typeof(AddressToHexConverter))]
        public Address Address { get; set; }
        [JsonProperty("weight")]
        public int Weight { get; set; }
    }
}
