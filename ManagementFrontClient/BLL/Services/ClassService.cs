using BLL.DTOs;
using BLL.IServices;
using BLL.Response;
using Newtonsoft.Json;
using System.Text;

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

        public async Task<IEnumerable<ClassDetailResponse>> GetClassDetailTodayByTeacherId(int teacherId)
        {
            var response = await _httpClient.GetAsync($"api/class/today-by-teacher/{teacherId}");

            var jsonString = await response.Content.ReadAsStringAsync();
            var classes = JsonConvert.DeserializeObject<IEnumerable<ClassDetailResponse>>(jsonString);

            return classes!;
        }
        public async Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailByTeacherIdInProgress(int teacherId)
        {
            var response = await _httpClient.GetAsync($"api/class/in-progress/by-teacher/{teacherId}");

            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonString);
            var classes = JsonConvert.DeserializeObject<IEnumerable<ClassDetailResponse>>(jsonString);

            return classes!;
        }
        public async Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailByTeacherIdUpcoming(int teacherId)
        {
            var response = await _httpClient.GetAsync($"api/class/upcomming/by-teacher/{teacherId}");

            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonString);
            var classes = JsonConvert.DeserializeObject<IEnumerable<ClassDetailResponse>>(jsonString);

            return classes!;
        }
        public async Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailByTeacherIdFinished(int teacherId)
        {
            var response = await _httpClient.GetAsync($"api/class/finished/by-teacher/{teacherId}");

            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonString);
            var classes = JsonConvert.DeserializeObject<IEnumerable<ClassDetailResponse>>(jsonString);

            return classes!;
        }

        public async Task<IEnumerable<ClassDetailResponse>> GetClassDetailTodayByStudentId(int studentId)
        {
            var response = await _httpClient.GetAsync($"api/class/today-by-student/{studentId}");

            var jsonString = await response.Content.ReadAsStringAsync();
            var classes = JsonConvert.DeserializeObject<IEnumerable<ClassDetailResponse>>(jsonString);

            return classes!;
        }
        public async Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailByStudentIdInProgress(int studentId)
        {
            var response = await _httpClient.GetAsync($"api/class/in-progress/by-student/{studentId}");

            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonString);
            var classes = JsonConvert.DeserializeObject<IEnumerable<ClassDetailResponse>>(jsonString);

            return classes!;
        }
        public async Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailByStudentIdUpcoming(int studentId)
        {
            var response = await _httpClient.GetAsync($"api/class/upcomming/by-student/{studentId}");

            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonString);
            var classes = JsonConvert.DeserializeObject<IEnumerable<ClassDetailResponse>>(jsonString);

            return classes!;
        }
        public async Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailByStudentIdFinished(int studentId)
        {
            var response = await _httpClient.GetAsync($"api/class/finished/by-student/{studentId}");

            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonString);
            var classes = JsonConvert.DeserializeObject<IEnumerable<ClassDetailResponse>>(jsonString);

            return classes!;
        }
        public async Task<IEnumerable<TeacherClassReviewResponse>> GetAllClassDetailAndReviewByStudentIdReview(int studentId)
        {
            var response = await _httpClient.GetAsync($"api/class/review/by-student/{studentId}");

            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonString);
            var classes = JsonConvert.DeserializeObject<IEnumerable<TeacherClassReviewResponse>>(jsonString);

            return classes!;
        }
        public async Task<IEnumerable<ClassDetailResponse>> GetAllClassDetailUpcoming()
        {
            var response = await _httpClient.GetAsync($"api/class/upcoming");

            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonString);
            var classes = JsonConvert.DeserializeObject<IEnumerable<ClassDetailResponse>>(jsonString);

            return classes!;
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
