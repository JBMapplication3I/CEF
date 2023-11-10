namespace CommonUtils.Cron.Exceptions
{
    using System;

    public class CronParserException : Exception
    {
        public CronParserException()
        {
        }

        public CronParserException(string message)
            : base(message)
        {
        }

        public CronParserException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}