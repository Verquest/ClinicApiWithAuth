using cw_8_22c.Models;
using cw_8_22c.Models.DTOs;
using System.Threading.Tasks;

namespace cw_8_22c.Services
{
    public interface IAccountService
    {
        public Task<bool> DoesUserExist(string login);
        public Task<bool> IsPasswordCorrect(string login, string password);
        public Task<bool> UpdateRefreshToken(string login, string refreshToken);
        public Task<string> RefreshToken(TokenModel tokenModel);
        public string GenerateRefreshToken();
        public string GenerateBearerToken();
    }
}
