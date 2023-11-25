using SpaManagement.Service.DTOs;

namespace SpaManagement.Service.Abstracts
{
    public interface IRoleService
    {
        Task<ResponseModel> CreateUpdate(RoleDTO roleDTO);
        Task DeleteRole(string key);
        Task<IEnumerable<RoleDTO>> GetListRole(int pageIndex, int pageSize);
        Task<RoleDTO> GetRoleById(string id);
        Task<IEnumerable<string>> GetRoles();
    }
}