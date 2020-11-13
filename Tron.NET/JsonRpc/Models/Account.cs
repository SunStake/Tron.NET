using System;
using System.Collections.Generic;
using System.Numerics;
using Newtonsoft.Json;
using Tron.Converters;

namespace Tron.JsonRpc.Models
{
    public class Account
    {
        [JsonProperty("address")]
        [JsonConverter(typeof(AddressToHexConverter))]
        public Address Address { get; set; }
        [JsonProperty("balance")]
        public BigInteger Balance { get; set; }
        [JsonProperty("create_time")]
        [JsonConverter(typeof(DateTimeOffsetToUinxTimeMsConverter))]
        public DateTimeOffset CreateTime { get; set; }
        [JsonProperty("latest_opration_time")]
        [JsonConverter(typeof(DateTimeOffsetToUinxTimeMsConverter))]
        public DateTimeOffset LatestOprationTime { get; set; }
        [JsonProperty("latest_consume_free_time")]
        [JsonConverter(typeof(DateTimeOffsetToUinxTimeMsConverter))]
        public DateTimeOffset LatestConsumeFreeTime { get; set; }
        [JsonProperty("account_resource")]
        public AccountResource AccountResource { get; set; }
        [JsonProperty("owner_permission")]
        public Permission OwnerPermission { get; set; }
        [JsonProperty("active_permission")]
        public IReadOnlyList<Permission> ActivePermission { get; set; }
    }
}
