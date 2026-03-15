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
    public class CourseService : ICourseService
    {
        private readonly HttpClient _httpClient;

        public CourseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            var response = await _httpClient.GetAsync("api/Course");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var courses = JsonConvert.DeserializeObject<IEnumerable<Course>>(jsonString);

            return courses!;
        }

        public async Task<Course> GetCourseById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Course/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Course>(jsonString);
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
        public async Task<Course> InsertCourse(Course obj)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Course", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var courseObj = JsonConvert.DeserializeObject<Course>(jsonString);
                    return courseObj!;
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

        public async Task<Course> UpdateCourse(Course obj)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/Course/{obj.Id}", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var courseObj = JsonConvert.DeserializeObject<Course>(jsonString);
                    return courseObj!;
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

        public async Task<Course> DeleteCourse(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Course/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var courseObj = JsonConvert.DeserializeObject<Course>(jsonString);
                    return courseObj!;
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
