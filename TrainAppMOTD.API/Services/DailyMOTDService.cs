

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http.Headers;
using TrainAppMOTD.API.DataBase;
using TrainAppMOTD.API.Models;
using TrainAppMOTD.API.Services.Inteface;

namespace TrainAppMOTD.API.Services
{

    public class DailyMOTDService : IDailyMOTDService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpClient _client;

        public DailyMOTDService(HttpClient client, IUnitOfWork unitOfWork)
        {
            _client = client;
            _unitOfWork = unitOfWork;
        }

        public void AddDailyMessageToBase(DailyMOTD dailyMOTD)
        {
            _unitOfWork.motdRepository.AddNewDailyMOTD(dailyMOTD);
        }

        public async Task<DailyMOTD> ReadDailyMessageFromBase(DateTime dateTime)
        {
            var dailyMessages = await _unitOfWork.motdRepository.GetDailyMOTD(dateTime);
            return dailyMessages;
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
