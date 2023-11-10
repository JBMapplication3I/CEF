#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace JPMC.MSDK
{
    public enum CommModule
    {
        Unknown,
        TCPConnect,
        HTTPSConnect,
        PNSConnect,
        TCPBatch,
        SFTPBatch,
        HTTPSUpload,
        PNSUpload
    }
}
