using DAL.Models;
using DAL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepository
{
    public interface IEnrollmentRepository : IGenericRepository<Enrollment>
    {
        public Task<IEnumerable<EnrollmentResponse>> GetAllEnrollmentDetail();
        public Task<EnrollmentResponse> GetErollmentByClassAndStudent(int classId, int studentId);
        Task<Enrollment> GetEnrollmentByStudent(int studentId);
        Task<Enrollment> GetEnrollmentByClass(int classId);
    }
}
