namespace StackExchange.Redis
{
    partial class ConnectionMultiplexer
    {
        internal SocketManager SocketManager { get; private set; }

        private bool ownsSocketManager;

        partial void OnCreateReaderWriter(ConfigurationOptions configuration)
        {
            ownsSocketManager = configuration.SocketManager == null;
            SocketManager = configuration.SocketManager ?? new SocketManager(ClientName, configuration.HighPrioritySocketThreads);
        }

        partial void OnCloseReaderWriter()
        {
            if (ownsSocketManager)
            {
                SocketManager?.Dispose();
            }

            SocketManager = null;
        }

        internal void RequestWrite(PhysicalBridge bridge, bool forced)
        {
            if (bridge == null)
            {
                return;
            }

            var tmp = SocketManager;
            if (tmp != null)
            {
                Trace("Requesting write: " + bridge.Name);
                tmp.RequestWrite(bridge, forced);
            }
        }
        partial void OnWriterCreated();
    }
}
