// <autogenerated>
// <copyright file="Sales.MockingSetup.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Sales section of the Mocking Setup class</summary>
// <remarks>This file was auto-generated by MockingSetupMaster.tt, changes to this
// file will be overwritten automatically when the T4 template is run again</remarks>
// </autogenerated>
// ReSharper disable CyclomaticComplexity, FunctionComplexityOverflow
#pragma warning disable CS0618 // Obsolete Items warnings ignored inside T4
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously ignored inside T4
#nullable enable
namespace Clarity.Ecommerce.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        // Enable the entire schema on run
        public bool DoSales { private get; set; }

        // Enable the tables specifically on run
        public bool DoSalesGroupTable { private get; set; }
        public bool DoSalesItemTargetTypeTable { private get; set; }

        // Dirty Checking
        public bool SalesGroupDirty { private get; set; }
        public bool SalesItemTargetTypeDirty { private get; set; }

        // Sets
        public Mock<DbSet<SalesGroup>>? SalesGroups { get; private set; }
        public Mock<DbSet<SalesItemTargetType>>? SalesItemTargetTypes { get; private set; }

        // Raw Data
        public List<Mock<SalesGroup>>? RawSalesGroups { get; private set; }
        public List<Mock<SalesItemTargetType>>? RawSalesItemTargetTypes { get; private set; }

        [System.Diagnostics.DebuggerStepThrough]
        public async Task AssignSchemaMocksSalesAsync(Mock<IClarityEcommerceEntities> context, bool dirtyOnly)
        {
            if (DoAll || DoSales || (DoSalesGroupTable && (!dirtyOnly || SalesGroupDirty)))
            {
                var attempts = 0;
                await RetryHelper.RetryOnExceptionAsync(async () =>
                {
                    if (++attempts > 1) { System.Diagnostics.Debug.WriteLine($"Assign Schema Mocks is taking extra attempts: {attempts}"); }
                    if (RawSalesGroups == null) { throw new InvalidOperationException("Raw SalesGroups was null"); }
                    await InitializeMockSetFromListAsync(SalesGroups, RawSalesGroups).ConfigureAwait(false);
                    context.Setup(m => m.SalesGroups).Returns(() => SalesGroups?.Object!);
                    context.Setup(m => m.Set<SalesGroup>()).Returns(() => SalesGroups?.Object!);
                })
                .ConfigureAwait(false);
            }
            if (DoAll || DoSales || (DoSalesItemTargetTypeTable && (!dirtyOnly || SalesItemTargetTypeDirty)))
            {
                var attempts = 0;
                await RetryHelper.RetryOnExceptionAsync(async () =>
                {
                    if (++attempts > 1) { System.Diagnostics.Debug.WriteLine($"Assign Schema Mocks is taking extra attempts: {attempts}"); }
                    if (RawSalesItemTargetTypes == null) { throw new InvalidOperationException("Raw SalesItemTargetTypes was null"); }
                    await InitializeMockSetFromListAsync(SalesItemTargetTypes, RawSalesItemTargetTypes).ConfigureAwait(false);
                    context.Setup(m => m.SalesItemTargetTypes).Returns(() => SalesItemTargetTypes?.Object!);
                    context.Setup(m => m.Set<SalesItemTargetType>()).Returns(() => SalesItemTargetTypes?.Object!);
                })
                .ConfigureAwait(false);
            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        private async Task InitializeMockSetSalesGroupsAsync(Mock<IClarityEcommerceEntities> context, List<Mock<SalesGroup>> data)
        {
            if (!DoAll && !DoSales && !DoSalesGroupTable) { return; }
            SalesGroups ??= new Mock<DbSet<SalesGroup>>();
            await InitializeMockSetFromListAsync(SalesGroups, data).ConfigureAwait(false);
            context.Setup(m => m.SalesGroups).Returns(() => SalesGroups.Object);
            context.Setup(m => m.Set<SalesGroup>()).Returns(() => SalesGroups.Object);
            TableRecordCounts[typeof(SalesGroup)] = data.Count;
            if (data.Any())
            {
                TableFirstRecordIDs[typeof(SalesGroup)] = data[0].Object.ID;
                TableFirstRecordCustomKeys[typeof(SalesGroup)] = data[0].Object.CustomKey;
            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        private async Task InitializeMockSetSalesItemTargetTypesAsync(Mock<IClarityEcommerceEntities> context, List<Mock<SalesItemTargetType>> data)
        {
            if (!DoAll && !DoSales && !DoSalesItemTargetTypeTable) { return; }
            SalesItemTargetTypes ??= new Mock<DbSet<SalesItemTargetType>>();
            await InitializeMockSetFromListAsync(SalesItemTargetTypes, data).ConfigureAwait(false);
            context.Setup(m => m.SalesItemTargetTypes).Returns(() => SalesItemTargetTypes.Object);
            context.Setup(m => m.Set<SalesItemTargetType>()).Returns(() => SalesItemTargetTypes.Object);
            TableRecordCounts[typeof(SalesItemTargetType)] = data.Count;
            if (data.Any())
            {
                TableFirstRecordIDs[typeof(SalesItemTargetType)] = data[0].Object.ID;
                TableFirstRecordCustomKeys[typeof(SalesItemTargetType)] = data[0].Object.CustomKey;
                TableFirstRecordNames[typeof(SalesItemTargetType)] = data[0].Object.Name;
                TableFirstRecordDisplayNames[typeof(SalesItemTargetType)] = data[0].Object.DisplayName;
            }
        }

        /// <summary>Creates a dummy SalesGroup.</summary>
        /// <returns>The new SalesGroup.</returns>
        [System.Diagnostics.DebuggerStepThrough]
        private Task<Mock<SalesGroup>> CreateADummySalesGroupAsync(int id, string? key, string? jsonAttributes = null, int? accountID = null!, int? billingContactID = null!, int? brandID = null!, bool? active = null)
        {
            return Task.Run(async () =>
            {
                var mock = new Mock<SalesGroup>();
                mock.SetupAllProperties();
                // IBase Properties
                mock.Object.ID = id;
                mock.Object.CustomKey = key;
                mock.Object.Active = active.HasValue ? active.Value : !DoInactives;
                mock.Object.CreatedDate = CreatedDate;
                mock.Object.UpdatedDate = null;
                mock.Object.Hash = null;
                mock.Object.JsonAttributes = jsonAttributes;
                // SalesGroup Properties
                mock.Object.AccountID = accountID;
                mock.Object.BillingContactID = billingContactID;
                mock.Object.BrandID = brandID;
                // Dynamic Linking
                // IHaveNotesBase Properties
                mock.Setup(m => m.Notes).Returns(() => RawNotes?.Where(x => x.Object.SalesGroupID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<Note>());
                // SalesGroup's Related Objects
                mock.Setup(m => m.Account).Returns(() => RawAccounts?.FirstOrDefault(x => x.Object.ID == mock.Object.AccountID)?.Object);
                mock.Setup(m => m.BillingContact).Returns(() => RawContacts?.FirstOrDefault(x => x.Object.ID == mock.Object.BillingContactID)?.Object);
                mock.Setup(m => m.Brand).Returns(() => RawBrands?.FirstOrDefault(x => x.Object.ID == mock.Object.BrandID)?.Object);
                // SalesGroup's Associated Objects
                mock.Setup(m => m.PurchaseOrders).Returns(() => RawPurchaseOrders?.Where(x => x.Object.SalesGroupID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<PurchaseOrder>());
                mock.Setup(m => m.SalesInvoices).Returns(() => RawSalesInvoices?.Where(x => x.Object.SalesGroupID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<SalesInvoice>());
                mock.Setup(m => m.SalesOrderMasters).Returns(() => RawSalesOrders?.Where(x => x.Object.SalesGroupAsMasterID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<SalesOrder>());
                mock.Setup(m => m.SalesQuoteRequestMasters).Returns(() => RawSalesQuotes?.Where(x => x.Object.SalesGroupAsRequestMasterID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<SalesQuote>());
                mock.Setup(m => m.SalesQuoteRequestSubs).Returns(() => RawSalesQuotes?.Where(x => x.Object.SalesGroupAsRequestSubID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<SalesQuote>());
                mock.Setup(m => m.SalesQuoteResponseMasters).Returns(() => RawSalesQuotes?.Where(x => x.Object.SalesGroupAsResponseMasterID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<SalesQuote>());
                mock.Setup(m => m.SalesQuoteResponseSubs).Returns(() => RawSalesQuotes?.Where(x => x.Object.SalesGroupAsResponseSubID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<SalesQuote>());
                mock.Setup(m => m.SalesReturns).Returns(() => RawSalesReturns?.Where(x => x.Object.SalesGroupID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<SalesReturn>());
                mock.Setup(m => m.SampleRequests).Returns(() => RawSampleRequests?.Where(x => x.Object.SalesGroupID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<SampleRequest>());
                mock.Setup(m => m.SubSalesOrders).Returns(() => RawSalesOrders?.Where(x => x.Object.SalesGroupAsSubID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<SalesOrder>());
                return mock;
            });
        }

        /// <summary>Creates a dummy SalesItemTargetType.</summary>
        /// <returns>The new SalesItemTargetType.</returns>
        [System.Diagnostics.DebuggerStepThrough]
        private Task<Mock<SalesItemTargetType>> CreateADummySalesItemTargetTypeAsync(int id, string? key, string? name, string? desc = null, int? sortOrder = null, string? displayName = null, string? translationKey = null, string? jsonAttributes = null, bool? active = null)
        {
            return Task.Run(async () =>
            {
                var mock = new Mock<SalesItemTargetType>();
                mock.SetupAllProperties();
                // IBase Properties
                mock.Object.ID = id;
                mock.Object.CustomKey = key;
                mock.Object.Active = active.HasValue ? active.Value : !DoInactives;
                mock.Object.CreatedDate = CreatedDate;
                mock.Object.UpdatedDate = null;
                mock.Object.Hash = null;
                mock.Object.JsonAttributes = jsonAttributes;
                // INameableBase Properties
                mock.Object.Name = name;
                mock.Object.Description = desc;
                // IDisplayableBase Properties
                mock.Object.SortOrder = sortOrder;
                mock.Object.DisplayName = displayName;
                mock.Object.TranslationKey = translationKey;
                // SalesItemTargetType Properties
                // Dynamic Linking
                // SalesItemTargetType's Related Objects
                // SalesItemTargetType's Associated Objects
                return mock;
            });
        }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Moq;

    internal partial class DataModelTestingRegistry
    {
        private void CreateRegistryForSales(MockingSetup mockingSetup)
        {
            Func<SalesGroup> mockFuncSalesGroup = () =>
            {
                var mock = new Mock<SalesGroup>();
                mock.SetupAllProperties();
                mock.Setup(m => m.ToHashableString()).CallBase();
                // SalesGroup Properties
                // Dynamic Linking
                // IHaveNotesBase Properties
                mock.Setup(m => m.Notes).Returns(() => mockingSetup.RawNotes?.Where(x => x.Object.SalesGroupID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<Note>());
                mock.Setup(m => m.Account).Returns(() => mockingSetup.RawAccounts?.FirstOrDefault(x => x.Object.ID == mock.Object.AccountID)?.Object);
                mock.Setup(m => m.BillingContact).Returns(() => mockingSetup.RawContacts?.FirstOrDefault(x => x.Object.ID == mock.Object.BillingContactID)?.Object);
                mock.Setup(m => m.Brand).Returns(() => mockingSetup.RawBrands?.FirstOrDefault(x => x.Object.ID == mock.Object.BrandID)?.Object);
                // SalesGroup's Associated Objects
                mock.Setup(m => m.PurchaseOrders).Returns(() => mockingSetup.RawPurchaseOrders?.Where(x => x.Object.SalesGroupID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<PurchaseOrder>());
                mock.Setup(m => m.SalesInvoices).Returns(() => mockingSetup.RawSalesInvoices?.Where(x => x.Object.SalesGroupID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<SalesInvoice>());
                mock.Setup(m => m.SalesOrderMasters).Returns(() => mockingSetup.RawSalesOrders?.Where(x => x.Object.SalesGroupAsMasterID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<SalesOrder>());
                mock.Setup(m => m.SalesQuoteRequestMasters).Returns(() => mockingSetup.RawSalesQuotes?.Where(x => x.Object.SalesGroupAsRequestMasterID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<SalesQuote>());
                mock.Setup(m => m.SalesQuoteRequestSubs).Returns(() => mockingSetup.RawSalesQuotes?.Where(x => x.Object.SalesGroupAsRequestSubID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<SalesQuote>());
                mock.Setup(m => m.SalesQuoteResponseMasters).Returns(() => mockingSetup.RawSalesQuotes?.Where(x => x.Object.SalesGroupAsResponseMasterID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<SalesQuote>());
                mock.Setup(m => m.SalesQuoteResponseSubs).Returns(() => mockingSetup.RawSalesQuotes?.Where(x => x.Object.SalesGroupAsResponseSubID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<SalesQuote>());
                mock.Setup(m => m.SalesReturns).Returns(() => mockingSetup.RawSalesReturns?.Where(x => x.Object.SalesGroupID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<SalesReturn>());
                mock.Setup(m => m.SampleRequests).Returns(() => mockingSetup.RawSampleRequests?.Where(x => x.Object.SalesGroupID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<SampleRequest>());
                mock.Setup(m => m.SubSalesOrders).Returns(() => mockingSetup.RawSalesOrders?.Where(x => x.Object.SalesGroupAsSubID == mock.Object.ID).Select(x => x.Object).ToList() ?? new List<SalesOrder>());
                // SalesGroup's Related Objects
                return mock.Object;
            };
            For<ISalesGroup>().Use(() => mockFuncSalesGroup());
            For<SalesGroup>().Use(() => mockFuncSalesGroup());
            Func<SalesItemTargetType> mockFuncSalesItemTargetType = () =>
            {
                var mock = new Mock<SalesItemTargetType>();
                mock.SetupAllProperties();
                mock.Setup(m => m.ToHashableString()).CallBase();
                // SalesItemTargetType Properties
                // Dynamic Linking
                // SalesItemTargetType's Associated Objects
                // SalesItemTargetType's Related Objects
                return mock.Object;
            };
            For<ISalesItemTargetType>().Use(() => mockFuncSalesItemTargetType());
            For<SalesItemTargetType>().Use(() => mockFuncSalesItemTargetType());
        }
    }
}
