namespace CommonUtils.Cron
{
    using System;
    using System.Threading.Tasks;

    public class CronTask
    {
        public CronInfo CronInfo { get; set; }

        public Task CurrentTask { get; set; }

        public DateTime NextRun { get; set; }

        public Action TaskAction { get; set; }
    }
}