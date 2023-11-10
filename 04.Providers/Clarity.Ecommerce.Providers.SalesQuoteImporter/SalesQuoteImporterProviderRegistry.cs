// <copyright file="SalesQuoteImporterProviderRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the SalesQuoteImporter provider registry class</summary>
namespace Clarity.Ecommerce.Providers.SalesQuoteImporter
{
    using Excel;
    using Interfaces.Providers.Importer;
    using StructureMap;
    using StructureMap.Pipeline;

    /// <summary>The SalesQuoteImporter provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class SalesQuoteImporterProviderRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="SalesQuoteImporterProviderRegistry"/> class.</summary>
        public SalesQuoteImporterProviderRegistry()
        {
            if (ExcelSalesQuoteImporterProviderConfig.IsValid(false))
            {
                For<ISalesQuoteImporterProviderBase>(new SingletonLifecycle()).Add<ExcelSalesQuoteImporterProvider>();
            }
        }
    }
}
