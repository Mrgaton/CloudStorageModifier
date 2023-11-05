using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudStorageModifier.APIHelper
{
    internal class Auth
    {
        public static readonly string defaultUri = "https://account-public-service-prod.ol.epicgames.com";

        public static readonly string token = "OThmN2U0MmMyZTNhNGY4NmE3NGViNDNmYmI0MWVkMzk6MGEyNDQ5YTItMDAxYS00NTFlLWFmZWMtM2U4MTI5MDFjNGQ3";

        public static async Task<string> GetCredentialsToken()
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, defaultUri + "/account/api/oauth/token"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", token);
                request.Content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                });

                using (HttpResponseMessage response = await Program.client.SendAsync(request))
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    return JObject.Parse(responseString)["access_token"].ToString();
                }
            }
        }

        public static async Task<string> GetAccessToken() => await GetAccessToken(await GetCredentialsToken());
        public static async Task<string> GetAccessToken(string credentialsToken)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, defaultUri + "/account/api/oauth/deviceAuthorization"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", credentialsToken);

                using (HttpResponseMessage response = await Program.client.SendAsync(request))
                {
                    JObject responseJson = JObject.Parse(await response.Content.ReadAsStringAsync());

                    string verificationUriComplete = responseJson["verification_uri_complete"].ToString();

                    string deviceCode = responseJson["device_code"].ToString();

                    Process.Start(verificationUriComplete);

                    return JObject.Parse(await WaitDeviceCompletion(deviceCode))["access_token"].ToString();
                }
            }
        }

        private static async Task<string> WaitDeviceCompletion(string deviceCode)
        {
            string responseString = string.Empty;

            while (true)
            {
                using (HttpRequestMessage subRequest = new HttpRequestMessage(HttpMethod.Post, defaultUri + "/account/api/oauth/token"))
                {
                    subRequest.Headers.Authorization = new AuthenticationHeaderValue("basic", token);

                    subRequest.Content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("grant_type", "device_code"),
                        new KeyValuePair<string, string>("device_code", deviceCode)
                    });

                    using (HttpResponseMessage subResponse = await Program.client.SendAsync(subRequest))
                    {
                        responseString = await subResponse.Content.ReadAsStringAsync();

                        if (responseString.Contains("access_token"))
                        {
                            break;
                        }
                        if (responseString.Contains("errors.com.epicgames.not_found"))
                        {
                            Environment.Exit(0);
                        }
                        if (responseString.Contains("errors.com.epicgames.account.authorization_pending"))
                        {
                            await Task.Delay(2500);
                        }
                    }
                }
            }

            return responseString;
        }

        public static async Task<string> GetExchangeCode() => await GetExchangeCode(await GetAccessToken());
        public static async Task<string> GetExchangeCode(string accessToken)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, defaultUri + "/account/api/oauth/exchange"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using (HttpResponseMessage response = await Program.client.SendAsync(request))
                {
                    return JObject.Parse(await response.Content.ReadAsStringAsync())["code"].ToString();
                }
            }
        }
    }
}
