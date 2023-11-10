#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using log4net;

namespace JPMC.MSDK.Common
{
    public interface ILoggingWrapper
    {
        void ConfigureLogging( bool loadAndWatch, string homeDir );
        ILog EngineLogger { get; }
        ILog DetailLogger { get; }
        bool IsConfigured { get; }
    }
}
