using DataLayer.Helpers;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Classes.RegisterAndLogin
{
    public class Login : ILogin
    {
        private readonly ApplicationDbContext _context;
        private PWDEncryption _encryptor;
        private readonly IConfiguration _config;

        public Login(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            _encryptor = new PWDEncryption(config);
        }
        public async Task<JwtSecurityToken> Authenticate(UserModel loginRecords)
        {
            loginRecords.Password = _encryptor.EncryptPassword(loginRecords.Password);
            UserModel user = await _context.User.Where(u => u.Email == loginRecords.Email && u.Password == loginRecords.Password).FirstOrDefaultAsync();

            if (user != null)
            {
                Claim[] claims = new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                      new Claim(ClaimTypes.Email, user.Email),
                      new Claim(ClaimTypes.Name, user.UserName)

                };

                var token = TokenClass.CreateToken(claims, _config);

                return token;
            }
            else
            {
                return null;
            }
        }
    }
}
