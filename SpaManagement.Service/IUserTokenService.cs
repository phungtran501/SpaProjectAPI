using SpaManagement.Domain.Entities;

namespace SpaManagement.Service
{
    public interface IUserTokenService
    {
        Task<UserToken> CheckRefreshToken(string code);
        Task SaveToken(UserToken userToken);
    }
}