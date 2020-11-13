using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tron.Rpc.Models;

namespace Tron.Rpc
{
    public class JsonRpcClient
    {
        private readonly Uri host;

        private readonly HttpClient httpClient = new HttpClient();

        public JsonRpcClient(string host) : this(new Uri(host)) { }

        public JsonRpcClient(Uri host)
        {
            if (host == null)
                throw new ArgumentNullException(nameof(host));

            this.host = host;
        }

        public Task<Account> GetAccountAsync(string address)
        {
            return GetAccountAsync(Address.FromBase58(address));
        }

        public async Task<Account> GetAccountAsync(Address address)
        {
            using (var hrm = new HttpRequestMessage(HttpMethod.Post, $"{host}/wallet/getaccount"))
            {
                hrm.Content = new StringContent(
                    JsonConvert.SerializeObject(
                        new
                        {
                            address = address.ToHexString(false),
                        }
                    ),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await httpClient.SendAsync(hrm);

                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Account>(content);
            }
        }
    }
}
