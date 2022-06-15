using ApartmentsParser.BusinessLogic.Interfaces;
using ApartmentsParser.Domain.ConfigurationObjects;
using Microsoft.Extensions.Configuration;
using Quartz;
using System;
using System.Threading.Tasks;

namespace ApartmentsParser.JobsRunner.Jobs
{
    public class OtodomUploader : IJob
    {
        private readonly IApartmentsParser _otodomParser;
        private readonly IConfiguration _configuration;

        public OtodomUploader(IApartmentsParser otodomParser, IConfiguration configuration)
        {
            _otodomParser = otodomParser ?? throw new ArgumentNullException(nameof(otodomParser));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var otodomConfiguration = new JobsConfiguration();
            _configuration.GetSection("OtodomJobsConfiguration").Bind(otodomConfiguration);

            foreach (var city in otodomConfiguration.ListOfCities)
            {
                await _otodomParser.ParsePages(city, otodomConfiguration.NumberOfPages);
            }
        }
    }
}
