using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Social.Google.Model;

namespace Social.Google
{   
    public class Client
    {
        private string _baseUrl { get; }
        private HttpClient _httpClient { get; set; }
        public Client(string baseUrl)
        {
            this._baseUrl =  baseUrl;
            this._httpClient = this.getWSClient();
        }

        public async Task<GGValidateResult> ValidateAccessToken(string tokenKey)
        {
            var url = "tokeninfo?id_token=" + tokenKey;
            HttpResponseMessage response = await this._httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsAsync<GGValidateResult>();
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
