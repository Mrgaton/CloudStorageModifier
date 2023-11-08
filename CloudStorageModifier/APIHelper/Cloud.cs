using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CloudStorageModifier.APIHelper
{
    internal static class Cloud
    {
        private static readonly string baseUri = "https://fngw-mcp-gc-livefn.ol.epicgames.com/fortnite/api/cloudstorage";

        public static async Task<byte[]> Download(string fileName, string accountId, string exchangeCode)
        {
            Program.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", exchangeCode);

            using (HttpResponseMessage response = await Program.client.GetAsync(baseUri + "/user/" + accountId + "/" + fileName))
            {
                return await response.Content.ReadAsByteArrayAsync();
            }
        }

        public static async Task<bool> Exist(string fileName, string accountId, string exchangeCode) => (await List(accountId, exchangeCode)).Children<JObject>().Any(jsonObject => jsonObject["filename"].ToString().ToLowerInvariant() == fileName.ToLower());

        public static async Task<JArray> List(string accountId, string exchangeCode)
        {
            Program.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", exchangeCode);

            using (HttpResponseMessage response = await Program.client.GetAsync(baseUri + "/user/" + accountId))
            {
                string responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseString);
                return JArray.Parse(responseString);
            }
        }

        public static async Task<JObject> Create(string fileName, byte[] fileData, string accountId, string exchangeCode)
        {
            Program.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", exchangeCode);

            using (HttpResponseMessage response = await Program.client.PostAsync(baseUri + "/user/" + accountId + "/" + fileName, new ByteArrayContent(fileData)))
            {
                string responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseString);
                return JObject.Parse(string.IsNullOrWhiteSpace(responseString) ? "{}" : responseString);
            }
        }

        public static async Task<JObject> Update(string fileName, byte[] fileData, string accountId, string exchangeCode)
        {
            Program.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", exchangeCode);

            using (HttpResponseMessage response = await Program.client.PutAsync(baseUri + "/user/" + accountId + "/" + fileName, new ByteArrayContent(fileData)))
            {
                string responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseString);
                return JObject.Parse(string.IsNullOrWhiteSpace(responseString) ? "{}" : responseString);
            }
        }
    }
}