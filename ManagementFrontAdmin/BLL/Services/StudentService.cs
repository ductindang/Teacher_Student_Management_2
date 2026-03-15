using BLL.DTOs;
using BLL.IServices;
using BLL.Response;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BLL.Services
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient _httpClient;

        public StudentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            var response = await _httpClient.GetAsync("api/Student");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var students = JsonConvert.DeserializeObject<IEnumerable<Student>>(jsonString);

            return students!;
        }

        public async Task<Student> GetStudentById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Student/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<Student>(jsonString);
                    return obj!;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null!;
                }
                else if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    return new Student();
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

        public async Task<IEnumerable<StudentEnrollmentRequest>> GetStudentsByClass(int classId)
        {
            var response = await _httpClient.GetAsync($"api/Student/class?classId={classId}");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var students = JsonConvert.DeserializeObject<IEnumerable<StudentEnrollmentRequest>>(jsonString);

            return students!;
        }
        public async Task<Student> InsertStudent(Student obj)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Student", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var studentObj = JsonConvert.DeserializeObject<Student>(jsonString);
                    return studentObj!;
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

        public async Task<Student> UpdateStudent(Student obj)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/Student/{obj.Id}", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var studentObj = JsonConvert.DeserializeObject<Student>(jsonString);
                    return studentObj!;
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

        public async Task<Student> UpdateAccount(int studentId, int userId)
        {
            try
            {
                var response = await _httpClient.PutAsync($"api/student/UpdateAccount?studentId={studentId}&userId={userId}", null);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var studentObj = JsonConvert.DeserializeObject<Student>(jsonString);
                    return studentObj!;
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

        public async Task<Student> DeleteStudent(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Student/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var studentObj = JsonConvert.DeserializeObject<Student>(jsonString);
                    return studentObj!;
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
