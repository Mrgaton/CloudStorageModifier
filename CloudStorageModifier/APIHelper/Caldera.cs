using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CloudStorageModifier.APIHelper
{
    internal class Caldera
    {
        public static async Task<JObject> CalderaRACP(string exchange, string accountId)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://caldera-service-prod.ecosec.on.epicgames.com/caldera/api/v1/launcher/racp"))
            {
                request.Headers.TryAddWithoutValidation("User-Agent", "Caldera/UNKNOWN-UNKNOWN-UNKNOWN");

                request.Content = new StringContent("{\"account_id\":\"" + accountId + "\",\"exchange_code\":\"" + exchange + "\",\"test_mode\":false,\"epic_app\":\"fortnite\",\"nvidia\":false,\"luna\":false,\"salmon\":false}");

                using (HttpResponseMessage response = await Program.client.SendAsync(request))
                {
                    JObject responseJson = JObject.Parse(await response.Content.ReadAsStringAsync());

                    Console.WriteLine();
                    Console.WriteLine(responseJson);

                    return responseJson;
                }
            }
        }
    }
}