

using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http.Headers;

namespace TrainAppMOTD.API.Services
{
    public class DailyMOTD
    {
        private readonly HttpClient _client;

        public DailyMOTD(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> GetDailyMessageAsync()
        {
            string messageOfTheDay = null;
            HttpResponseMessage response = await _client.GetAsync("https://zenquotes.io/api/today");
            if (response.IsSuccessStatusCode)
            {
                messageOfTheDay = await response.Content.ReadAsStringAsync();
            }

            return messageOfTheDay;
        }
    }
}
