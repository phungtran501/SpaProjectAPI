﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpaManagement.Domain.Entities;
using SpaManagement.Domain.Enums;
using SpaManagement.Service.Abstracts;
using SpaManagement.Service.DTOs;

namespace SpaManagement.Controllers
{

    public class AccountController : BaseController
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

                return Ok(result);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(string userId)
        {
            await _accountService.DeleteAccount(userId);
            return Ok(true);
        }

        [HttpGet("get-accounts")]
        public async Task<IActionResult> GetAccounts()
        {
            var accounts = await _accountService.GetUsers();

            return Ok(accounts);
        }
    }
}
