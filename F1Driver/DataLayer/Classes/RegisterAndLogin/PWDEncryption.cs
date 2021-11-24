using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Classes.RegisterAndLogin
{
    class PWDEncryption
    {
        private IConfiguration _config;

        public PWDEncryption(IConfiguration config)
        {
            _config = config;
        }

        public string EncryptPassword(string password)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string unHashed = password + _config["Secret"];
            byte[] data = MD5.Create().ComputeHash(Encoding.Default.GetBytes(unHashed));
            for (int i = 0; i < data.Length; i++)
            {
                stringBuilder.Append(data[i].ToString("x2"));
            }
            return stringBuilder.ToString();

        }
    }
}
