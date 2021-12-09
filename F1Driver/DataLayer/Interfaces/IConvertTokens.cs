using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IConvertTokens
    {
        public Task<UserModel> TokenConverterMethod(string token);
    }
}
