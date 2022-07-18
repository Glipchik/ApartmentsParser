using ApartmentsParser.BusinessLogic.Interfaces;
using ApartmentsParser.Domain.ConfigurationObjects;
using Microsoft.Extensions.Options;
using Quartz;
using System;
using System.Threading.Tasks;

namespace ApartmentsParser.JobsRunner.Jobs
{
    public class OtodomUploader : IJob
    {
        private readonly IOtodomParser _otodomParser;
        private readonly OtodomJobsConfiguration _otodomConfiguration;

        public OtodomUploader(IOtodomParser otodomParser, IOptions<OtodomJobsConfiguration> otodomConfiguration)
        {
            _otodomParser = otodomParser ?? throw new ArgumentNullException(nameof(otodomParser));
            _otodomConfiguration = otodomConfiguration.Value ?? throw new ArgumentNullException(nameof(otodomConfiguration));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            foreach (var city in _otodomConfiguration.ListOfCities)
            {
                await _otodomParser.ParsePages(city, _otodomConfiguration.NumberOfPages);
            }
        }
    }
}
