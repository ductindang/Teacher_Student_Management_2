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
    public class TeacherService : ITeacherService
    {
        private readonly HttpClient _httpClient;

        public TeacherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Teacher>> GetAllTeachers()
        {
            var response = await _httpClient.GetAsync("api/teacher");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var teachers = JsonConvert.DeserializeObject<IEnumerable<Teacher>>(jsonString);

            return teachers!;
        }

        public async Task<Teacher> GetTeacherById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/teacher/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Teacher>(jsonString);
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

        public async Task<Teacher> GetTeacherByUserId(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/teacher/userId/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Teacher>(jsonString);
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
        public async Task<Teacher> InsertTeacher(Teacher obj)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/teacher", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var teacherObj = JsonConvert.DeserializeObject<Teacher>(jsonString);
                    return teacherObj!;
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

        public async Task<Teacher> UpdateTeacher(Teacher obj)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/teacher/{obj.Id}", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var teacherObj = JsonConvert.DeserializeObject<Teacher>(jsonString);
                    return teacherObj!;
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

        public async Task<Teacher> UpdateAccount(int teacherId, int userId)
        {
            try
            {
                var response = await _httpClient.PutAsync($"api/teacher/UpdateAccount?teacherId={teacherId}&userId={userId}", null);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var teacherObj = JsonConvert.DeserializeObject<Teacher>(jsonString);
                    return teacherObj!;
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

        public async Task<Teacher> DeleteTeacher(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/teacher/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var teacherObj = JsonConvert.DeserializeObject<Teacher>(jsonString);
                    return teacherObj!;
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
