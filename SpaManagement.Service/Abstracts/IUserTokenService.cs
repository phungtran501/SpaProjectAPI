using SpaManagement.Domain.Entities;

namespace SpaManagement.Service.Abstracts
{
    public interface IUserTokenService
    {
        Task<UserToken> CheckRefreshToken(string code);
        Task SaveToken(UserToken userToken);
    }
}