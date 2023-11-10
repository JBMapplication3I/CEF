// <copyright file="UserStandardWorkflowTests.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user standard workflow tests.generated class</summary>
#nullable enable
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System.Threading.Tasks;
    using Ecommerce.Testing;
    using Models;

    public partial class Contacts_Users_StandardWorkflowTests
    {
        /// <inheritdoc/>
        protected override async Task<UserModel> GenNewModelAsync(
            MockingSetup mockingSetup,
            string contextProfileName)
        {
            var retVal = await base.GenNewModelAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
            retVal.UserName = "Test1234";
            retVal.Email = "Test1234@email.com";
            return retVal;
        }

        protected override MockingSetup GetMockingSetupWithExistingDataForThisTableAndExpandedTables()
        {
            return new MockingSetup
            {
                DoAccountTable = true,
                DoAccountStatusTable = true,
                DoAccountTypeTable = true,
                DoAddressTable = true,
                DoBrandUserTable = true,
                DoContactTable = true,
                DoContactTypeTable = true,
                DoCountryTable = true,
                DoCurrencyTable = true,
                DoFranchiseUserTable = true,
                DoLanguageTable = true,
                DoNoteTable = true,
                DoNoteTypeTable = true,
                DoRegionTable = true,
                DoStoreTable = true,
                DoStoreUserTable = true,
                DoUserTable = true,
                DoUserFileTable = true,
                DoUserImageTable = true,
                DoUserOnlineStatusTable = true,
                DoUserStatusTable = true,
                DoUserTypeTable = true,
                DoRoleUserTable = true,
                DoUserRoleTable = true,
            };
        }

        /*
        /// <inheritdoc/>
        protected override UserModel? Create_WithValidData_ModelHook()
        {
            var retVal = new UserModel
            {
                Active = true,
                CustomKey = await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false),
                // Required Related Properties
                TypeID = mockingSetup.RawUserTypes.First().Object.ID,
                StatusID = mockingSetup.RawUserStatuses.First().Object.ID,
                ContactID = mockingSetup.RawContacts.First().Object.ID,
                // Optional Related Properties
                AccountID = null,
                PreferredStoreID = null,
                CurrencyID = null,
                LanguageID = null,
                UserOnlineStatusID = null,
                UserName = "Test1234",
                Email = "Test1234@email.com",
            };
            return retVal;
        }
        */
    }
}
