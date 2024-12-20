﻿using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Tangy_Models;
using TangyWeb_Client.Service.IService;

namespace TangyWeb_Client.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;
        private string BaseServerUrl;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task<SuccessModelDTO> Checkout(StripePaymentDTO model)
        {
            try {
            var content = JsonConvert.SerializeObject(model);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/stripepayment/create", bodyContent);
            string responseResult = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                    //var result = JsonConvert.DeserializeObject<SuccessModelDTO>(responseResult);
                    var result = System.Text.Json.JsonSerializer.Deserialize<SuccessModelDTO>(responseResult);
                    return result;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(responseResult);
                throw new Exception(errorModel.ErrorMessage);
            }
        }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
