using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CloudStorageModifier.APIHelper
{
    internal class Cloud
    {
        private static readonly string baseUri = "https://fngw-mcp-gc-livefn.ol.epicgames.com/fortnite/api/cloudstorage";
        public static async Task<byte[]> Download(string fileName, string accountId, string exchangeCode)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, baseUri + "/user/" + accountId + "/" + fileName))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", exchangeCode);

                using (HttpResponseMessage response = await Program.client.SendAsync(request))
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }
            }
        }
        public static async Task<bool> Exist(string fileName, string accountId, string exchangeCode) => (await List(accountId, exchangeCode)).Children<JObject>().Any(jsonObject => jsonObject["filename"].ToString().ToLower() == fileName.ToLower());
        public static async Task<JArray> List(string accountId, string exchangeCode)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, baseUri + "/user/" + accountId))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", exchangeCode);

                using (HttpResponseMessage response = await Program.client.SendAsync(request))
                {
                    string responseString = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(responseString);

                    return JArray.Parse(responseString);
                }
            }
        }
        public static async Task<JObject> Create(string fileName, byte[] fileData, string accountId, string exchangeCode)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, baseUri + "/user/" + accountId + "/" + fileName))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", exchangeCode);

                request.Content = new ByteArrayContent(fileData);

                using (HttpResponseMessage response = await Program.client.SendAsync(request))
                {
                    string responseString = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(responseString);

                    return JObject.Parse(responseString);
                }
            }
        }
        public static async Task<JObject> Update(string fileName, byte[] fileData, string accountId, string exchangeCode)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, baseUri + "/user/" + accountId + "/" + fileName))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", exchangeCode);

                request.Content = new ByteArrayContent(fileData);

                using (HttpResponseMessage response = await Program.client.SendAsync(request))
                {
                    string responseString = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(responseString);

                    return JObject.Parse(responseString ?? "{}");
                }
            }
        }
    }
}
