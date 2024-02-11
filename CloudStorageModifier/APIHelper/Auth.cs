using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CloudStorageModifier.APIHelper
{
    internal static class Auth
    {
        public static readonly string baseUri = "https://account-public-service-prod03.ol.epicgames.com";

        public static readonly AuthenticationHeaderValue token = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes("98f7e42c2e3a4f86a74eb43fbb41ed39:0a2449a2-001a-451e-afec-3e812901c4d7")));

        public static async Task<string> GetCredentialsToken()
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, baseUri + "/account/api/oauth/token"))
            {
                SetHeaders(request);

                request.Headers.Authorization = token;
                request.Content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                });

                using (HttpResponseMessage response = await Program.client.SendAsync(request))
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(responseString);

                    return JObject.Parse(responseString)["access_token"].ToString();
                }
            }
        }

        public static async Task<JObject> GetAccessToken() => await GetAccessToken(await GetCredentialsToken());

        public static async Task<JObject> GetAccessToken(string credentialsToken)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, baseUri + "/account/api/oauth/deviceAuthorization"))
            {
                SetHeaders(request);

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", credentialsToken);

                using (HttpResponseMessage response = await Program.client.SendAsync(request))
                {
                    JObject responseJson = JObject.Parse(await response.Content.ReadAsStringAsync());

                    Console.WriteLine(responseJson);

                    string verificationUriComplete = responseJson["verification_uri_complete"].ToString();

                    string deviceCode = responseJson["device_code"].ToString();

                    Process.Start(verificationUriComplete);

                    JObject finalResponse = await WaitDeviceCompletion(deviceCode);

                    return finalResponse;
                }
            }
        }

        private static async Task<JObject> WaitDeviceCompletion(string deviceCode)
        {
            string responseString = string.Empty;

            while (true)
            {
                using (HttpRequestMessage subRequest = new HttpRequestMessage(HttpMethod.Post, baseUri + "/account/api/oauth/token"))
                {
                    SetHeaders(subRequest);

                    subRequest.Headers.Authorization = token;

                    subRequest.Content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("grant_type", "device_code"),
                        new KeyValuePair<string, string>("device_code", deviceCode)
                    });

                    using (HttpResponseMessage subResponse = await Program.client.SendAsync(subRequest))
                    {
                        responseString = await subResponse.Content.ReadAsStringAsync();

                        Console.WriteLine(responseString);

                        if (responseString.Contains("access_token"))
                        {
                            break;
                        }
                        else if (responseString.Contains("errors.com.epicgames.not_found"))
                        {
                            return JObject.Parse(responseString);
                        }

                        await Task.Delay(2000);
                    }
                }
            }

            return JObject.Parse(responseString);
        }

        public static async Task<JObject> GetExchangeCode() => await GetExchangeCode((await GetAccessToken())["access_token"].ToString());

        public static async Task<JObject> GetExchangeCode(string accessToken)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, baseUri + "/account/api/oauth/exchange"))
            {
                SetHeaders(request);

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using (HttpResponseMessage response = await Program.client.SendAsync(request))
                {
                    string responseString = await response.Content.ReadAsStringAsync();

                    Console.WriteLine();
                    Console.WriteLine(responseString);

                    return JObject.Parse(responseString);
                }
            }
        }

        public static async Task<JObject> CreateDeviceAuth(string auth, string accountId)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, baseUri + "/account/api/public/account/" + accountId + "/deviceAuth"))
            {
                SetHeaders(request);

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", auth);

                using (HttpResponseMessage response = await Program.client.SendAsync(request))
                {
                    JObject responseJson = JObject.Parse(await response.Content.ReadAsStringAsync());

                    Console.WriteLine();
                    Console.WriteLine(responseJson);

                    return responseJson;
                }
            }
        }

        private static AuthenticationHeaderValue launcherAppClient2Auth = new AuthenticationHeaderValue("basic", "MzRhMDJjZjhmNDQxNGUyOWIxNTkyMTg3NmRhMzZmOWE6ZGFhZmJjY2M3Mzc3NDUwMzlkZmZlNTNkOTRmYzc2Y2Y=");

        public static async Task<JObject> OauthTokenFromExchange(string exchange)
        {
            var form = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("grant_type", "exchange_code"),
                new KeyValuePair<string, string>("exchange_code", exchange)
            });

            return await OauthTokenCore(launcherAppClient2Auth, form);
        }

        public static async Task<JObject> OauthTokenFromDevice(string deviceId, string accountId, string secret)
        {
            var form = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("grant_type", "device_auth"),
                new KeyValuePair<string, string>("account_id", accountId),
                new KeyValuePair<string, string>("device_id", deviceId),
                new KeyValuePair<string, string>("secret", secret),
            });

            var switchAuth = await OauthTokenCore(token, form);

            /*var exchange = await GetExchangeCode((string)switchAuth["access_token"]);

            var claro = await OauthTokenFromExchange((string)exchange["code"]);

           await GrantAcess((string)claro["access_token"], (string)switchAuth["account_id"]);*/

            return switchAuth;

            // return ;
        }

        public static async Task GrantAcess(string acessToken, string accountId)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://fortnite-public-service-prod11.ol.epicgames.com/fortnite/api/game/v2/grant_access/" + accountId))
            {
                //SetHeaders(request);

                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", acessToken);
                request.Content = new StringContent("{}", Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await Program.client.SendAsync(request))
                {
                    string responseString = await response.Content.ReadAsStringAsync();

                    Console.WriteLine();
                    Console.WriteLine(responseString);
                }
            }
        }

        public static async Task<JObject> OauthTokenCore(AuthenticationHeaderValue authorization, FormUrlEncodedContent content)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://account-public-service-prod.ol.epicgames.com/account/api/oauth/token"))
            {
                SetHeaders(request);

                if (authorization != null) request.Headers.Authorization = authorization;

                request.Content = content;

                using (HttpResponseMessage response = await Program.client.SendAsync(request))
                {
                    string responseString = await response.Content.ReadAsStringAsync();

                    Console.WriteLine();
                    Console.WriteLine(responseString);

                    return JObject.Parse(responseString);
                }
            }
        }

        private static void SetHeaders(HttpRequestMessage request)
        {
            request.Headers.TryAddWithoutValidation("X-Epic-Correlation-ID", "UE4-62e1d9c640791c216f07929e04435e85-46BA238143F576601C84D8AC9E558C3E-F0161AC140D32007137383A0E58E09FE");
            request.Headers.TryAddWithoutValidation("User-Agent", "UELauncher/15.19.0-30236863+++Portal+Release-Live Windows/10.0.19045.1.256.64bit");
        }
    }
}