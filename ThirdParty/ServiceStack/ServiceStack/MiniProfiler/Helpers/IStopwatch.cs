using System.Diagnostics;

namespace ServiceStack.MiniProfiler.Helpers
{

    internal interface IStopwatch
    {
        long ElapsedTicks { get; }
        long Frequency { get; }
        bool IsRunning { get; }
        void Stop();
    }

    internal class StopwatchWrapper : IStopwatch
    {
        public static IStopwatch StartNew()
        {
            return new StopwatchWrapper();
        }

        private Stopwatch _sw;

        private StopwatchWrapper()
        {
            _sw = Stopwatch.StartNew();
        }

        public long ElapsedTicks => _sw.ElapsedTicks;

        public long Frequency => Stopwatch.Frequency;

        public bool IsRunning => _sw.IsRunning;

        public void Stop()
        {
            _sw.Stop();
        }
    }

}
