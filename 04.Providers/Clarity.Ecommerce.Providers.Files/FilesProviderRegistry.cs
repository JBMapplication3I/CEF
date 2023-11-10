// <copyright file="FilesProviderRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the files provider registry class</summary>
#if NET5_0_OR_GREATER
namespace Clarity.Ecommerce.Providers.Files
{
    // using AWSS3;
    using Interfaces.Providers.Files;
    using Lamar;
    using LocalFileSystem;

    /// <summary>The files provider registry.</summary>
    /// <seealso cref="ServiceRegistry"/>
    [JetBrains.Annotations.PublicAPI]
    public class FilesProviderRegistry : ServiceRegistry
    {
        /// <summary>Initializes a new instance of the <see cref="FilesProviderRegistry"/> class.</summary>
        public FilesProviderRegistry()
        {
            For<IUploadResponse>().Use<UploadResponse>();
            For<IUploadResult>().Use<UploadResult>();
            Use<UploadController>().Singleton().For<IUploadController>();
            // if (AWSS3FilesProviderConfig.IsValid(false))
            // {
            //     For<IFilesProviderBase>().Singleton().Use<AWSS3FilesProvider>();
            //     return;
            // }
            if (LocalFilesProviderConfig.IsValid(false))
            {
                Use<LocalFilesProvider>().Singleton().For<IFilesProviderBase>();
            }
            // Assign default
            Use<LocalFilesProvider>().Singleton().For<IFilesProviderBase>();
        }
    }
}
#else
namespace Clarity.Ecommerce.Providers.Files
{
    // using AWSS3;
    using Interfaces.Providers.Files;
    using LocalFileSystem;
    using StructureMap;
    using StructureMap.Pipeline;

    /// <summary>The files provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class FilesProviderRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="FilesProviderRegistry"/> class.</summary>
        public FilesProviderRegistry()
        {
            For<IUploadResponse>().Use<UploadResponse>();
            For<IUploadResult>().Use<UploadResult>();
            For<IUploadController>(new SingletonLifecycle()).Use<UploadController>();
            var found = false;
            // if (AWSS3FilesProviderConfig.IsValid(false))
            // {
            //     For<IFilesProviderBase>(new SingletonLifecycle()).Add<AWSS3FilesProvider>();
            //     found = true;
            // }
            if (LocalFilesProviderConfig.IsValid(false))
            {
                For<IFilesProviderBase>(new SingletonLifecycle()).Add<LocalFilesProvider>();
                found = true;
            }
            if (found)
            {
                return;
            }
            // Assign default
            For<IFilesProviderBase>(new SingletonLifecycle()).Add<LocalFilesProvider>();
        }
    }
}
#endif
