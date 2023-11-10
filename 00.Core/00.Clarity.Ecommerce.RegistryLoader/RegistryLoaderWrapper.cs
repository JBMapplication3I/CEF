// <copyright file="RegistryLoaderWrapper.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the registry loader wrapper class</summary>
#pragma warning disable SA1202 // Elements should be ordered by access
namespace Clarity.Ecommerce
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Providers;
    using Interfaces.Providers.AddressValidation;
    using Interfaces.Providers.Caching;
    using Interfaces.Providers.Chatting;
    using Interfaces.Providers.CurrencyConversions;
    using Interfaces.Providers.Files;
    using Interfaces.Providers.Importer;
    using Interfaces.Providers.Inventory;
    using Interfaces.Providers.Memberships;
    using Interfaces.Providers.Packaging;
    using Interfaces.Providers.Payments;
    using Interfaces.Providers.Personalization;
    using Interfaces.Providers.Pricing;
    using Interfaces.Providers.Proxy;
    using Interfaces.Providers.SalesInvoiceHandlers.Actions;
    using Interfaces.Providers.SalesInvoiceHandlers.Queries;
    using Interfaces.Providers.SalesQuoteHandlers.Actions;
    using Interfaces.Providers.SalesQuoteHandlers.Checkouts;
    using Interfaces.Providers.SalesQuoteHandlers.Queries;
    using Interfaces.Providers.SalesReturnHandlers.Actions;
    using Interfaces.Providers.SalesReturnHandlers.Queries;
    using Interfaces.Providers.SampleRequestHandlers.Actions;
    using Interfaces.Providers.SampleRequestHandlers.Checkouts;
    using Interfaces.Providers.SampleRequestHandlers.Queries;
    using Interfaces.Providers.Searching;
    using Interfaces.Providers.Shipping;
    using Interfaces.Providers.Surcharges;
    using Interfaces.Providers.Surveys;
    using Interfaces.Providers.Taxes;
    using Interfaces.Providers.VinLookup;
#if !NET5_0_OR_GREATER
    using StructureMap.Pipeline;
