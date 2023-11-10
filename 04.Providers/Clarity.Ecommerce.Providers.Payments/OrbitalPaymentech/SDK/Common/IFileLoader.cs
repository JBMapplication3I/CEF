#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace JPMC.MSDK.Common
{
    public interface IFileLoader
    {
        string LoadFile( string name );
        string GetFileName( string filter );
    }
}
