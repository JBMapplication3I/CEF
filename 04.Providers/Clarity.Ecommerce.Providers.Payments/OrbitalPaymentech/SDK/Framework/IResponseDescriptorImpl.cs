#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace JPMC.MSDK.Framework
{
    public interface IResponseDescriptorImpl : IResponseDescriptor
    {
        new IResponse GetNextParsedRecord();
    }
}
