using cw_8_22c.Models;
using cw_8_22c.Models.DTOs;
using cw_8_22c.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace cw_8_22c.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserDTO request)
        {
            User user = new User()
            {
                Login = request.Login,
                Password = request.Password,
                RefreshToken = null
            };
            var hasher = new PasswordHasher<User>();
            var hashedPassword = hasher.HashPassword(user, user.Password);

            bool doesUserExist = await _accountService.DoesUserExist(request.Login);
            if (!doesUserExist)
                return Unauthorized("No user with the specified login.");

            bool isPasswordCorrect = await _accountService.IsPasswordCorrect(user.Login, hashedPassword);
            if (!isPasswordCorrect)
                return Unauthorized("Incorrect password.");

            var token = _accountService.GenerateBearerToken();

            var refreshToken = _accountService.GenerateRefreshToken();

            bool successfultRefreshTokenUpdate = await _accountService.UpdateRefreshToken(user.Login, refreshToken);

            if (successfultRefreshTokenUpdate)
                return BadRequest("error updating refresh token");
            return Ok(new
            {
                token = token,
                refreshToken
            });
        }
        [HttpPost]
        [Route("/[controller]/refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            var newToken = await _accountService.RefreshToken(tokenModel);
            if (newToken is null)
                return NotFound("no session with given refreshToken");
            return Ok(newToken);

        }
    }
}
