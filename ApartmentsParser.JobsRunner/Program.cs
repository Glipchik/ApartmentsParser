using ApartmentsParser.BusinessLogic.DI;
using ApartmentsParser.DataAccess.DI;
using ApartmentsParser.Domain.ConfigurationObjects;
using ApartmentsParser.JobsRunner.Jobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;

namespace ApartmentsParser.JobsRunner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                    var olxConfiguration = new OlxJobsConfiguration();

                    configuration.GetSection("OlxJobsConfiguration").Bind(olxConfiguration);

                    services.AddDataLogic(configuration);
                    services.AddBusinessLogic();

                    services.AddQuartz(q =>
                    {
                        q.UseMicrosoftDependencyInjectionJobFactory();

                        var jobKey = new JobKey("OlxParserJob");

                        q.AddJob<OlxUploader>(opts => opts.WithIdentity(jobKey));

                        q.AddTrigger(opts => opts
                            .ForJob(jobKey)
                            .WithIdentity("OlxParserJob-TimerTrigger")
                            .WithCronSchedule(olxConfiguration.RepeatInterval));

                        q.AddTrigger(opts => opts
                            .ForJob(jobKey) 
                            .WithIdentity("OlxParserJob-StartNowTrigger")
                            .StartNow());
                    });

                    services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

                    services.Configure<OlxJobsConfiguration>(configuration.GetSection("OlxJobsConfiguration"));
                });
    }
}
