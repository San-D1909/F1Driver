using ModelLayer;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IRegister
    {
        Task<JwtSecurityToken> RegisterMethod(UserModel registerRecords);
    }
}
