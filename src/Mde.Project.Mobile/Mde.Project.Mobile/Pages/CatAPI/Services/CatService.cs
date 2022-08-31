using Mde.Project.Mobile.Cat_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mde.Project.Mobile.Cat_API.Services
{
    public class CatService : ICatService
    {
        private readonly HttpClient _httpClient;
        public CatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task GetCustomCat(string CatUrl)
        {
            var response = await _httpClient.GetAsync(CatUrl);
            response.EnsureSuccessStatusCode();

        }
    }
}
