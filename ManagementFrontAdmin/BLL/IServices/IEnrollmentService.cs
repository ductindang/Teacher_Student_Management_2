using BLL.DTOs;
using BLL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IEnrollmentService
    {
        public Task<IEnumerable<EnrollmentResponse>> GetAllEnrollments();
        public Task<Enrollment> GetEnrollmentById(int id);
        public Task<Enrollment> InsertEnrollment(Enrollment obj);
        public Task<Enrollment> UpdateEnrollment(Enrollment obj);
        public Task<Enrollment> DeleteEnrollment(int id);
    }
}
