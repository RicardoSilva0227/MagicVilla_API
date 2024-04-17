using MagicVillaAPICourse.Data;
using MagicVillaAPICourse.Models;
using MagicVillaAPICourse.Models.Dto;
using MagicVillaAPICourse.Repository.IRepository;
using Microsoft.Extensions.Logging.Console;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MagicVillaAPICourse.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private string secretKey;

        public UserRepository(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _context = dbContext;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public bool IsUniqueUser(string username)
        {
            var user = _context.LocalUsers.FirstOrDefault(x => x.UserName == username);
            if (user == null)
                return true;

            return false;
        }

        #region Login
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequest)
        {
            var user = _context.LocalUsers.FirstOrDefault(x=>x.UserName.ToLower()==loginRequest.UserName.ToLower() 
            && x.Password.ToLower()==loginRequest.Password.ToLower());

            if (user == null)
            {
                return new LoginResponseDto()
                {
                    Token = "",
                    User = null,
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            //Storing the secret Key to Decrypt the token.
            var key = Encoding.ASCII.GetBytes(secretKey);

            //Creating the token, adding info to it (user id and role), telling how long til it expires and adding the key to that token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            LoginResponseDto loginResponse = new LoginResponseDto()
            {
                Token = tokenHandler.WriteToken(token),
                User = user,
            };
            return loginResponse;
        }
        #endregion

        #region Registration
        public async Task<LocalUser> Register(RegistrationRequestDto registrationRequest)
        {
            LocalUser user = new LocalUser()
            {
                UserName = registrationRequest.UserName,
                Password = registrationRequest.Password,
                Name = registrationRequest.Name,
                Role = registrationRequest.Role,
            };

            await _context.LocalUsers.AddAsync(user);
            await _context.SaveChangesAsync();
            user.Password = "";
            return user;
        }
        #endregion
    }
}
