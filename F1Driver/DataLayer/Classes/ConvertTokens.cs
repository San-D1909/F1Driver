using DataLayer.Helpers;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Classes
{
    public class ConvertTokens : IConvertTokens
    {
        private readonly ApplicationDbContext _context;
        private IConfiguration _config;
        public ConvertTokens(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<UserModel> TokenConverterMethod(string token)
        {
            var user = TokenClass.Verify(token, _config);
            if (user is null)
            {
                return null;
            }
            int id = Convert.ToInt32(user.Claims.First().Value);
            UserModel userById = await _context.User.Where(user=>user.ID == id).FirstOrDefaultAsync();
            if(userById.BettingScore==null)
            {
                userById.BettingScore = 0;
            }
            return userById;
        }
    }
}
