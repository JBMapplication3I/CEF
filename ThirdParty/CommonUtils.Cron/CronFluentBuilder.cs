namespace CommonUtils.Cron
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CronFluentBuilder
    {
        public CronFluentBuilder()
        {
            Minutes = new List<ushort>();
            Hours = new List<ushort>();
            DaysOfMonth = new List<ushort>();
            DaysOfWeek = new List<ushort>();
            Months = new List<ushort>();
        }

        private List<ushort> DaysOfMonth { get; }

        private List<ushort> DaysOfWeek { get; }

        private List<ushort> Hours { get; }

        private List<ushort> Minutes { get; }

        private List<ushort> Months { get; }

        public static CronFluentBuilder Build()
        {
            return new();
        }

        public CronFluentBuilder AddDaysOfMonth(params ushort[] m)
        {
            DaysOfMonth.AddRange(m);
            return this;
        }

        public CronFluentBuilder AddDaysOfWeek(params ushort[] m)
        {
            DaysOfWeek.AddRange(m);
            return this;
        }

        public CronFluentBuilder AddHours(params ushort[] m)
        {
            Hours.AddRange(m);
            return this;
        }

        public CronFluentBuilder AddMinutes(params ushort[] m)
        {
            Minutes.AddRange(m);
            return this;
        }

        public CronFluentBuilder AddMonths(params ushort[] m)
        {
            Months.AddRange(m);
            return this;
        }

        public CronFluentBuilder At(DateTime dt)
        {
            Minutes.Clear();
            Hours.Clear();
            DaysOfMonth.Clear();
            DaysOfWeek.Clear();
            Months.Clear();
            Minutes.Add(Convert.ToUInt16(dt.Minute + 1));
            Hours.Add(Convert.ToUInt16(dt.Hour));
            DaysOfMonth.Add(Convert.ToUInt16(dt.Day));
            var dayOfWeek = new[] { dt.DayOfWeek };
            SetDaysOfWeek(dayOfWeek);
            Months.Add(Convert.ToUInt16(dt.Month));
            return this;
        }

        public CronInfo GetCronInfo()
        {
            return CronParser.ParseExpr(GetExpr());
        }

        public string GetExpr()
        {
            char chr;
            string str;
            string str1;
            string str2;
            string str3;
            string str4;
            var str5 = "{0} {1} {2} {3} {4}";
            var nums = Minutes.Distinct();
            var nums1 = Hours.Distinct();
            var nums2 = DaysOfMonth.Distinct();
            var nums3 = Months.Distinct();
            var nums4 = DaysOfWeek.Distinct();
            if (nums.Count() == CronParser.MINUTE_RANGE.Length)
            {
                str = '*'.ToString();
            }
            else
            {
                chr = ',';
                str = string.Join(chr.ToString(), nums);
            }

            var str6 = str;
            if (!nums1.Any() || nums1.Count() == CronParser.HOUR_RANGE.Length)
            {
                str1 = '*'.ToString();
            }
            else
            {
                chr = ',';
                str1 = string.Join(chr.ToString(), nums1);
            }

            var str7 = str1;
            if (!nums2.Any() || nums2.Count() == CronParser.DAY_RANGE.Length)
            {
                str2 = '*'.ToString();
            }
            else
            {
                chr = ',';
                str2 = string.Join(chr.ToString(), nums2);
            }

            var str8 = str2;
            if (!nums3.Any() || nums3.Count() == CronParser.MONTH_RANGE.Length)
            {
                str3 = '*'.ToString();
            }
            else
            {
                chr = ',';
                str3 = string.Join(chr.ToString(), nums3);
            }

            var str9 = str3;
            if (!nums4.Any() || nums4.Count() == CronParser.DOW_RANGE.Length)
            {
                str4 = '*'.ToString();
            }
            else
            {
                chr = ',';
                str4 = string.Join(chr.ToString(), nums4);
            }

            var str10 = str4;
            var objArray = new object[] { str6, str7, str8, str9, str10 };
            return string.Format(str5, objArray);
        }

        public CronFluentBuilder SetDaysOfMonth(params ushort[] dom)
        {
            DaysOfMonth.AddRange(dom);
            return this;
        }

        public CronFluentBuilder SetDaysOfMonth(params string[] dom)
        {
            var strArrays = dom;
            for (var i = 0; i < strArrays.Length; i++)
            {
                var str = strArrays[i];
                var nums = CronParser.Instance.ParseRangeExpr(CronParser.DAY_RANGE, str, ushort.TryParse);
                DaysOfMonth.AddRange(nums);
            }

            return this;
        }

        public CronFluentBuilder SetDaysOfWeek(params string[] dow)
        {
            var strArrays = dow;
            for (var i = 0; i < strArrays.Length; i++)
            {
                var str = strArrays[i];
                var nums = CronParser.Instance.ParseRangeExpr(CronParser.DOW_RANGE, str, ushort.TryParse);
                DaysOfWeek.AddRange(nums);
            }

            return this;
        }

        public CronFluentBuilder SetDaysOfWeek(params ushort[] dow)
        {
            DaysOfWeek.AddRange(dow);
            return this;
        }

        public CronFluentBuilder SetDaysOfWeek(params DayOfWeek[] dow)
        {
            var nums = new List<ushort>();
            var dayOfWeekArray = dow;
            for (var i = 0; i < dayOfWeekArray.Length; i++)
            {
                switch (dayOfWeekArray[i])
                {
                    case DayOfWeek.Sunday:
                    {
                        nums.Add(0);
                        break;
                    }
                    case DayOfWeek.Monday:
                    {
                        nums.Add(1);
                        break;
                    }
                    case DayOfWeek.Tuesday:
                    {
                        nums.Add(2);
                        break;
                    }
                    case DayOfWeek.Wednesday:
                    {
                        nums.Add(3);
                        break;
                    }
                    case DayOfWeek.Thursday:
                    {
                        nums.Add(4);
                        break;
                    }
                    case DayOfWeek.Friday:
                    {
                        nums.Add(5);
                        break;
                    }
                    case DayOfWeek.Saturday:
                    {
                        nums.Add(6);
                        break;
                    }
                }
            }

            DaysOfWeek.AddRange(nums);
            return this;
        }

        public CronFluentBuilder SetHours(params string[] hours)
        {
            var strArrays = hours;
            for (var i = 0; i < strArrays.Length; i++)
            {
                var str = strArrays[i];
                var nums = CronParser.Instance.ParseRangeExpr(CronParser.HOUR_RANGE, str, ushort.TryParse);
                Hours.AddRange(nums);
            }

            return this;
        }

        public CronFluentBuilder SetHours(params ushort[] hours)
        {
            Hours.AddRange(hours);
            return this;
        }

        public CronFluentBuilder SetMinutes(params string[] mins)
        {
            var strArrays = mins;
            for (var i = 0; i < strArrays.Length; i++)
            {
                var str = strArrays[i];
                var nums = CronParser.Instance.ParseRangeExpr(CronParser.MINUTE_RANGE, str, ushort.TryParse);
                Minutes.AddRange(nums);
            }
            return this;
        }

        public CronFluentBuilder SetMinutes(params ushort[] mins)
        {
            Minutes.AddRange(mins);
            return this;
        }

        public CronFluentBuilder SetMonths(params ushort[] m)
        {
            Months.AddRange(m);
            return this;
        }

        public CronFluentBuilder SetMonths(params string[] m)
        {
            var strArrays = m;
            for (var i = 0; i < strArrays.Length; i++)
            {
                var str = strArrays[i];
                var nums = CronParser.Instance.ParseRangeExpr(CronParser.MONTH_RANGE, str, ushort.TryParse);
                Months.AddRange(nums);
            }
            return this;
        }
    }
}
