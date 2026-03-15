using BLL.DTOs;

namespace BLL.IServices
{
    public interface IRoleService
    {
        public Task<IEnumerable<Role>> GetAllRoles();
        public Task<Role> GetRoleById(int id);
        public Task<Role> InsertRole(Role obj);
        public Task<Role> UpdateRole(Role obj);
        public Task<Role> DeleteRole(int id);
    }
}
