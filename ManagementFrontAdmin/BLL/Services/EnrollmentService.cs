using BLL.DTOs;
using BLL.IServices;
using BLL.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly HttpClient _httpClient;

        public EnrollmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<EnrollmentResponse>> GetAllEnrollments()
        {
            var response = await _httpClient.GetAsync("api/Enrollment");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var enrollments = JsonConvert.DeserializeObject<IEnumerable<EnrollmentResponse>>(jsonString);

            return enrollments!;
        }

        public async Task<Enrollment> GetEnrollmentById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Enrollment/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Enrollment>(jsonString);
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
        public async Task<Enrollment> InsertEnrollment(Enrollment obj)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Enrollment", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var enrollmentObj = JsonConvert.DeserializeObject<Enrollment>(jsonString);
                    return enrollmentObj!;
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

        public async Task<Enrollment> UpdateEnrollment(Enrollment obj)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/Enrollment/{obj.Id}", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var enrollmentObj = JsonConvert.DeserializeObject<Enrollment>(jsonString);
                    return enrollmentObj!;
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

        public async Task<Enrollment> DeleteEnrollment(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Enrollment/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var enrollmentObj = JsonConvert.DeserializeObject<Enrollment>(jsonString);
                    return enrollmentObj!;
                }
                else if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    return new Enrollment();
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
