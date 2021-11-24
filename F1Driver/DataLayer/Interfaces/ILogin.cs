﻿using ModelLayer;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface ILogin
    {
        public Task<JwtSecurityToken> Authenticate(UserModel user);
    }
}
