using DAL.Models;

namespace DAL.Repositories.IRepository
{
    public interface ITeacherRepository : IGenericRepository<Teacher>
    {
        Task<Teacher> GetTeacherByUserId(int userId);
    }
}
