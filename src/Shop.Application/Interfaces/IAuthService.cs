using Shop.Application.DTOs.Auth;

namespace Shop.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto dto);

        Task<string?> LoginAsync(LoginDto dto);
    }
}