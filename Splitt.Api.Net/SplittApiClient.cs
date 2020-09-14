using Splitt.Api.Net.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Splitt.Api.Net
{
    public class SplittApiClient
    {
        private HttpClient _client;
        private string _apiKey;
        private string _baseApiUrl = "https://api-sandbox.splitt.nl";
        public SplittApiClient(string apiKey, bool useSandbox = true)
        {
            _client = new HttpClient();
            _apiKey = apiKey;
            if (!useSandbox)
                _baseApiUrl = "https://api.splitt.nl";
        }
        public async Task<MerchantResponse> CreateMerchantAsync(MerchantRequest request)
        {
            var requestDict = new Dictionary<string, object>();
            var requestList = new List<MerchantRequest>();
            requestList.Add(request);
            requestDict.Add("merchants", requestList);
            var response = await SendPostAsync("/provider/merchant", JsonConvert.SerializeObject(requestDict));
            var rawResponseBody = await response.Content.ReadAsStringAsync();
            var dictData = JsonConvert.DeserializeObject<Dictionary<string, object>>(rawResponseBody);
            var rawResponseDataBody = Convert.ToString(dictData["response"]);
            var listResponse = JsonConvert.DeserializeObject<List<MerchantResponse>>(rawResponseDataBody);
            return listResponse[0];
        }
        public async Task<List<MerchantResponse>> CreateMerchantsAsync(List<MerchantRequest> requests)
        {
            var requestDict = new Dictionary<string, object>();
            requestDict.Add("merchants", requests);
            var response = await SendPostAsync("/provider/merchant", JsonConvert.SerializeObject(requestDict));
            var rawResponseBody = await response.Content.ReadAsStringAsync();
            var dictData = JsonConvert.DeserializeObject<Dictionary<string, object>>(rawResponseBody);
            var rawResponseDataBody = Convert.ToString(dictData["response"]);
            var listResponse = JsonConvert.DeserializeObject<List<MerchantResponse>>(rawResponseDataBody);
            return listResponse;
        }
        public async Task<MerchantResponse> GetMerchantAsync(string merchantId)
        {
            var response = await SendGetAsync("/provider/merchant/" + merchantId, "");
            var rawResponseBody = await response.Content.ReadAsStringAsync();
            var dictData = JsonConvert.DeserializeObject<Dictionary<string, object>>(rawResponseBody);
            var rawResponseDataBody = Convert.ToString(dictData["response"]);
            return JsonConvert.DeserializeObject<MerchantResponse>(rawResponseDataBody);
        }
        public async Task<TransactionResponse> CreateTransactionAsync(TransactionRequest request)
        {
            var response = await SendPostAsync("/merchant/transaction", JsonConvert.SerializeObject(request));
            var rawResponseBody = await response.Content.ReadAsStringAsync();
            var dictData = JsonConvert.DeserializeObject<Dictionary<string, object>>(rawResponseBody);
            var rawResponseDataBody = Convert.ToString(dictData["response"]);
            return JsonConvert.DeserializeObject<TransactionResponse>(rawResponseDataBody);
        }
        public async Task<TransactionResponse> GetTransactionAsync(string transactionId)
        {
            var response = await SendGetAsync("/merchant/transaction/" + transactionId, "");
            var rawResponseBody = await response.Content.ReadAsStringAsync();
            var dictData = JsonConvert.DeserializeObject<Dictionary<string, object>>(rawResponseBody);
            var rawResponseDataBody = Convert.ToString(dictData["response"]);
            return JsonConvert.DeserializeObject<TransactionResponse>(rawResponseDataBody);
        }
        private async Task<HttpResponseMessage> SendGetAsync(string url, string body)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, string.Format("{0}{1}", _baseApiUrl, url));
            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
            httpRequest.Content = new StringContent(body);
            var httpResponse = await _client.SendAsync(httpRequest);
            httpResponse.EnsureSuccessStatusCode();
            return httpResponse;
        }
        private async Task<HttpResponseMessage> SendPostAsync(string url, string body)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, string.Format("{0}{1}", _baseApiUrl, url));
            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
            httpRequest.Content = new StringContent(body);
            var httpResponse = await _client.SendAsync(httpRequest);
            httpResponse.EnsureSuccessStatusCode();
            return httpResponse;
        }
    }
}