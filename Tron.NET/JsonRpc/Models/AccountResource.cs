using System;
using Newtonsoft.Json;
using Tron.Converters;

namespace Tron.JsonRpc.Models
{
    public class AccountResource
    {
        [JsonProperty("latest_consume_time_for_energy")]
        [JsonConverter(typeof(DateTimeOffsetToUinxTimeMsConverter))]
        public DateTimeOffset LatestConsumeTimeForEnergy { get; set; }
    }
}