#endif

    /// <summary>A registry loader wrapper.</summary>
    /// <remarks>This wraps the GetInstance function so that you don't need to reference StructureMap
    /// unless you are doing a more advanced call.</remarks>
    public static class RegistryLoaderWrapper
    {
        private enum Providers
        {
            /// <summary>An enum constant representing the pricing provider option.</summary>
            Pricing,

            /// <summary>An enum constant representing the taxes provider option.</summary>
            Taxes,

            /// <summary>An enum constant representing the packaging provider option.</summary>
            Packaging,

            /// <summary>An enum constant representing the searching provider option.</summary>
            Searching,

            /// <summary>An enum constant representing the sales quote import provider option.</summary>
            SalesQuoteImport,

            /// <summary>An enum constant representing the product import provider option.</summary>
            ProductImport,

            /// <summary>An enum constant representing the personalization provider option.</summary>
            Personalization,

            /// <summary>An enum constant representing the shipping provider option.</summary>
            Shipping,

            /// <summary>An enum constant representing the membership provider option.</summary>
            Membership,

            /// <summary>An enum constant representing the currency provider option.</summary>
            Currency,

            /// <summary>An enum constant representing the survey provider option.</summary>
            Survey,

            /// <summary>An enum constant representing the chatting provider option.</summary>
            Chatting,

            /// <summary>An enum constant representing the files provider option.</summary>
            Files,

            /// <summary>An enum constant representing the address validation option.</summary>
            AddressValidation,

            /// <summary>An enum constant representing the payments option.</summary>
            Payments,

            /// <summary>An enum constant representing the proxy option.</summary>
            Proxy,

            /// <summary>An enum constant representing the inventory option.</summary>
            Inventory,

            /// <summary>An enum constant representing the caching option.</summary>
            Caching,

            /// <summary>An enum constant representing the sales invoice actions handlers option.</summary>
            SalesInvoiceActionsHandlers,

            /// <summary>An enum constant representing the isales nvoice queries handlers option.</summary>
            SalesInvoiceQueriesHandlers,

            /// <summary>An enum constant representing the sales quote checkouts option.</summary>
            SalesQuoteCheckouts,

            /// <summary>An enum constant representing the sales quote actions handlers option.</summary>
            SalesQuoteActionsHandlers,

            /// <summary>An enum constant representing the sales quote queries handlers option.</summary>
            SalesQuoteQueriesHandlers,

            /// <summary>An enum constant representing the sales return actions handlers option.</summary>
            SalesReturnActionsHandlers,

            /// <summary>An enum constant representing the sales return queries handlers option.</summary>
            SalesReturnQueriesHandlers,

            /// <summary>An enum constant representing the sample request checkouts option.</summary>
            SampleRequestCheckouts,

            /// <summary>An enum constant representing the sample request actions handlers option.</summary>
            SampleRequestActionsHandlers,

            /// <summary>An enum constant representing the sample request queries handlers option.</summary>
            SampleRequestQueriesHandlers,

            /// <summary>Handles adding surcharges to payments.</summary>
            Surcharges,

            /// <summary>An enum constant representing the vin lookup option.</summary>
            VinLookup,
        }

        /// <summary>Gets a value indicating whether the intentionally no payment provider is set.</summary>
        /// <value>True if intentionally no payment provider, false if not.</value>
        private static bool IntentionallyNoPaymentProvider { get; }
            = ProviderConfig.CheckIsEnabledBySettings("NoPaymentProvider");

        /// <summary>Gets a value indicating whether the using oracle database.</summary>
        /// <value>True if using oracle database, false if not.</value>
        private static bool UsingOracleDB { get; }
            = bool.Parse(ConfigurationManager.AppSettings["UsingOracle"] ?? "false");

        /// <summary>Gets the loaded providers.</summary>
        /// <value>The loaded providers.</value>
        private static ConcurrentDictionary<Providers, IEnumerable<IProviderBase>> LoadedProviders { get; }
            = new();

        /// <summary>Gets the instance.</summary>
        /// <typeparam name="TObject">Type of the object.</typeparam>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The instance.</returns>
        [DebuggerStepThrough]
        public static TObject GetInstance<TObject>(string? contextProfileName = null)
        {
            return RegistryLoader.NamedContainerInstance(contextProfileName).GetInstance<TObject>();
        }

#if NET5_0_OR_GREATER
        /// <summary>Gets the instance.</summary>
        /// <typeparam name="TObject">    Type of the object.</typeparam>
        /// <param name="dbContext">         Context for the database.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The instance.</returns>
        [DebuggerStepThrough]
        public static TObject GetInstance<TObject>(IDbContext dbContext, string? contextProfileName = null)
        {
            var nested = RegistryLoader.NamedContainerInstance(contextProfileName)
                .GetNestedContainer();
            nested.Inject(dbContext);
            return nested.GetInstance<TObject>();
        }
#else
        /// <summary>Gets the instance.</summary>
        /// <typeparam name="TObject">Type of the object.</typeparam>
        /// <param name="arguments">         The arguments.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The instance.</returns>
        [DebuggerStepThrough]
        public static TObject GetInstance<TObject>(Dictionary<string, object> arguments, string? contextProfileName = null)
        {
            return RegistryLoader.NamedContainerInstance(contextProfileName).GetInstance<TObject>(new ExplicitArguments(arguments));
        }

        /// <summary>Gets the instance.</summary>
        /// <typeparam name="TObject">Type of the object.</typeparam>
        /// <param name="arguments">         The arguments.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The instance.</returns>
        [DebuggerStepThrough]
        public static TObject GetInstance<TObject>(ExplicitArguments arguments, string? contextProfileName = null)
        {
            return RegistryLoader.NamedContainerInstance(contextProfileName).GetInstance<TObject>(arguments);
        }
#endif

        /// <summary>Gets a context.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The context.</returns>
        [DebuggerStepThrough]
        public static IClarityEcommerceEntities GetContext(string? contextProfileName)
        {
            var context = UsingOracleDB
                ? RegistryLoader.NamedContainerInstance(contextProfileName).GetInstance<IClarityEcommerceEntities>("Oracle")
                : RegistryLoader.NamedContainerInstance(contextProfileName).GetInstance<IClarityEcommerceEntities>();
            context.ContextProfileName = contextProfileName;
            return context;
        }

        /// <summary>Get the global surcharge provider, of which there is only ever one.</summary>
        /// <param name="contextProfileName">For dependency injection.</param>
        /// <returns>The surcharge provider. If no provider was configured, NullSurchargeProvider is used (which always
        /// returns $0.00).</returns>
        public static ISurchargeProviderBase? GetSurchargeProvider(string? contextProfileName)
            => GetProvider<ISurchargeProviderBase>(Providers.Surcharges, contextProfileName);

        /// <summary>Gets shipping providers.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The shipping providers.</returns>
        public static List<IShippingProviderBase> GetShippingProviders(string? contextProfileName)
        {
            if (!LoadedProviders.ContainsKey(Providers.Shipping))
            {
                LoadedProviders[Providers.Shipping]
                    = GetDefaultAndActiveProviders<IShippingProviderBase>(contextProfileName);
            }
            return LoadedProviders[Providers.Shipping].Cast<IShippingProviderBase>().ToList();
        }

        /// <summary>Gets personalization providers.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The personalization providers.</returns>
        public static List<IPersonalizationProviderBase> GetPersonalizationProviders(string? contextProfileName)
        {
            if (!LoadedProviders.ContainsKey(Providers.Personalization))
            {
                LoadedProviders[Providers.Personalization]
                    = GetDefaultAndActiveProviders<IPersonalizationProviderBase>(contextProfileName);
            }
            return LoadedProviders[Providers.Personalization].Cast<IPersonalizationProviderBase>().ToList();
        }

        /// <summary>Gets import providers.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The import providers.</returns>
        public static List<IImporterProviderBase> GetImportProviders(string? contextProfileName)
        {
            if (!LoadedProviders.ContainsKey(Providers.ProductImport))
            {
                LoadedProviders[Providers.ProductImport]
                    = GetDefaultAndActiveProviders<IImporterProviderBase>(contextProfileName);
            }
            return LoadedProviders[Providers.ProductImport].Cast<IImporterProviderBase>().ToList();
        }

        /// <summary>Gets specific import provider.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The specific import provider.</returns>
        public static IImporterProviderBase? GetSpecificImportProvider(string name, string? contextProfileName)
        {
            if (!LoadedProviders.ContainsKey(Providers.ProductImport))
            {
                LoadedProviders[Providers.ProductImport]
                    = GetDefaultAndActiveProviders<IImporterProviderBase>(contextProfileName);
            }
            return LoadedProviders[Providers.ProductImport].FirstOrDefault(x => x.Name == name) as IImporterProviderBase;
        }

        #region Mutually Exclusive Providers (Only allows one to be active)
        /// <summary>Gets a CEF cache client. Initializes it if it hasn't been already.</summary>
        /// <param name="contextProfileName">     Name of the context profile.</param>
        /// <returns>The CEF cache client.</returns>
        public static async Task<ICacheProviderBase?> GetCacheClientAsync(string? contextProfileName)
        {
            var provider = GetProvider<ICacheProviderBase>(Providers.Caching, contextProfileName);
            if (provider is { IsInitialized: false })
            {
                await provider.InitAsync(contextProfileName).ConfigureAwait(false);
            }
            return provider;
        }

        /// <summary>Gets tax provider. Initializes it if it hasn't been already.</summary>
        /// <param name="contextProfileName">     Name of the context profile.</param>
        /// <returns>The tax provider.</returns>
        public static async Task<ITaxesProviderBase?> GetTaxProviderAsync(string? contextProfileName)
        {
            var provider = GetProvider<ITaxesProviderBase>(Providers.Taxes, contextProfileName);
            if (provider is { IsInitialized: false })
            {
                await provider.InitAsync(contextProfileName).ConfigureAwait(false);
            }
            return provider;
        }

        /// <summary>Gets sales invoice actions provider.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The sales invoice actions provider.</returns>
        public static ISalesInvoiceActionsProviderBase? GetSalesInvoiceActionsProvider(string? contextProfileName)
        {
            return GetProvider<ISalesInvoiceActionsProviderBase>(Providers.SalesInvoiceActionsHandlers, contextProfileName);
        }

        /// <summary>Gets sales invoice actions provider.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The sales invoice actions provider.</returns>
        public static ISalesInvoiceQueriesProviderBase? GetSalesInvoiceQueriesProvider(string? contextProfileName)
        {
            return GetProvider<ISalesInvoiceQueriesProviderBase>(Providers.SalesInvoiceQueriesHandlers, contextProfileName);
        }

        /// <summary>Gets sales quote checkout provider.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The sales quote checkout provider.</returns>
        public static ISalesQuoteSubmitProviderBase? GetSalesQuoteCheckoutProvider(string? contextProfileName)
        {
            return GetProvider<ISalesQuoteSubmitProviderBase>(Providers.SalesQuoteCheckouts, contextProfileName);
        }

        /// <summary>Gets sales quote actions provider.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The sales quote checkout provider.</returns>
        public static ISalesQuoteActionsProviderBase? GetSalesQuoteActionsProvider(string? contextProfileName)
        {
            return GetProvider<ISalesQuoteActionsProviderBase>(Providers.SalesQuoteActionsHandlers, contextProfileName);
        }

        /// <summary>Gets sales quote queries provider.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The sales quote queries provider.</returns>
        public static ISalesQuoteQueriesProviderBase? GetSalesQuoteQueriesProvider(string? contextProfileName)
        {
            return GetProvider<ISalesQuoteQueriesProviderBase>(Providers.SalesQuoteQueriesHandlers, contextProfileName);
        }

        /// <summary>Gets sales return actions provider.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The sales return checkout provider.</returns>
        public static ISalesReturnActionsProviderBase? GetSalesReturnActionsProvider(string? contextProfileName)
        {
            return GetProvider<ISalesReturnActionsProviderBase>(Providers.SalesReturnActionsHandlers, contextProfileName);
        }

        /// <summary>Gets sales return queries provider.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The sales return queries provider.</returns>
        public static ISalesReturnQueriesProviderBase? GetSalesReturnQueriesProvider(string? contextProfileName)
        {
            return GetProvider<ISalesReturnQueriesProviderBase>(Providers.SalesReturnQueriesHandlers, contextProfileName);
        }

        /// <summary>Gets sample request checkout provider.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The sample request checkout provider.</returns>
        public static ISampleRequestCheckoutProviderBase? GetSampleRequestCheckoutProvider(string? contextProfileName)
        {
            return GetProvider<ISampleRequestCheckoutProviderBase>(Providers.SampleRequestCheckouts, contextProfileName);
        }

        /// <summary>Gets sample request actions provider.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The sample request checkout provider.</returns>
        public static ISampleRequestActionsProviderBase? GetSampleRequestActionsProvider(string? contextProfileName)
        {
            return GetProvider<ISampleRequestActionsProviderBase>(Providers.SampleRequestActionsHandlers, contextProfileName);
        }

        /// <summary>Gets sample request queries provider.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The sample request queries provider.</returns>
        public static ISampleRequestQueriesProviderBase? GetSampleRequestQueriesProvider(string? contextProfileName)
        {
            return GetProvider<ISampleRequestQueriesProviderBase>(Providers.SampleRequestQueriesHandlers, contextProfileName);
        }

        /// <summary>Gets survey provider.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The survey provider.</returns>
        public static ISurveyProviderBase? GetSurveyProvider(string? contextProfileName)
        {
            return GetProvider<ISurveyProviderBase>(Providers.Survey, contextProfileName);
        }

        /// <summary>Gets currency conversion provider.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The currency conversion provider.</returns>
        public static ICurrencyConversionsProviderBase? GetCurrencyConversionProvider(string? contextProfileName)
        {
            return GetProvider<ICurrencyConversionsProviderBase>(Providers.Currency, contextProfileName);
        }

        /// <summary>Gets files provider.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The files provider.</returns>
        public static IFilesProviderBase? GetFilesProvider(string? contextProfileName)
        {
            return GetProvider<IFilesProviderBase>(Providers.Files, contextProfileName);
        }

        /// <summary>Gets payments providers.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The payments providers.</returns>
        public static List<IPaymentsProviderBase> GetPaymentsProviders(string? contextProfileName)
        {
            if (!LoadedProviders.ContainsKey(Providers.Payments))
            {
                LoadedProviders[Providers.Payments]
                    = GetDefaultAndActiveProviders<IPaymentsProviderBase>(contextProfileName);
            }
            return LoadedProviders[Providers.Payments].Cast<IPaymentsProviderBase>().ToList();
        }

        /// <summary>Gets payment provider.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The payment provider.</returns>
        public static IPaymentsProviderBase? GetPaymentProvider(string? contextProfileName)
        {
            if (IntentionallyNoPaymentProvider)
            {
                return null;
            }
            return GetPaymentProviderInner(contextProfileName)
                ?? throw new System.InvalidOperationException(
                    "ERROR! Could not load a Payment Provider. Are your provider settings in the web config correct?");
        }

        /// <summary>Gets chatting provider.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The chatting provider.</returns>
        public static IChattingProviderBase? GetChattingProvider(string? contextProfileName)
        {
            return GetProvider<IChattingProviderBase>(Providers.Chatting, contextProfileName);
        }

        /// <summary>Gets the membership provider.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The membership provider.</returns>
        public static IMembershipsProviderBase? GetMembershipProvider(string? contextProfileName)
        {
            return GetProvider<IMembershipsProviderBase>(Providers.Membership, contextProfileName);
        }

        /// <summary>Gets shipping providers.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The shipping providers.</returns>
        public static IPackagingProviderBase? GetPackagingProvider(string? contextProfileName)
        {
            return GetProvider<IPackagingProviderBase>(Providers.Packaging, contextProfileName);
        }

        /// <summary>Gets sales quote importer provider.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The sales quote importer provider.</returns>
        public static ISalesQuoteImporterProviderBase? GetSalesQuoteImporterProvider(string? contextProfileName)
        {
            return GetProvider<ISalesQuoteImporterProviderBase>(Providers.SalesQuoteImport, contextProfileName);
        }

        /// <summary>Gets address validation provider.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The address validation provider.</returns>
        public static IAddressValidationProviderBase? GetAddressValidationProvider(string? contextProfileName)
        {
            return GetProvider<IAddressValidationProviderBase>(Providers.AddressValidation, contextProfileName);
        }

        /// <summary>Gets pricing provider.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The pricing provider.</returns>
        public static IPricingProviderBase? GetPricingProvider(string? contextProfileName)
        {
            return GetProvider<IPricingProviderBase>(Providers.Pricing, contextProfileName);
        }

        /// <summary>Gets searching provider.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The searching provider.</returns>
        public static ISearchingProviderBase? GetSearchingProvider(string? contextProfileName)
        {
            return GetProvider<ISearchingProviderBase>(Providers.Searching, contextProfileName);
        }

        /// <summary>Gets Proxy provider.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The Proxy provider.</returns>
        public static IProxyProviderBase? GetProxyProvider(string? contextProfileName)
        {
            return GetProvider<IProxyProviderBase>(Providers.Proxy, contextProfileName);
        }

        /// <summary>Gets Inventory provider.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The Inventory provider.</returns>
        public static IInventoryProviderBase? GetInventoryProvider(string? contextProfileName)
        {
            return GetProvider<IInventoryProviderBase>(Providers.Inventory, contextProfileName);
        }

        /// <summary>Gets payment provider.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The payment provider.</returns>
        private static IPaymentsProviderBase? GetPaymentProviderInner(string? contextProfileName)
        {
            return GetProvider<IPaymentsProviderBase>(Providers.Payments, contextProfileName);
        }
        /// <summary>Gets Vin Lookup provider.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The Proxy provider.</returns>
        public static IVinLookupProviderBase? GetVinLookupProvider(string? contextProfileName)
        {
            return GetProvider<IVinLookupProviderBase>(Providers.VinLookup, contextProfileName);
        }
        #endregion

        /// <summary>Gets a provider.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="kind">              The kind.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The provider.</returns>
        private static T? GetProvider<T>(Providers kind, string? contextProfileName)
            where T : class, IProviderBase
        {
            if (!LoadedProviders.ContainsKey(kind))
            {
                LoadedProviders[kind] = GetDefaultAndActiveProviders<T>(contextProfileName);
            }
            return LoadedProviders[kind].FirstOrDefault() as T;
        }

        /// <summary>Gets default and active providers.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The default and active providers.</returns>
        private static List<T> GetDefaultAndActiveProviders<T>(string? contextProfileName)
            where T : IProviderBase
        {
            return RegistryLoader.NamedContainerInstance(contextProfileName).GetAllInstances<T>().ToList();
        }
    }
}
