using MagicVillaWebProject.Models.Dto;

namespace MagicVillaWebProject.Services.IServices
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDto loginRequest);
        Task<T> RegisterAsync<T>(RegistrationRequestDto user);
    }
}
