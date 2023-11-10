namespace CommonUtils.Cron
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class CronScheduler : IDisposable
    {
        private bool _enabled;

        private bool disposed;

        private Timer _pollTimer;

        public CronScheduler()
        {
            this.Schedules = new List<CronTask>();
        }

        ~CronScheduler()
        {
            Dispose(false);
        }

        public bool Enabled
        {
            get => this._enabled;
            set
            {
                if (!this._enabled && value)
                {
                    this._pollTimer = new Timer(this.PollTimer_Callback, this.Schedules, 50, 200);
                }

                if (this._enabled && !value)
                {
                    if (this._pollTimer != null)
                    {
                        this._pollTimer.Dispose();
                        this.IsProcessing = false;
                    }
                }

                this._enabled = value;
            }
        }

        public bool IsProcessing { get; set; }

        public long UpdatedAt { get; private set; }

        private List<CronTask> Schedules { get; }

        public void AddTask(string expression, Action task)
        {
            var cronInfo = CronParser.ParseExpr(expression);
            var schedules = this.Schedules;
            var cronTask = new CronTask { CronInfo = cronInfo, TaskAction = task, NextRun = cronInfo.NextRun().Value };
            schedules.Add(cronTask);
        }

        public void AddTask(CronInfo ci, Action task)
        {
            var schedules = this.Schedules;
            var cronTask = new CronTask { CronInfo = ci, TaskAction = task, NextRun = ci.NextRun().Value };
            schedules.Add(cronTask);
        }

        public void AddTask(CronInfo ci, ITask task)
        {
            this.AddTask(ci, () => task.Execute());
        }

        public void Disable()
        {
            this.Enabled = false;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            this._pollTimer?.Dispose();
            foreach (var s in this.Schedules)
            {
                s.CurrentTask?.Dispose();
            }
            this.Schedules.Clear();
            disposed = true;
        }

        public void Enable()
        {
            this.Enabled = true;
        }

        public Task[] GetPendingTasks()
        {
            var array = (from s in this.Schedules
                         where s.CurrentTask != null && s.CurrentTask.Status == TaskStatus.Running
                         select s.CurrentTask).ToArray();
            return array;
        }

        private void PollTimer_Callback(object obj)
        {
            this.UpdatedAt = DateTime.Now.Ticks;
            if (!this.Enabled || this.IsProcessing)
            {
                return;
            }

            this.IsProcessing = true;
            var now = DateTime.Now;
            foreach (var schedule in this.Schedules)
            {
                if (schedule.CurrentTask != null && schedule.CurrentTask.Status == TaskStatus.Running || !this.Enabled
                    || (schedule.NextRun - now).Milliseconds >= 0)
                {
                    continue;
                }

                schedule.CurrentTask = new Task(schedule.TaskAction);
                schedule.CurrentTask.Start();
                schedule.NextRun = schedule.CronInfo.NextRun().Value;
            }

            this.IsProcessing = false;
        }
    }
}