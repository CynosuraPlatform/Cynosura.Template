using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cynosura.Core.Data;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Messaging.WorkerInfos;
using Cynosura.Template.Core.Messaging.WorkerRuns;
using Cynosura.Template.Worker.Infrastructure;
using Cynosura.Template.Worker.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Cynosura.Template.Worker.WorkerInfos
{
    public class WorkerInfoSheduler
    {
        private readonly IEntityRepository<WorkerInfo> _workerInfoRepository;
        private readonly IEntityRepository<WorkerRun> _workerRunRepository;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly ILogger<WorkerInfoSheduler> _logger;

        private readonly string _scheduleTaskGroup = "ScheduleTasks";

        public WorkerInfoSheduler(IEntityRepository<WorkerInfo> workerInfoRepository,
            IEntityRepository<WorkerRun> workerRunRepository,
            ISchedulerFactory schedulerFactory,
            ILogger<WorkerInfoSheduler> logger)
        {
            _workerInfoRepository = workerInfoRepository;
            _workerRunRepository = workerRunRepository;
            _schedulerFactory = schedulerFactory;
            _logger = logger;
        }

        public async Task ScheduleAsync(int? workerInfoId = null)
        {
            var workerInfos = await _workerInfoRepository.GetEntities()
                .Include(e => e.ScheduleTasks)
                .Where(e => workerInfoId == null || e.Id == workerInfoId)
                .ToListAsync();

            var scheduler = await _schedulerFactory.GetScheduler();

            foreach (var workerInfo in workerInfos)
            {
                var jobKey = new JobKey(RunWorkerInfoJob.JobKey);
                var triggers = await scheduler.GetTriggersOfJob(jobKey);
                foreach (var trigger in triggers)
                {
                    var message = trigger.JobDataMap[QuartzData.Message] as RunWorkerInfo;
                    if (message != null && message.Id == workerInfo.Id && trigger.Key.Group == _scheduleTaskGroup)
                    {
                        await scheduler.UnscheduleJob(trigger.Key);
                    }
                }
                foreach (var scheduleTask in workerInfo.ScheduleTasks)
                {
                    var jobData = new JobDataMap
                    {
                        [QuartzData.Message] = new RunWorkerInfo
                        {
                            Id = workerInfo.Id,
                        }
                    };
                    var cronExpression = $"{(!string.IsNullOrWhiteSpace(scheduleTask.Seconds) ? scheduleTask.Seconds : "*")} " +
                        $"{(!string.IsNullOrWhiteSpace(scheduleTask.Minutes) ? scheduleTask.Minutes : "*")} " +
                        $"{(!string.IsNullOrWhiteSpace(scheduleTask.Hours) ? scheduleTask.Hours : "*")} " +
                        $"{(!string.IsNullOrWhiteSpace(scheduleTask.DayOfMonth) ? scheduleTask.DayOfMonth : "*")} " +
                        $"{(!string.IsNullOrWhiteSpace(scheduleTask.Month) ? scheduleTask.Month : "*")} " +
                        $"{(!string.IsNullOrWhiteSpace(scheduleTask.DayOfWeek) ? scheduleTask.DayOfWeek : "?")}";
                    if (!string.IsNullOrEmpty(scheduleTask.Year))
                    {
                        cronExpression += $" {scheduleTask.Year}";
                    }

                    _logger.LogInformation("Adding trigger for {JobKey}: cron {CronExpression}", jobKey, cronExpression);
                    var trigger = TriggerBuilder.Create()
                        .WithIdentity($"ScheduleTask{scheduleTask.Id}", _scheduleTaskGroup)
                        .WithCronSchedule(cronExpression)
                        .ForJob(jobKey)
                        .UsingJobData(jobData)
                        .Build();

                    await scheduler.ScheduleJob(trigger);
                }
            }
        }

        public async Task RetryAsync(int workerRunId)
        {
            var retryRun = await _workerRunRepository.GetEntities()
                .Include(e => e.WorkerInfo)
                .Where(e => e.Id == workerRunId)
                .Where(e => (e.Status == Core.Enums.WorkerRunStatus.Error && e.TriesLeft > 0) || e.NeedRetry)
                .FirstOrDefaultAsync();

            if (retryRun == null)
            {
                return;
            }

            if (retryRun.WorkerInfo.RetryCount == null || retryRun.WorkerInfo.RetryInterval == null)
            {
                return;
            }

            var scheduler = await _schedulerFactory.GetScheduler();

            var jobKey = new JobKey(RunWorkerInfoJob.JobKey);
            var runWorkerInfo = new RunWorkerInfo
            {
                Id = retryRun.WorkerInfoId,
                Data = retryRun.Data,
            };
            if (retryRun.Status == Core.Enums.WorkerRunStatus.Error)
            {
                runWorkerInfo.TriesLeft = retryRun.TriesLeft - 1;
            }
            var jobData = new JobDataMap
            {
                [QuartzData.Message] = runWorkerInfo
            };
            var nextTryDate = retryRun.EndDateTime!.Value.Add(retryRun.WorkerInfo.RetryInterval.Value);

            _logger.LogInformation("Adding trigger for {JobKey}: at {Date}", jobKey, nextTryDate);
            var trigger = TriggerBuilder.Create()
                .StartAt(nextTryDate)
                .ForJob(jobKey)
                .UsingJobData(jobData)
                .Build();
            await scheduler.ScheduleJob(trigger);
        }

        public async Task RunAsync(StartWorkerRun startWorkerRun)
        {
            var workerRun = await _workerRunRepository.GetEntities()
                .Where(e => e.Id == startWorkerRun.WorkerRunId)
                .FirstAsync();
            var scheduler = await _schedulerFactory.GetScheduler();

            var jobKey = new JobKey($"{StartWorkerRunJob.JobKey}_{workerRun.WorkerInfoId}");
            await EnsureJobExistsAsync(jobKey, scheduler);

            var jobData = new JobDataMap
            {
                [QuartzData.Message] = startWorkerRun
            };
            await scheduler.TriggerJob(jobKey, jobData);
        }

        private static async Task EnsureJobExistsAsync(JobKey jobKey, IScheduler scheduler)
        {
            var job = await scheduler.GetJobDetail(jobKey);
            if (job == null)
            {
                job = JobBuilder.Create<StartWorkerRunJob>()
                    .WithIdentity(jobKey)
                    .StoreDurably()
                    .Build();
                await scheduler.AddJob(job, false);
            }
        }
    }
}
