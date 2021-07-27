using System;
using Hangfire;
using Liar.HangFire.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace Liar.HangFire
{
    public static class BackgroundJobsExtensions
    {
        /// <summary>
        /// Test
        /// </summary>
        /// <param name="service"></param>
        public static void UseTestJob(this IServiceProvider service)
        {
            var job = service.GetService<TestJob>();

            RecurringJob.AddOrUpdate("测试", () => job.ExecuteAsync(), CronType.Hour(1, 3));
        }
    }
}
