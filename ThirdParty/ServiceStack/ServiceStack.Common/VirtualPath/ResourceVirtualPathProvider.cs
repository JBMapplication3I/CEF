﻿using System;
using System.Reflection;
using ServiceStack.DataAnnotations;
using ServiceStack.IO;

namespace ServiceStack.VirtualPath
{
    [Obsolete("Renamed to ResourceVirtualFiles")]
    public abstract class ResourceVirtualPathProvider : AbstractVirtualPathProviderBase
    {
        protected ResourceVirtualDirectory RootDir;
        protected Assembly BackingAssembly;
        protected string RootNamespace;

        public override IVirtualDirectory RootDirectory => RootDir;
        public override string VirtualPathSeparator => "/";
        public override string RealPathSeparator => ".";
        
        public DateTime LastModified { get; set; } 

        public ResourceVirtualPathProvider(Type baseTypeInAssmebly)
            : this(baseTypeInAssmebly.GetAssembly(), GetNamespace(baseTypeInAssmebly)) { }

        public ResourceVirtualPathProvider(Assembly backingAssembly, string rootNamespace=null)
        {
            BackingAssembly = backingAssembly ?? throw new ArgumentNullException(nameof(backingAssembly));
            RootNamespace = rootNamespace ?? backingAssembly.GetName().Name;

            Initialize();
        }

        private static string GetNamespace(Type type)
        {
            var attr = type.FirstAttribute<SchemaAttribute>();
            return attr != null ? attr.Name : type.Namespace;
        }

        protected sealed override void Initialize()
        {
            var asm = BackingAssembly;
            RootDir = new(this, null, asm, LastModified, RootNamespace);
        }

        public override string CombineVirtualPath(string basePath, string relativePath)
        {
            return string.Concat(basePath, VirtualPathSeparator, relativePath);
        }
    }
}
