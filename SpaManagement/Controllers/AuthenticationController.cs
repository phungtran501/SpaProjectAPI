using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaManagement.Authentication.Service;
using SpaManagement.DTOs;
using SpaManagement.Service.Abstracts;

namespace SpaManagement.Controllers
{
    public class AuthenticationController : BaseController
    {
        IUserService _customerService;
        ITokenHandler _tokenHandler;

        public AuthenticationController(IUserService customerService, ITokenHandler tokenHandler)
        {
            _customerService = customerService;
            _tokenHandler = tokenHandler;
        }

        [HttpPost("login")] 
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AccountModel accountModel) 
        {
            if (accountModel is null)
            {
                return BadRequest("User is not exist");
            }

            var user = await _customerService.CheckLogin(accountModel.Username, accountModel.Password);

            if (user is null)
            {
                return BadRequest("User or password is incorrect");
            }

            (string accessToken, DateTime expiredDateAccess) = await _tokenHandler.CreateAccessToken(user);

            return Ok(new JwtModel
            {
                AccessToken = accessToken
            });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel token)
        {
            return Ok(await _tokenHandler.ValidateRefreshToken(token.RefreshToken));
        }
    }
}
