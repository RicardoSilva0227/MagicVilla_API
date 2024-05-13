using MagicVillaWebProject.Models;
using MagicVillaWebProject.Models.Dto;
using MagicVillaWebProject.Services.IServices;

namespace MagicVillaWebProject.Services
{
    public class VillaNumberService : BaseService, IVillaNumberService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string villaUrl;

        public VillaNumberService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaNumberCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType =  SD.SD.ApiType.POST,
                Data = dto, 
                Url = villaUrl+ "/api/v1/VillaNumber",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.SD.ApiType.DELETE,
                Url = villaUrl + "/api/v1/VillaNumber/" + id,
                Token = token
            });   
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.SD.ApiType.GET,
                Url = villaUrl + "/api/v1/VillaNumber",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.SD.ApiType.GET,
                Url = villaUrl + "/api/v1/VillaNumber/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(VillaNumberUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.SD.ApiType.PUT,
                Data = dto,
                Url = villaUrl + "/api/v1/VillaNumber/" + dto.VillaNo,
                Token = token
            });
        }
    }
}
