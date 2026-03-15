using BLL.DTOs;
using BLL.Response;

namespace BLL.IServices
{
    public interface IClassService
    {
        public Task<IEnumerable<ClassResponse>> GetAllClasses();
        public Task<ClassResponse> GetClassById(int id);
        public Task<Class> InsertClass(Class obj);
        public Task<Class> UpdateClass(Class obj);
        public Task<Class> DeleteClass(int id);

        public Task<ClassImage?> UpdateClassImage(ClassImage classImage);
    }
}
