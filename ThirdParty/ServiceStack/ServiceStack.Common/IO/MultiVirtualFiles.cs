using ServiceStack.VirtualPath;

namespace ServiceStack.IO
{
    public class MultiVirtualFiles
#pragma warning disable 618
        : MultiVirtualPathProvider
#pragma warning restore 618
    {
        public MultiVirtualFiles(params IVirtualPathProvider[] childProviders) : base(childProviders) {}
    }
}