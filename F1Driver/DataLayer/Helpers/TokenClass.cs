using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Helpers
{
    public static class TokenClass
    {
		public static JwtSecurityToken CreateToken(Claim[] claims, IConfiguration config)
		{
			var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["Secret"]));

			var tokenHandler = new JwtSecurityTokenHandler();

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

			return token;
		}

		public static string WriteToken(JwtSecurityToken token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			return tokenHandler.WriteToken(token);
		}

		public static JwtSecurityToken Verify(string jwt, IConfiguration config)
		{
			try
			{
				var tokenHandler = new JwtSecurityTokenHandler();
				var key = Encoding.ASCII.GetBytes(config["Secret"]);

				tokenHandler.ValidateToken(jwt, new TokenValidationParameters
				{
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuerSigningKey = true,
					ValidateIssuer = false,
					ValidateAudience = false

				}, out SecurityToken validatedToken);

				return (JwtSecurityToken)validatedToken;
			}

			catch (Exception ex)
			{
				throw new Exception($"INCORRECT TOKEN:. {ex.Message}");
			}
		}
	}
}
