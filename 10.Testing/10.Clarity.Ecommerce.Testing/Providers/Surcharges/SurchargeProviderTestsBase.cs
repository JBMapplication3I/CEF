// <copyright file="PayoneerPaymentsProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payoneer payments provider tests class</summary>
// ReSharper disable StringIndexOfIsCultureSpecific.1
namespace Clarity.Ecommerce.Providers.Surcharges.Testing
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Ecommerce.Testing;
    using Interfaces.Providers.Surcharges;

    /// <summary>
    /// Standard helpers for testing surcharge providers.
    /// </summary>
    public abstract class SurchargeProviderTestsBase
    {
        protected async Task Run<TSurchargeProvider>(Func<string, Task> test, [CallerMemberName] string? method = null)
            where TSurchargeProvider : ISurchargeProviderBase
        {
            var contextProfileName = $"{GetType().Name}|{method}";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<ISurchargeProviderBase>().Use<TSurchargeProvider>();
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                await test(contextProfileName).ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
    }
}
