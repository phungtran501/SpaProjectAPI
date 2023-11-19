using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpaManagement.Domain.Entities;
using SpaManagement.DTOs;

namespace SpaManagement.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController: ControllerBase
    {
        UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        PasswordHasher<ApplicationUser> _passwordHasher;
        PasswordValidator<ApplicationUser> _passwordValidator;

        public UserController(IMapper mapper ,
            UserManager<ApplicationUser> userManager, 
            PasswordHasher<ApplicationUser> passwordHasher,
            PasswordValidator<ApplicationUser> passwordValidator)
        {
            _mapper = mapper;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _passwordValidator = passwordValidator;
        }

        [HttpPost("register")] 
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserModel userVM)
        {
            if (userVM is null)
            {
                return BadRequest("Invalid Data");
            }

            var user = _mapper.Map<ApplicationUser>(userVM);

            var validationPassword = await _passwordValidator.ValidateAsync(_userManager, user, userVM.Password);

            user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {

                await _userManager.AddToRoleAsync(user, "User");
                return Ok(true);
            }
            else
                return BadRequest(result.Errors);
            
        }

    }

   
}
