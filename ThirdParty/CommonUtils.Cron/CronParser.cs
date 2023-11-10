namespace CommonUtils.Cron
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CommonUtils.Cron.Exceptions;

    public class CronParser
    {
        internal const char ANY_TOKEN = '*';

        internal const char LIST_TOKEN = ',';

        internal const char RANGE_TOKEN = '-';

        internal const char SEGMENTS_TOKEN = '/';

        internal static readonly ushort[] DAY_RANGE;

        internal static readonly ushort[] DOW_RANGE;

        internal static readonly ushort[] HOUR_RANGE;

        internal static readonly ushort[] MINUTE_RANGE;

        internal static readonly ushort[] MONTH_RANGE;

        internal static readonly ushort[] SECOND_RANGE;

        private static readonly CronParser _instance;

        private static readonly object _sync;

        static CronParser()
        {
            _sync = new object();
            _instance = new CronParser();
            MINUTE_RANGE = null;
            SECOND_RANGE = null;
            DAY_RANGE = null;
            MONTH_RANGE = null;
            HOUR_RANGE = null;
            DOW_RANGE = null;
            MINUTE_RANGE = new ushort[]
                               {
                                   0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23,
                                   24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44,
                                   45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59
                               };
            SECOND_RANGE = new ushort[]
                               {
                                   0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23,
                                   24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44,
                                   45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59
                               };
            DAY_RANGE = new ushort[]
                            {
                                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24,
                                25, 26, 27, 28, 29, 30, 31
                            };
            MONTH_RANGE = new ushort[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            HOUR_RANGE = new ushort[]
                             {
                                 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23
                             };
            DOW_RANGE = new ushort[] { 0, 1, 2, 3, 4, 5, 6 };
        }

        public static CronParser Instance
        {
            get
            {
                CronParser cronParser;
                lock (_sync)
                {
                    cronParser = _instance;
                }

                return cronParser;
            }
        }

        public static CronInfo ParseExpr(string expr)
        {
            return Instance.Parse(expr ?? string.Format("{0} {0} {0} {0} {0}", '*'));
        }

        public CronInfo Parse(string expr)
        {
            CronInfo cronInfo;
            try
            {
                var cronInfo1 = new CronInfo();
                var strArrays = expr.Split(new[] { ' ' });
                var str = strArrays[0];
                var str1 = strArrays[1];
                var str2 = strArrays[2];
                var str3 = strArrays[3];
                var str4 = strArrays[4];
                cronInfo1.ExprMinutes = str;
                cronInfo1.ExprHours = str1;
                cronInfo1.ExprDaysOfMonth = str2;
                cronInfo1.ExprMonths = str3;
                cronInfo1.ExprDaysOfWeek = str4;
                cronInfo1.Minutes = this.ParseRangeExpr(MINUTE_RANGE, cronInfo1.ExprMinutes, ushort.TryParse);
                cronInfo1.Hours = this.ParseRangeExpr(HOUR_RANGE, cronInfo1.ExprHours, ushort.TryParse);
                cronInfo1.Months = this.ParseRangeExpr(MONTH_RANGE, cronInfo1.ExprMonths, ushort.TryParse);
                cronInfo1.DaysOfMonth = this.ParseRangeExpr(DAY_RANGE, cronInfo1.ExprDaysOfMonth, ushort.TryParse);
                cronInfo1.DaysOfWeek = this.ParseRangeExpr(DOW_RANGE, cronInfo1.ExprDaysOfWeek, ushort.TryParse);
                this.TranslateDaysOfWeek(cronInfo1);
                cronInfo = cronInfo1;
            }
            catch (IndexOutOfRangeException indexOutOfRangeException)
            {
                throw new CronParserException(
                    string.Format("Invalid number of arguments in expression [{0}]", expr),
                    indexOutOfRangeException);
            }
            catch (Exception exception)
            {
                throw new CronParserException(string.Format("Error parsing: {0}", expr), exception);
            }

            return cronInfo;
        }

        internal List<T> ParseRangeExpr<T>(IEnumerable<T> range, string p, ParseFunc<T> func)
            where T : IComparable
        {
            var ts = new List<T>();
            var chrArray = new[] { ',' };
            var strArrays = p.Split(chrArray);
            var num = 0;
            while (num < strArrays.Length)
            {
                var str = strArrays[num];
                if (!(str == '*'.ToString()))
                {
                    if (func(str, out var t))
                    {
                        ts.Add(t);
                    }
                    else if (str.Contains<char>('/'))
                    {
                        chrArray = new[] { '/' };
                        var strArrays1 = str.Split(chrArray);
                        var str1 = strArrays1[0];
                        var num1 = ushort.Parse(strArrays1[1]);
                        var ts1 = ParseRange(range, str1, func);
                        ts.AddRange(this.ApplyIncr(ts1, num1));
                    }
                    else if (str.Contains<char>('-'))
                    {
                        ts.AddRange(ParseRange(range, str, func));
                    }

                    num++;
                }
                else
                {
                    ts.AddRange(range);
                    break;
                }
            }

            return ts.Distinct().ToList();
        }

        private static IEnumerable<T> ParseRange<T>(IEnumerable<T> range, string expr, ParseFunc<T> func)
            where T : IComparable
        {
            IEnumerable<T> ts;
            var strArrays = expr.Split(new[] { '-' });
            if (strArrays.Length <= 1)
            {
                ts = range;
            }
            else
            {
                var ts1 = range.Where(
                    ri =>
                        {
                            func(strArrays[0], out var t);
                            func(strArrays[1], out var t1);
                            return ri.CompareTo(t) >= 0 && ri.CompareTo(t1) <= 0;
                        });
                ts = ts1;
            }

            return ts;
        }

        private IEnumerable<T> ApplyIncr<T>(IEnumerable<T> definedRange, ushort incr)
            where T : IComparable
        {
            var ts = new List<T>();
            var num = 0;
            var num1 = definedRange.Count();
            do
            {
                ts.Add(definedRange.ElementAt(num));
                num += incr;
            }
            while (num < num1);

            return ts;
        }

        private void TranslateDaysOfWeek(CronInfo ci)
        {
            ci.DaysOfWeekT = new List<DayOfWeek>(ci.DaysOfWeek.Count);
            foreach (var daysOfWeek in ci.DaysOfWeek)
            {
                switch (daysOfWeek % 7)
                {
                    case 0:
                    case 7:
                    {
                        ci.DaysOfWeekT.Add(DayOfWeek.Sunday);
                        break;
                    }
                    case 1:
                    {
                        ci.DaysOfWeekT.Add(DayOfWeek.Monday);
                        break;
                    }
                    case 2:
                    {
                        ci.DaysOfWeekT.Add(DayOfWeek.Tuesday);
                        break;
                    }
                    case 3:
                    {
                        ci.DaysOfWeekT.Add(DayOfWeek.Wednesday);
                        break;
                    }
                    case 4:
                    {
                        ci.DaysOfWeekT.Add(DayOfWeek.Thursday);
                        break;
                    }
                    case 5:
                    {
                        ci.DaysOfWeekT.Add(DayOfWeek.Friday);
                        break;
                    }
                    case 6:
                    {
                        ci.DaysOfWeekT.Add(DayOfWeek.Saturday);
                        break;
                    }
                }
            }

            ci.DaysOfWeekT = ci.DaysOfWeekT.Distinct().ToList();
        }
    }
}
