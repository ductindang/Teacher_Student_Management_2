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
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var response = await _httpClient.GetAsync("api/user");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(jsonString);

            return users!;
        }

        public async Task<User> GetUserByEmailPass(string email, string password)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/user/email_pass?email={Uri.EscapeDataString(email)}&password={Uri.EscapeDataString(password)}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<User>(jsonString);

                    return user!;
                }else if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
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
                Console.WriteLine($"An error occured: {ex.Message}");
                return null!;
            }
        }

        public async Task<User> InsertUser(User user)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/user", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var userObj = JsonConvert.DeserializeObject<User>(jsonString);
                    return userObj!;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return null!;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
                return null!;
            }
        }

        public async Task<User> UpdateUser(User user)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/user/{user.Id}", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var userObj = JsonConvert.DeserializeObject<User>(jsonString);
                    return userObj!;
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

        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/User/Email?email={Uri.EscapeDataString(email)}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<User>(jsonString);
                    return user!;
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

        public async Task<User> GetUserById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/User/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<User>(jsonString);
                    return user!;
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
    }
}
