using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SpaManagement.Authentication.Service;
using SpaManagement.DTOs;
using SpaManagement.Service;

namespace SpaManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        IUserService _customerService;
        ITokenHandler _tokenHandler;
        IUserTokenService _userTokenService;
        public AuthenticationController(IUserService customerService, ITokenHandler tokenHandler, IUserTokenService userTokenService)
        {
            _customerService = customerService;
            _tokenHandler = tokenHandler;
            _userTokenService = userTokenService;
        }
        [HttpPost("login")] //api/authentication/login
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AccountModel accountModel)  //{}, const user = { 'username': 'abc', password: '123'}
        {
            if (accountModel is null)
            {
                return BadRequest("User is not exist");
            }

            var user = await _customerService.CheckLogin(accountModel.Username, accountModel.Password);

            if (user is null)
            {
                return NotFound();
            }

            (string accessToken, DateTime expiredDateAccess) = await _tokenHandler.CreateAccessToken(user);
            //(string code, string refreshToken, DateTime expiredDateRefresh) = await _tokenHandler.CreateRefreshToken(user);

            //await _userTokenService.SaveToken(new Domain.Entities.UserToken
            //{
            //    AccessToken = accessToken,
            //    RefreshToken = refreshToken,
            //    CodeRefreshToken = code,
            //    ExpiredDateAccessToken = expiredDateAccess,
            //    ExpiredDateRefreshToken = expiredDateRefresh,
            //    CreatedToken = DateTime.Now,
            //    UserId = user.Id
            //});

            return Ok(new JwtModel
            {
                AccessToken = accessToken,
                Fullname = user.Fullname,
                Username = user.UserName,
                AccessTokenExpiredDate = expiredDateAccess.ToString("yyyy/MM/dd hh:mm:ss")
            });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel token)
        {
            return Ok(await _tokenHandler.ValidateRefreshToken(token.RefreshToken));
        }
    }
}
