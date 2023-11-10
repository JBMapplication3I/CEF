namespace CommonUtils.Cron
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CronInfo
    {
        public CronInfo()
        {
            this.CurrentDayQueue = DateTime.Now.Date;
        }

        public CronInfo(string expr)
        {
            var cronInfo = CronParser.Instance.Parse(expr);
            this.DaysOfMonth = cronInfo.DaysOfMonth;
            this.DaysOfWeek = cronInfo.DaysOfWeek;
            this.Expr = cronInfo.Expr;
            this.ExprDaysOfMonth = cronInfo.ExprDaysOfMonth;
            this.ExprDaysOfWeek = cronInfo.ExprDaysOfWeek;
            this.ExprHours = cronInfo.ExprHours;
            this.ExprMinutes = cronInfo.ExprMinutes;
            this.ExprMonths = cronInfo.ExprMonths;
            this.Hours = cronInfo.Hours;
            this.Minutes = cronInfo.Minutes;
            this.Months = cronInfo.Months;
        }

        public DateTime CurrentDayQueue { get; set; }

        public List<ushort> DaysOfMonth { get; set; }

        public List<ushort> DaysOfWeek { get; set; }

        public List<DayOfWeek> DaysOfWeekT { get; set; }

        public string Expr { get; set; }

        public string ExprDaysOfMonth { get; set; }

        public string ExprDaysOfWeek { get; set; }

        public string ExprHours { get; set; }

        public string ExprMinutes { get; set; }

        public string ExprMonths { get; set; }

        public Queue<DateTime> FiniteRunQueue { get; set; }

        public List<ushort> Hours { get; set; }

        public bool IsInfinite => this.Months.Count == 12;

        public List<ushort> Minutes { get; set; }

        public List<ushort> Months { get; set; }

        public DateTime? NextRun()
        {
            TimeSpan timeOfDay;
            bool flag;
            bool flag1;
            if (!this.IsInfinite)
            {
                if (this.CurrentDayQueue != DateTime.Now.Date)
                {
                    this.FiniteRunQueue = null;
                }

                this.FiniteRunQueue ??= this.BuildRunQueue(this);
                try
                {
                    return this.FiniteRunQueue.Dequeue();
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
            }

            var now = DateTime.Now;
            var flag2 = this.Months.Count == 0 || this.Months.Contains(Convert.ToUInt16(now.Month));
            var flag3 = this.DaysOfMonth.Count == 0 || this.DaysOfMonth.Contains(Convert.ToUInt16(now.Day));
            var flag4 = this.DaysOfWeekT.Count == 0 || this.DaysOfWeekT.Contains(now.DayOfWeek);
            if (this.Hours.Count == 0)
            {
                flag = true;
            }
            else
            {
                var hours = this.Hours;
                timeOfDay = now.TimeOfDay;
                flag = hours.Contains(Convert.ToUInt16(timeOfDay.Hours));
            }

            if (this.Minutes.Count == 0)
            {
                flag1 = true;
            }
            else
            {
                var minutes = this.Minutes;
                timeOfDay = now.TimeOfDay;
                flag1 = minutes.Contains(Convert.ToUInt16(timeOfDay.Minutes));
            }

            timeOfDay = now.TimeOfDay;
            return this.IncrementDateRecursive(
                new DateTime(now.Year, now.Month, now.Day, timeOfDay.Hours, timeOfDay.Minutes, 0).AddMinutes(1));
        }

        private Queue<DateTime> BuildRunQueue(CronInfo cronInfo)
        {
            Queue<DateTime> dateTimes;
            var dateTimes1 = new Queue<DateTime>();
            var dateTimes2 = new List<DateTime>();
            foreach (var month in cronInfo.Months)
            {
                foreach (var daysOfMonth in cronInfo.DaysOfMonth)
                {
                    foreach (var daysOfWeekT in cronInfo.DaysOfWeekT)
                    {
                        foreach (var hour in cronInfo.Hours)
                        {
                            foreach (var minute in cronInfo.Minutes)
                            {
                                try
                                {
                                    var dateTime = new DateTime(DateTime.Now.Year, month, daysOfMonth);
                                    if (dateTime.Date > this.CurrentDayQueue)
                                    {
                                        dateTimes2.Sort();
                                        (from d in dateTimes2 where d > DateTime.Now select d).ToList()
                                            .ForEach(dateTimes1.Enqueue);
                                        dateTimes = dateTimes1;
                                        return dateTimes;
                                    }

                                    if (dateTime.DayOfWeek == daysOfWeekT)
                                    {
                                        dateTime = dateTime.AddHours(hour);
                                        dateTime = dateTime.AddMinutes(minute);
                                        dateTimes2.Add(dateTime);
                                    }
                                }
                                catch (ArgumentOutOfRangeException)
                                {
                                    // Do Nothing
                                }
                            }
                        }
                    }
                }
            }

            dateTimes2.Sort();
            (from d in dateTimes2 where d > DateTime.Now select d).ToList().ForEach(dateTimes1.Enqueue);
            dateTimes = dateTimes1;
            return dateTimes;
        }

        private DateTime? IncrementDateRecursive(DateTime? dt, int it = 0)
        {
            DateTime? nullable;
            var value = dt.Value;
            var date = value.Date;
            var flag = false;
            while (!this.Months.Contains(Convert.ToUInt16(dt.Value.Month)))
            {
                value = dt.Value;
                value = value.AddMonths(1);
                var dateTime = value.Date;
                dt = new DateTime(dateTime.Year, dateTime.Month, 1);
                flag = true;
            }

            if (dt.Value.Date != date)
            {
                var num5 = it + 1;
                it = num5;
                nullable = this.IncrementDateRecursive(dt, num5);
                return nullable;
            }

            while (!this.DaysOfMonth.Contains(Convert.ToUInt16(dt.Value.Day)))
            {
                value = dt.Value;
                value = value.AddDays(1);
                dt = value.Date;
                flag = true;
            }

            if (dt.Value.Date != date)
            {
                var num4 = it + 1;
                it = num4;
                nullable = this.IncrementDateRecursive(dt, num4);
                return nullable;
            }

            while (!this.DaysOfWeekT.Contains(dt.Value.DayOfWeek))
            {
                value = dt.Value;
                value = value.AddDays(1);
                dt = value.Date;
                flag = true;
            }

            if (dt.Value.Date != date)
            {
                var num3 = it + 1;
                it = num3;
                nullable = this.IncrementDateRecursive(dt, num3);
                return nullable;
            }

            while (!this.Hours.Contains(Convert.ToUInt16(dt.Value.TimeOfDay.Hours)))
            {
                value = dt.Value;
                dt = value.AddHours(1);
                value = dt.Value;
                var timeOfDay = dt.Value.TimeOfDay;
                dt = value.AddMinutes(-timeOfDay.TotalMinutes % 60);
                flag = true;
            }

            if (dt.Value.Date != date)
            {
                var num2 = it + 1;
                it = num2;
                nullable = this.IncrementDateRecursive(dt, num2);
                return nullable;
            }

            while (!this.Minutes.Contains(Convert.ToUInt16(dt.Value.TimeOfDay.Minutes)))
            {
                value = dt.Value;
                dt = value.AddMinutes(1);
                flag = true;
            }

            if (dt.Value.Date != date)
            {
                var num1 = it + 1;
                it = num1;
                nullable = this.IncrementDateRecursive(dt, num1);
                return nullable;
            }

            if (flag)
            {
                var num = it + 1;
                it = num;
                nullable = this.IncrementDateRecursive(dt, num);
                return nullable;
            }

            return dt;
        }
    }
}