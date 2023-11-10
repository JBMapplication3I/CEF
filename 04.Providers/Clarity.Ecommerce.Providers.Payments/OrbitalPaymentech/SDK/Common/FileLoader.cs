#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace JPMC.MSDK.Common
{
    public class FileLoader : IFileLoader
    {
        private string homeDir;

        public FileLoader( string homeDir )
        {
            this.homeDir = homeDir;
        }

        public string LoadFile( string name )
        {
            return null;
        }

        public string GetFileName( string filter )
        {
            return null;
        }
    }
}
