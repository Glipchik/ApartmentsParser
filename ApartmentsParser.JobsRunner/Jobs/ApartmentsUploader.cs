using ApartmentsParser.BusinessLogic.Interfaces;
using Microsoft.Extensions.Configuration;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApartmentsParser.JobsRunner.Jobs
{
    public class ApartmentsUploader : IJob
    {
        private readonly IOtodomParser _otodomParser;
        private readonly IConfiguration _configuration;

        public ApartmentsUploader(IOtodomParser otodomParser, IConfiguration configuration)
        {
            _otodomParser = otodomParser ?? throw new ArgumentNullException(nameof(otodomParser));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            string genericLink = "https://www.otodom.pl/pl/oferty/sprzedaz/mieszkanie/city?page=1";
            var listOfCitites = new List<string>()
            { "Warszawa", "Lodz", "Wroclaw", "Krakow", "Poznan",
                "Gdansk", "Szczecin", "Bydgoszcz", "Lublin", "Katowice" };

            int numberOfPages = _configuration.GetValue<Int32>("NumberOfPages");

            foreach (var city in listOfCitites)
            {
                await _otodomParser.ParseOtodomPages(genericLink.Replace("city", city), city, numberOfPages);
            }
        }
    }
}
