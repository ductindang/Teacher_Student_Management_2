using DAL.Models;

namespace DAL.Repositories.IRepository
{
    public interface IClassImageRepository : IGenericRepository<ClassImage>
    {
        Task<ClassImage> GetClassImageByClass(int classId);
    }
}
