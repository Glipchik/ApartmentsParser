using ApartmentsParser.BusinessLogic.DI;
using ApartmentsParser.DataAccess.DI;
using ApartmentsParser.Domain.ConfigurationObjects;
using ApartmentsParser.JobsRunner.Jobs;
using Microsoft.Extensions.Configuration;
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
                    var otodomConfiguration = new JobsConfiguration();
                    configuration.GetSection("OtodomJobsConfiguration").Bind(otodomConfiguration);
                    services.AddDataLogic(configuration);
                    services.AddBusinessLogic();
                    services.AddQuartz(q =>
                    {
                        q.UseMicrosoftDependencyInjectionJobFactory();

                        var jobKey = new JobKey("OtodomParserJob");

                        q.AddJob<OtodomUploader>(opts => opts.WithIdentity(jobKey));

                        q.AddTrigger(opts => opts
                            .ForJob(jobKey)
                            .WithIdentity("OtodomParserJob-TimerTrigger")
                            .WithCronSchedule(otodomConfiguration.RepeatInterval));

                        q.AddTrigger(opts => opts
                            .ForJob(jobKey)
                            .WithIdentity("OtodomParserJob-StartNowTrigger")
                            .StartNow());
                    });
                    services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
                });
    }
}