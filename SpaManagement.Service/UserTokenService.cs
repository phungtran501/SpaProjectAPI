using SpaManagement.Data.Abstract;
using SpaManagement.Domain.Entities;
using SpaManagement.Service.Abstracts;

namespace SpaManagement.Service
{
    public class UserTokenService : IUserTokenService
    {
        IUnitOfWork _unitOfWork;

        public UserTokenService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task SaveToken(UserToken userToken)
        {
            await _unitOfWork.UserTokenRepository.Insert(userToken);
            await _unitOfWork.UserTokenRepository.Commit();
        }

        public async Task<UserToken?> CheckRefreshToken(string code)
        {
            return await _unitOfWork.UserTokenRepository.GetSingleByConditionAsync(x => x.CodeRefreshToken == code);
        }
    }
}
