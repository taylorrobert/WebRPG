using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Services
{
    public class APIService
    {
        public HttpClient _client;

        public APIService()
        {
            _client = new HttpClient();

            _client.BaseAddress = new Uri("http://localhost:55564/api/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> Ping()
        {
            HttpResponseMessage response = await _client.GetAsync("Dev");

            if (response.IsSuccessStatusCode)
            {
                var ping = await response.Content.ReadAsStringAsync();
                return ping;
            }
            return "Unable to contact server.";
        }
    }
}
