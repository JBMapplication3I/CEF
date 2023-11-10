// <copyright file="ImporterProviderRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Importer Provider StructureMap 4 Registry to associate the interfaces with their concretes</summary>
#if NET5_0_OR_GREATER
namespace Clarity.Ecommerce.Providers.Importer
{
    using CSV;
    using Excel;
    // using GoogleSheet;
    using Interfaces.Providers.Importer;
    using Lamar;

    /// <summary>The Importer provider registry.</summary>
    /// <seealso cref="ServiceRegistry"/>
    [JetBrains.Annotations.PublicAPI]
    public class ImporterProviderRegistry : ServiceRegistry
    {
        /// <summary>Initializes a new instance of the <see cref="ImporterProviderRegistry"/> class.</summary>
        public ImporterProviderRegistry()
        {
            // For<IImporterProviderBase>().Add<GoogleSheetImporterProvider>().Singleton();
            For<IImporterProviderBase>().Add<CSVImporterProvider>().Singleton();
            For<IImporterProviderBase>().Add<ExcelImporterProvider>().Singleton();
        }
    }
}
#else
namespace Clarity.Ecommerce.Providers.Importer
{
    using CSV;
    using Excel;
    // using GoogleSheet;
    using Interfaces.Providers.Importer;
    using StructureMap;
    using StructureMap.Pipeline;

    /// <summary>The Importer provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class ImporterProviderRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="ImporterProviderRegistry"/> class.</summary>
        public ImporterProviderRegistry()
        {
            // For<IImporterProviderBase>(new SingletonLifecycle()).Add<GoogleSheetImporterProvider>();
            For<IImporterProviderBase>(new SingletonLifecycle()).Add<CSVImporterProvider>();
            For<IImporterProviderBase>(new SingletonLifecycle()).Add<ExcelImporterProvider>();
        }
    }
}
#endif
