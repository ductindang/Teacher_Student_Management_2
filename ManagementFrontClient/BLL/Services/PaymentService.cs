using BLL.DTOs;
using BLL.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            var response = await _httpClient.GetAsync("api/Payment");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var payments = JsonConvert.DeserializeObject<IEnumerable<Payment>>(jsonString);

            return payments!;
        }

        public async Task<Payment> GetPaymentById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Payment/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Payment>(jsonString);
                    return obj!;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null!;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return null!;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null!;
            }
        }
        public async Task<Payment> InsertPayment(Payment obj)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Payment", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var paymentObj = JsonConvert.DeserializeObject<Payment>(jsonString);
                    return paymentObj!;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return null!;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
                return null!;
            }
        }

        public async Task<Payment> UpdatePayment(Payment obj)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/Payment/{obj.Id}", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var paymentObj = JsonConvert.DeserializeObject<Payment>(jsonString);
                    return paymentObj!;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return null!;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
                return null!;
            }
        }

        public async Task<Payment> DeletePayment(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Payment/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var paymentObj = JsonConvert.DeserializeObject<Payment>(jsonString);
                    return paymentObj!;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return null!;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
                return null!;
            }
        }
    }
}
