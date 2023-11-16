namespace SpaManagement.Service.Abstracts
{
    public interface IRoleService
    {
        Task<IEnumerable<string>> GetRoles();
    }
}