using MagicVillaAPICourse.Models;
using MagicVillaAPICourse.Models.Dto;

namespace MagicVillaAPICourse.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDto> Login (LoginRequestDto loginRequest);
        Task<LocalUser> Register(RegistrationRequestDto registrationRequest);

    }
}
