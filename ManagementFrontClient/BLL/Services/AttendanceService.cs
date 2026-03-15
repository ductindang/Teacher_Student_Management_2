using BLL.DTOs;
using BLL.IServices;
using BLL.Request;
using BLL.Response;
using Newtonsoft.Json;
using System.Text;

namespace BLL.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly HttpClient _httpClient;

        public AttendanceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<StudentAttendanceResponse>> GetStudentsBySchedule(int scheduleId)
        {
            var response = await _httpClient.GetAsync($"api/attendance/schedule/{scheduleId}");

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            var students = JsonConvert.DeserializeObject<IEnumerable<StudentAttendanceResponse>>(jsonString);

            return students!;
        }

        public async Task<bool> TakeAttendance(AttendanceRequest request)
        {
            try
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(request),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync("api/attendance/UpdateAttendance", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<AttendanceScheduleClassResponse>> GetAllAttendanceDetails(int studentId, int classId)
        {
            var response = await _httpClient.GetAsync($"api/attendance/student/{studentId}/class/{classId}");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var classes = JsonConvert.DeserializeObject<IEnumerable<AttendanceScheduleClassResponse>>(jsonString);

            return classes!;
        }
    }
}
