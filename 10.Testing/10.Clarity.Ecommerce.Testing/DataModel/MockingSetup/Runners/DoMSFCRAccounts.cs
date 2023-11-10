// <copyright file="DoMockingSetupForContextRunnerAccounts.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner accounts class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerAccountsAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Accounts
            if (DoAll || DoAccounts || DoAccountTable)
            {
                RawAccounts = new()
                {
                    await CreateADummyAccountAsync(id: 1, key: "MIBSF49682", name: "Black Star Farms", desc: "A company", isTaxable: true, typeID: 2, credit: 0,
                        jsonAttributes: new SerializableAttributesDictionary { ["PreferredDeliveryDay"] = new SerializableAttributeObject { Key = "PreferredDeliveryDay", Value = nameof(DayOfWeek.Tuesday) } }.SerializeAttributesDictionary()).ConfigureAwait(false),
                    await CreateADummyAccountAsync(id: 2, key: "MIWSF49682", name: "White Star Farms", desc: "A company", isTaxable: true, typeID: 2, credit: 0).ConfigureAwait(false),
                    await CreateADummyAccountAsync(id: 3, key: "MIGSF49682", name: "Gray Star Farms",  desc: "A company", isTaxable: true, typeID: 2, credit: 0).ConfigureAwait(false),
                };
                await InitializeMockSetAccountsAsync(mockContext, RawAccounts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Account Associations
            if (DoAll || DoAccounts || DoAccountAssociationTable)
            {
                var index = 0;
                RawAccountAssociations = new()
                {
                    await CreateADummyAccountAssociationAsync(id: ++index, key: "MIBSF49682|MIWSF49682", slaveID: 2).ConfigureAwait(false),
                };
                await InitializeMockSetAccountAssociationsAsync(mockContext, RawAccountAssociations).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Account Association Types
            if (DoAll || DoAccounts || DoAccountAssociationTypeTable)
            {
                var index = 0;
                RawAccountAssociationTypes = new()
                {
                    await CreateADummyAccountAssociationTypeAsync(id: ++index, key: "General", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetAccountAssociationTypesAsync(mockContext, RawAccountAssociationTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Account Contacts
            if (DoAll || DoAccounts || DoAccountContactTable)
            {
                var index = 0;
                RawAccountContacts = new()
                {
                    await CreateADummyAccountContactAsync(id: ++index, key: "MIBSF49682|CONTACT|ADDRS-1", name: "BILL TO", desc: "desc", isBilling: true, transmittedToERP: true).ConfigureAwait(false),
                    await CreateADummyAccountContactAsync(id: ++index, key: "MIBSF49682|CONTACT|ADDRS-2", name: "SHIP TO", desc: "desc", slaveID: 2, isPrimary: true,  transmittedToERP: true).ConfigureAwait(false),
                    await CreateADummyAccountContactAsync(id: ++index, key: "MIBSF49682|CONTACT|ADDRS-3", name: "OTHER",   desc: "desc", slaveID: 3, transmittedToERP: true).ConfigureAwait(false),
                };
                await InitializeMockSetAccountContactsAsync(mockContext, RawAccountContacts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Account Currencies
            if (DoAll || DoAccounts || DoAccountCurrencyTable)
            {
                var index = 0;
                RawAccountCurrencies = new()
                {
                    await CreateADummyAccountCurrencyAsync(id: ++index, key: index.ToString()).ConfigureAwait(false),
                };
                await InitializeMockSetAccountCurrenciesAsync(mockContext, RawAccountCurrencies).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Account Files
            if (DoAll || DoAccounts || DoAccountFileTable)
            {
                var index = 0;
                RawAccountFiles = new()
                {
                    await CreateADummyAccountFileAsync(id: ++index, key: "File-" + index, name: "An File " + index, desc: "desc").ConfigureAwait(false),
                };
                await InitializeMockSetAccountFilesAsync(mockContext, RawAccountFiles).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Account Images
            if (DoAll || DoAccounts || DoAccountImageTable)
            {
                var index = 0;
                RawAccountImages = new()
                {
                    await CreateADummyAccountImageAsync(id: ++index, key: "Image-" + index, name: "An Image " + index, desc: "desc", displayName: "An Image " + index, isPrimary: true, originalFileFormat: "jpg", originalFileName: "image.jpg", thumbnailFileFormat: "jpg", thumbnailFileName: "image.jpg").ConfigureAwait(false),
                };
                await InitializeMockSetAccountImagesAsync(mockContext, RawAccountImages).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Account Image Types
            if (DoAll || DoAccounts || DoAccountImageTypeTable)
            {
                var index = 0;
                RawAccountImageTypes = new()
                {
                    await CreateADummyAccountImageTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetAccountImageTypesAsync(mockContext, RawAccountImageTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Account Price Points
            if (DoAll || DoAccounts || DoAccountPricePointTable)
            {
                var index = 0;
                RawAccountPricePoints = new()
                {
                    await CreateADummyAccountPricePointAsync(id: ++index, key: "MIBSF49682|LTL",    masterID: 1, slaveID: 4).ConfigureAwait(false),
                    await CreateADummyAccountPricePointAsync(id: ++index, key: "MIWSF49682|WEB",    masterID: 2).ConfigureAwait(false),
                    await CreateADummyAccountPricePointAsync(id: ++index, key: "MIGSF49682|RETAIL", masterID: 3, slaveID: 5).ConfigureAwait(false),
                };
                await InitializeMockSetAccountPricePointsAsync(mockContext, RawAccountPricePoints).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Account Products
            if (DoAll || DoAccounts || DoAccountProductTable)
            {
                var index = 0;
                RawAccountProducts = new()
                {
                    await CreateADummyAccountProductAsync(id: ++index, key: "1|1", slaveID: 1151).ConfigureAwait(false),
                };
                await InitializeMockSetAccountProductsAsync(mockContext, RawAccountProducts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Account Product Types
            if (DoAll || DoAccounts || DoAccountProductTypeTable)
            {
                var index = 0;
                RawAccountProductTypes = new()
                {
                    await CreateADummyAccountProductTypeAsync(id: ++index, key: "Customized", name: "Customized", desc: "desc", sortOrder: 0, displayName: "Customized").ConfigureAwait(false),
                };
                await InitializeMockSetAccountProductTypesAsync(mockContext, RawAccountProductTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Account Statuses
            if (DoAll || DoAccounts || DoAccountStatusTable)
            {
                var index = 0;
                RawAccountStatuses = new()
                {
                    await CreateADummyAccountStatusAsync(id: ++index, key: "NORMAL", name: "Normal", desc: "desc", displayName: "Normal").ConfigureAwait(false),
                };
                await InitializeMockSetAccountStatusesAsync(mockContext, RawAccountStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Account Types
            if (DoAll || DoAccounts || DoAccountTypeTable)
            {
                var index = 0;
                RawAccountTypes = new()
                {
                    await CreateADummyAccountTypeAsync(id: ++index, key: "CUSTOMER", name: "Customer", desc: "desc", sortOrder: 2, displayName: "Customer").ConfigureAwait(false),
                    await CreateADummyAccountTypeAsync(id: ++index, key: "VENDOR", name: "Vendor", desc: "desc", sortOrder: 1, displayName: "Vendor").ConfigureAwait(false),
                    await CreateADummyAccountTypeAsync(id: ++index, key: "AFFILIATE", name: "Affiliate", desc: "desc", sortOrder: 3, displayName: "Affiliate").ConfigureAwait(false),
                    await CreateADummyAccountTypeAsync(id: ++index, key: "ORGANIZATION", name: "Organization", desc: "desc", sortOrder: 4, displayName: "Organization").ConfigureAwait(false),
                };
                await InitializeMockSetAccountTypesAsync(mockContext, RawAccountTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Account Usage Balances
            if (DoAll || DoAccounts || DoAccountUsageBalanceTable)
            {
                var index = 0;
                RawAccountUsageBalances = new()
                {
                    await CreateADummyAccountUsageBalanceAsync(id: ++index, key: "1|1", slaveID: 1151, quantity: 1).ConfigureAwait(false),
                };
                await InitializeMockSetAccountUsageBalancesAsync(mockContext, RawAccountUsageBalances).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Account User Roles
            if (DoAll || DoAccounts || DoAccountUserRoleTable)
            {
                RawAccountUserRoles = new()
                {
                    await CreateADummyAccountUserRoleAsync(active: true, masterID: 1, slaveID: 1).ConfigureAwait(false),
                };
                await InitializeMockSetAccountUserRolesAsync(mockContext, RawAccountUserRoles).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Contractors
            if (DoAll || DoAccounts || DoContractorTable)
            {
                var index = 0;
                RawContractors = new()
                {
                    await CreateADummyContractorAsync(id: ++index, key: "Contractor-" + index).ConfigureAwait(false),
                };
                await InitializeMockSetContractorsAsync(mockContext, RawContractors).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Service Areas
            if (DoAll || DoAccounts || DoServiceAreaTable)
            {
                var index = 0;
                RawServiceAreas = new()
                {
                    await CreateADummyServiceAreaAsync(id: ++index, key: "Service-Area-" + index).ConfigureAwait(false),
                };
                await InitializeMockSetServiceAreasAsync(mockContext, RawServiceAreas).ConfigureAwait(false);
            }
            #endregion
        }

        [System.Diagnostics.DebuggerStepThrough]
        public Task AssignSchemaMocksAccountsExtrasAsync(Mock<IClarityEcommerceEntities> context, bool dirtyOnly)
        {
            return Task.CompletedTask;
        }
    }
}
