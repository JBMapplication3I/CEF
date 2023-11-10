using System.IO;
using ServiceStack.VirtualPath;

namespace ServiceStack.IO
{
    public class FileSystemVirtualFiles
#pragma warning disable 618
        : FileSystemVirtualPathProvider
#pragma warning restore 618
    {
        public FileSystemVirtualFiles(string rootDirectoryPath) : base(rootDirectoryPath) {}
        public FileSystemVirtualFiles(DirectoryInfo rootDirInfo) : base(rootDirInfo) {}
    }
}
