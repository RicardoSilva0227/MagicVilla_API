using MagicVillaWebProject.Models;
using MagicVillaWebProject.Models.Dto;
using MagicVillaWebProject.Services.IServices;
using Microsoft.AspNetCore.Identity.Data;

namespace MagicVillaWebProject.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string villaUrl;

        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }


        public Task<T> LoginAsync<T>(LoginRequestDto loginRequest)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.SD.ApiType.POST,
                Data = loginRequest,
                Url = villaUrl + "/api/v1/UsersAuth/login"
            });
        }

        public Task<T> RegisterAsync<T>(RegistrationRequestDto user)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.SD.ApiType.POST,
                Data = user,
                Url = villaUrl + "/api/v1/UsersAuth/register"
            });
        }
    }
}
