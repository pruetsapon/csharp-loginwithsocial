using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Social.Facebook.Model;

namespace Social.Facebook
{   
    /// <summary>
    /// A client Printisy web service
    /// </summary>
    public class Client
    {
        private string _baseUrl { get; }
        private string _appId { get; }
        private string _secretKey { get; }
        private HttpClient _httpClient { get; set; }
        public Client(string baseUrl, string appId, string secretKey)
        {
            this._baseUrl = baseUrl;
            this._appId = appId;
            this._secretKey = secretKey;
            this._httpClient  = this.getWSClient();
        }

        public async Task<ValidateResult> ValidateAccessToken( string tokenKey)
        {
            var url = "debug_token?input_token=" + tokenKey + "&access_token=" + this._appId + "|" + this._secretKey;
            HttpResponseMessage response = await this._httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsAsync<ValidateResult>();
                return token;
            }
            return null;
        }

        private HttpClient getWSClient() {
            var client = new HttpClient();
            client.BaseAddress = new Uri(this._baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
