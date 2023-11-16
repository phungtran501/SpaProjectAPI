using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SpaManagement.Domain.Entities;
using SpaManagement.Domain.Enums;
using SpaManagement.Service;
using SpaManagement.Service.Abstracts;
using SpaManagement.Service.DTOs;

namespace SpaManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(IAccountService accountService, UserManager<ApplicationUser> userManager)
        {
            _accountService = accountService;
            _userManager = userManager;
        }

        [HttpGet("get-list")]
        public async Task<IActionResult> Index(int page, int per_page)
        {
            var accounts = await _accountService.GetAllListAccount(page, per_page);

            return Ok(accounts);
        }

        [HttpGet("{id}/detail")]
        public async Task<IActionResult> GetAccountDetail(string id)
        {

            var user = await _accountService.GetAccountById(id);

            return Ok(user);
        }

        [HttpPost("save")]
        public async Task<IActionResult> InsertUpdate([FromBody] AccountDTO accountDTO)
        {
            var result = await _accountService.CreateUpdate(accountDTO);
            if (result.Status && result.StatusType == StatusType.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string userId)
        {
            await _accountService.DeleteAccount(userId);
            return Json(true);
        }

        [HttpGet("get-accounts")]
        public async Task<IActionResult> GetAccounts()
        {
            var accounts = await _accountService.GetUsers();

            return Ok(accounts);
        }
    }
}
