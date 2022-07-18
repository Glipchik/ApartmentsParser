using ApartmentsParser.BusinessLogic.Interfaces;
using ApartmentsParser.Domain.ConfigurationObjects;
using Microsoft.Extensions.Options;
using Quartz;
using System;
using System.Threading.Tasks;

namespace ApartmentsParser.JobsRunner.Jobs
{
    public class OlxUploader : IJob
    {
        private readonly IOlxParser _apartmentsParser;
        private readonly OlxJobsConfiguration _olxConfiguration;

        public OlxUploader(IOlxParser olxParser, IOptions<OlxJobsConfiguration> olxConfiguration)
        {
            _apartmentsParser = olxParser ?? throw new ArgumentNullException(nameof(olxParser));
            _olxConfiguration = olxConfiguration.Value ?? throw new ArgumentNullException(nameof(olxConfiguration));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            foreach (var city in _olxConfiguration.ListOfCities)
            {
                await _apartmentsParser.ParsePages(city, _olxConfiguration.NumberOfPages);
            }
        }
    }
}
