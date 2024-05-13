using MagicVillaWebProject.Models;
using MagicVillaWebProject.Models.Dto;
using MagicVillaWebProject.Services.IServices;

namespace MagicVillaWebProject.Services
{
    public class VillaService : BaseService, IVillaService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string villaUrl;

        public VillaService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType =  SD.SD.ApiType.POST,
                Data = dto, 
                Url = villaUrl+ "/api/v1/villaAPI",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.SD.ApiType.DELETE,
                Url = villaUrl + "/api/v1/villaAPI/" + id,
                Token = token
            });   
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.SD.ApiType.GET,
                Url = villaUrl + "/api/v1/villaAPI",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.SD.ApiType.GET,
                Url = villaUrl + "/api/v1/villaAPI/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.SD.ApiType.PUT,
                Data = dto,
                Url = villaUrl + "/api/v1/villaAPI/" + dto.Id,
                Token = token
            });
        }
    }
}
