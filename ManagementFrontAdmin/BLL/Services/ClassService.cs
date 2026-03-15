using BLL.DTOs;
using BLL.IServices;
using BLL.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ClassService : IClassService
    {
        private readonly HttpClient _httpClient;

        public ClassService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<ClassResponse>> GetAllClasses()
        {
            var response = await _httpClient.GetAsync("api/class");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var classes = JsonConvert.DeserializeObject<IEnumerable<ClassResponse>>(jsonString);

            return classes!;
        }

        public async Task<ClassResponse> GetClassById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Class/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<ClassResponse>(jsonString);
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
        public async Task<Class> InsertClass(Class obj)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/class", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var classObj = JsonConvert.DeserializeObject<Class>(jsonString);
                    return classObj!;
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

        public async Task<Class> UpdateClass(Class obj)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/class/{obj.Id}", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var classObj = JsonConvert.DeserializeObject<Class>(jsonString);
                    return classObj!;
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

        public async Task<Class> DeleteClass(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/class/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var classObj = JsonConvert.DeserializeObject<Class>(jsonString);
                    return classObj!;
                }else if(response.StatusCode == HttpStatusCode.Conflict)
                {
                    return new Class();
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

        public async Task<ClassImage?> UpdateClassImage(ClassImage classImage)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(classImage), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/class/classImage/{classImage.ClassId}", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var classImgObj = JsonConvert.DeserializeObject<ClassImage>(jsonString);
                    return classImgObj!;
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
