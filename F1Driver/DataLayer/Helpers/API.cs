using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class API
    {
        readonly string baseURL = "http://ergast.com/api/f1/";
        public string RequestString
        {
            get { return requestString; }
            set { requestString = baseURL + value + ".json?"; }
        }
        public string requestString = "";
        public async Task<string> SelectJSONFromAPI(string requestString)
        {
            WebRequest requestObject = WebRequest.Create(requestString);
            requestObject.Method = "GET";
            HttpWebResponse responseObject = (HttpWebResponse)requestObject.GetResponse();
            string resultJSON = null;
            await using (Stream stream = responseObject.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                resultJSON = sr.ReadToEnd();
                sr.Close();
            }
            return resultJSON;
        }
    }
}
