using SpaManagement.Service.DTOs;

namespace SpaManagement.Service.Abstracts
{
    public interface IAccountService
    {
        Task<ResponseModel> CreateUpdate(AccountDTO accountDTO);
        Task DeleteAccount(string userId);
        Task<AccountDTO> GetAccountById(string userId);
        Task<IEnumerable<AccountDTO>> GetAllListAccount(int pageIndex, int pageSize);
        Task<IEnumerable<AccountDTO>> GetUsers();
    }
}