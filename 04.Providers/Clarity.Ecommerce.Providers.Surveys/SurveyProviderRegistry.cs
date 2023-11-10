// <copyright file="SurveyProviderRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Survey Provider StructureMap 4 Registry to associate the interfaces with their concretes</summary>
namespace Clarity.Ecommerce.Providers.Surveys
{
    using Interfaces.Providers.Surveys;
    using StructureMap;
    using StructureMap.Pipeline;
    using SurveyMonkey;

    /// <summary>The Survey provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class SurveyProviderRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="SurveyProviderRegistry"/> class.</summary>
        public SurveyProviderRegistry()
        {
            if (SurveyMonkeyProviderConfig.IsValid(false))
            {
                For<ISurveyProviderBase>(new SingletonLifecycle()).Add<SurveyMonkeyProvider>();
            }
        }
    }
}
