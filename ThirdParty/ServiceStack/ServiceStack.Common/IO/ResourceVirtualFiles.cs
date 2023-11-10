using System;
using System.Reflection;
using ServiceStack.VirtualPath;

namespace ServiceStack.IO
{
    public class ResourceVirtualFiles
#pragma warning disable 618
        : ResourceVirtualPathProvider
#pragma warning restore 618
    {
        public ResourceVirtualFiles(Type baseTypeInAssembly) : base(baseTypeInAssembly) {}
        public ResourceVirtualFiles(Assembly backingAssembly, string rootNamespace = null) : base(backingAssembly, rootNamespace) {}
    }
}