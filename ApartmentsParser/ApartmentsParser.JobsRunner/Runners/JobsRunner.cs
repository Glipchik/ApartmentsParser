using ApartmentsParser.JobsRunner.Jobs;
using Quartz;
using Quartz.Impl;

namespace ApartmentsParser.JobsRunner.Runners
{
    public class JobsRunner
    {
        public static async void Start(int timeLapse)
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<ApartmentsUploader>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger", "group")
                .StartNow()
                .WithSimpleSchedule(s => s
                .WithIntervalInSeconds(timeLapse)
                .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
