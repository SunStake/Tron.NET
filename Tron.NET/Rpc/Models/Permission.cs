using System.Collections.Generic;
using Newtonsoft.Json;

namespace Tron.Rpc.Models
{
    public class Permission
    {
        [JsonProperty("permission_name")]
        public string PermissionName { get; set; }
        [JsonProperty("threshold")]
        public int Threshold { get; set; }
        [JsonProperty("keys")]
        public IReadOnlyList<PermissionKey> Keys { get; set; }
    }
}
