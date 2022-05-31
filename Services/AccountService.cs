using cw_8_22c.Models;
using cw_8_22c.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace cw_8_22c.Services
{
    public class AccountService : IAccountService
    {
        private readonly MedDbContext _context;

        public AccountService(MedDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DoesUserExist(string login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(lp => lp.Login == login);
            if (user == null)
                return false;
            return true;
        }


        public async Task<bool> IsPasswordCorrect(string login, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(lp => lp.Login == login && lp.Password == password);
            if (user == null)
                return false;
            return true;
        }

        public async Task<bool> UpdateRefreshToken(string login, string refreshToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(lp => lp.Login == login);
            if (user == null)
                return false;
            user.RefreshToken = refreshToken;
            _context.SaveChanges();
            return true;
        }

        public async Task<string> RefreshToken(TokenModel tokenModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.RefreshToken == tokenModel.RefreshToken);
            if (user is null)
                return null;
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            _context.SaveChanges();
            return newRefreshToken;
        }

        public string GenerateRefreshToken()
        {
            var refreshToken = "";
            using (var genNum = RandomNumberGenerator.Create())
            {
                var r = new byte[1024];
                genNum.GetBytes(r);
                refreshToken = Convert.ToBase64String(r);
            }
            return refreshToken;
        }

        public string GenerateBearerToken()
        {
            var claims = new Claim[]
            {
                new(ClaimTypes.Name, "test"),
                new("Custom", "data"),
                new Claim(ClaimTypes.Role, "admin")
            };

            var secret = "awdnpandipoawndoaiwdnosaenfgoisanfioapnfioandpawnjdpawondpawodnaodhnn";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var options = new JwtSecurityToken("https://localhost:5001", "https://localhost:5001", claims, expires: DateTime.UtcNow.AddMinutes(5), signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(options);
        }
    }

}
