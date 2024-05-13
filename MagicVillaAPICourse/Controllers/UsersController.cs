using MagicVillaAPICourse.Models;
using MagicVillaAPICourse.Models.Dto;
using MagicVillaAPICourse.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVillaAPICourse.Controllers
{
    [Route("api/v1/UsersAuth")]
    [ApiController]
    [ApiVersionNeutral]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        protected APIResponse _response;
        public UsersController(IUserRepository userRepostory)
        {
            _userRepository = userRepostory;
            _response = new();
        }

        #region Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _userRepository.Login(model);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username or password is incorrect");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.result = loginResponse;
            return Ok(_response);
        }
        #endregion

        #region Registration
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto Model)
        {
            bool ifUserNameUnique = _userRepository.IsUniqueUser(Model.UserName);
            if (!ifUserNameUnique)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username Already Exists!");
                return BadRequest(_response);
            }

            var user = await _userRepository.Register(Model);
            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Error while resgitering!");
                return View(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);
        }
        #endregion
    }
}
