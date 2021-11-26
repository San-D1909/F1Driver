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
    public class Register : IRegister
    {
        private readonly ApplicationDbContext _context;
        private PWDEncryption _encryptor;
        private readonly IConfiguration _config;

        public Register(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            _encryptor = new PWDEncryption(config);
        }
        public async Task<JwtSecurityToken> RegisterMethod(UserModel registerRecords)
        {
            if (registerRecords.Password.Length >= 5 && registerRecords.Password.Any(c => char.IsUpper(c)) && registerRecords.Password.Any(c => char.IsSymbol(c)) && registerRecords.Password.Any(c => char.IsDigit(c)))
            {
                registerRecords.Password = _encryptor.EncryptPassword(registerRecords.Password);
                UserModel user = await _context.User.Where(u => u.Email == registerRecords.Email && u.Password == registerRecords.Password).FirstOrDefaultAsync();
                if (user == null)
                {
                    _context.User.Add(registerRecords);
                    await _context.SaveChangesAsync();
                    Claim[] claims = new Claim[]
                    {
                     new Claim(ClaimTypes.NameIdentifier, registerRecords.ID.ToString()),
                      new Claim(ClaimTypes.Email, registerRecords.Email),
                      new Claim(ClaimTypes.Name, registerRecords.UserName)

                    };

                    var token = TokenClass.CreateToken(claims, _config);
                    return token;
                }
                return null;
            }
            else
            {
                return null;
            }
        }
    }
}
