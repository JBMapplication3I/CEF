// <autogenerated>
// <copyright file="Purchasing.StandardWorkflowTests.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Purchasing section of the Mocking Setup class</summary>
// <remarks>This file was auto-generated by StandardWorkflowTestsMaster.tt, changes to this
// file will be overwritten automatically when the T4 template is run again</remarks>
// </autogenerated>
#pragma warning disable CS0618 // Obsoletes Ignored in T4s
#nullable enable
// ReSharper disable PartialTypeWithSinglePart
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Models;
    using Moq;
    using Workflow;
    using Xunit;

    public abstract class Purchasing_PurchaseOrders_WorkflowTestsBase
        : AbstractBase<
            IPurchaseOrder,
            PurchaseOrder,
            IPurchaseOrderModel,
            PurchaseOrderModel,
            IPurchaseOrderSearchModel,
            PurchaseOrderSearchModel,
            PurchaseOrderWorkflow>
    {
        protected Purchasing_PurchaseOrders_WorkflowTestsBase(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        protected override MockingSetup GetMockingSetupWithExistingDataForThisTable()
        {
            return new MockingSetup { DoPurchaseOrderTable = true };
        }

        protected override MockingSetup GetMockingSetupWithExistingDataForThisTableAndExpandedTables()
        {
            return new MockingSetup
            {
                DoAccountTable = true,
                DoAccountStatusTable = true,
                DoAccountTypeTable = true,
                DoAddressTable = true,
                DoBrandTable = true,
                DoContactTable = true,
                DoContactTypeTable = true,
                DoCountryTable = true,
                DoFranchiseTable = true,
                DoInventoryLocationTable = true,
                DoNoteTable = true,
                DoNoteTypeTable = true,
                DoPurchaseOrderTable = true,
                DoPurchaseOrderContactTable = true,
                DoPurchaseOrderFileTable = true,
                DoPurchaseOrderItemTable = true,
                DoPurchaseOrderStateTable = true,
                DoPurchaseOrderStatusTable = true,
                DoPurchaseOrderTypeTable = true,
                DoRateQuoteTable = true,
                DoRegionTable = true,
                DoSalesGroupTable = true,
                DoSalesOrderPurchaseOrderTable = true,
                DoShipCarrierTable = true,
                DoStoreTable = true,
                DoUserTable = true,
                DoVendorTable = true,
            };
        }

        /// <inheritdoc/>
        protected override Mock<DbSet<PurchaseOrder>>? GetMockSet(MockingSetup mockingSetup)
        {
            return mockingSetup.PurchaseOrders;
        }

        /// <inheritdoc/>
        protected override List<Mock<PurchaseOrder>>? GetRawSet(MockingSetup mockingSetup)
        {
            return mockingSetup.RawPurchaseOrders;
        }

        /// <inheritdoc/>
        protected override async Task<PurchaseOrderModel> GenNewModelAsync(MockingSetup mockingSetup, string contextProfileName)
        {
            return new PurchaseOrderModel
            {
                Active = true,
                CustomKey = await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false),
                // Required Related Properties
                StatusID = mockingSetup.RawPurchaseOrderStatuses.First().Object.ID,
                StateID = mockingSetup.RawPurchaseOrderStates.First().Object.ID,
                TypeID = mockingSetup.RawPurchaseOrderTypes.First().Object.ID,
                // Optional Related Properties
                InventoryLocationID = null,
                ShipCarrierID = null,
                VendorID = null,
                SalesGroupID = null,
                AccountID = null,
                BrandID = null,
                FranchiseID = null,
                StoreID = null,
                UserID = null,
                BillingContactID = null,
                ShippingContactID = null,
            };
        }
    }

    [Trait("Category", "Workflows.Purchasing.PurchaseOrders.Standard")]
    public partial class Purchasing_PurchaseOrders_StandardWorkflowTests : Purchasing_PurchaseOrders_WorkflowTestsBase
    {
        public Purchasing_PurchaseOrders_StandardWorkflowTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact]
        public virtual async Task Verify_ThisTablesTests_CheckExists_RunAppropriately()
        {
            await Task.WhenAll(
                Verify_CheckExistsByID_ThatExists_Should_ReturnAnID(),
                Verify_CheckExistsByID_ThatDoesntExist_Should_ReturnNull(),
                Verify_CheckExistsByID_WithAnInvalidID_Should_ThrowAnInvalidOperationException(),
                Verify_CheckExistsByKey_ThatExists_Should_ReturnAnID(),
                Verify_CheckExistsByKey_ThatDoesntExist_Should_ReturnNull(),
                Verify_CheckExistsByKey_WithAnInvalidKey_Should_ThrowAnInvalidOperationException());
        }

        [Fact]
        public virtual async Task Verify_ThisTablesTests_Get_RunAppropriately()
        {
            await Task.WhenAll(
                Verify_Get_ByID_Should_ReturnAModelWithFullMap(),
                Verify_Get_ByKey_Should_ReturnAModelWithFullMap());
        }

        [Fact]
        public virtual async Task Verify_ThisTablesTests_Resolve_RunAppropriately()
        {
            await Task.WhenAll(
                Verify_Resolve_WithAnIDThatExists_Should_ReturnAModelWithFullMap(),
                Verify_Resolve_WithAKeyThatExists_Should_ReturnAModelWithFullMap(),
                Verify_ResolveWithAutoGenerate_WithAValidModelThatDoesntExist_Should_CreateAndReturnAModelWithFullMap(),
                Verify_ResolveWithAutoGenerate_WithNoDataAndANullModel_Should_ThrowAnInvalidDataException(),
                Verify_ResolveWithAutoGenerateOptional_WithNoDataAndANullModel_Should_ReturnNull());
        }

        [Fact]
        public virtual async Task Verify_ThisTablesTests_Search_RunAppropriately()
        {
            await Task.WhenAll(
                Verify_Search_WithAsListingTrue_Should_ReturnAListOfModelsWithListingMapping(),
                Verify_Search_Should_ReturnAListOfModelsWithLiteMapping(),
                Verify_SearchForConnect_Should_ReturnAListOfModelsWithLiteMapping(),
                Verify_GetDigest_Should_ReturnAListOfModelsWithLiteMapping());
        }

        [Fact]
        public virtual async Task Verify_ThisTablesTests_Create_RunAppropriately()
        {
            await Task.WhenAll(
                Verify_Create_WithValidData_Should_AddToTheDbSetAndSaveChangesAndReturnAModelWithFullMapAndNewID(),
                Verify_Create_WithADuplicateKey_Should_ThrowAnInvalidOperationException(),
                Verify_Create_WithDataThatCantBeSaved_Should_ThrowAnInvalidDataException(),
                Verify_Create_WithAPositiveNonMaximumId_Should_ThrowAnInvalidOperationException());
        }

        [Fact]
        public virtual async Task Verify_ThisTablesTests_Update_RunAppropriately()
        {
            await Task.WhenAll(
                Verify_Update_WithValidData_Should_UpdateValuesAndUpdatedDateAndReturnAModelWithFullMap(),
                Verify_Update_WithDataThatCantBeSaved_Should_ThrowAnInvalidDataException(),
                Verify_Update_WithAnIDLessThanOrEqualToZeroOrMinOrMax_Should_ThrowAnInvalidOperationException(),
                Verify_Update_WithAnIDNotInTheData_Should_ThrowAnArgumentException());
        }

        [Fact]
        public virtual async Task Verify_ThisTablesTests_Upsert_RunAppropriately()
        {
            await Task.WhenAll(
                Verify_Upsert_WithValidData_ThatShouldCreate_Should_AddToTheDbSetAndSaveChangesAndReturnAModelWithFullMapAndNewID(),
                Verify_Upsert_WithValidData_ThatShouldUpdate_Should_UpdateValuesAndUpdatedDateAndReturnAModelWithFullMap(),
                Verify_Upsert_WithValidDataButNoID_ThatShouldUpdate_Should_UpdateValuesAndUpdatedDateAndReturnAModelWithFullMap());
        }

        [Fact]
        public virtual async Task Verify_ThisTablesTests_Deactivate_RunAppropriately()
        {
            await Task.WhenAll(
                Verify_Deactivate_ByID_WithAValidIDInTheData_Should_UpdateActiveAndUpdatedDateValuesAndSaveChanges(),
                Verify_Deactivate_ByID_WithAnIDNotInTheData_Should_ReturnFalse(),
                Verify_Deactivate_ByID_WithAnInvalidID_Should_ThrowAnInvalidOperationException(),
                Verify_Deactivate_ByID_ThatIsNotActive_Should_NotUpdateItemAndReturnTrue(),
                Verify_Deactivate_ByKey_WithAValidKeyInTheData_Should_UpdateActiveAndUpdatedDateValuesAndSaveChangesAndNoLongerBeGettable(),
                Verify_Deactivate_ByKey_WithAKeyNotInTheData_Should_ReturnFalse(),
                Verify_Deactivate_ByKey_WithAnInvalidKey_Should_ThrowAnInvalidOperationException(),
                Verify_Deactivate_ByKey_ThatIsNotActive_Should_NotUpdateItemAndReturnTrueAndNoLongerBeGettable());
        }

        [Fact]
        public virtual async Task Verify_ThisTablesTests_Reactivate_RunAppropriately()
        {
            await Task.WhenAll(
                Verify_Reactivate_ByID_WithAValidIDInTheData_Should_UpdateActiveAndUpdatedDateValuesAndSaveChanges(),
                Verify_Reactivate_ByID_WithAnIDNotInTheData_Should_ReturnFalse(),
                Verify_Reactivate_ByID_WithAnInvalidID_Should_ThrowAnInvalidOperationException(),
                Verify_Reactivate_ByID_ThatIsActive_Should_NotUpdateItemAndReturnTrue(),
                Verify_Reactivate_ByKey_WithAValidKeyInTheData_Should_UpdateActiveAndUpdatedDateValuesAndSaveChanges(),
                Verify_Reactivate_ByKey_WithAKeyNotInTheData_Should_ReturnFalse(),
                Verify_Reactivate_ByKey_WithAnInvalidKey_Should_ThrowAnInvalidOperationException(),
                Verify_Reactivate_ByKey_ThatIsActive_Should_NotUpdateItemAndReturnTrue());
        }

        [Fact]
        public virtual async Task Verify_ThisTablesTests_Delete_RunAppropriately()
        {
            await Task.WhenAll(
                Verify_Delete_ByID_WithAValidIDInTheData_Should_RemoveAndSaveChanges(),
                Verify_Delete_ByID_WithAnIDNotInTheData_Should_ReturnTrue(),
                Verify_Delete_ByID_WithAnInvalidID_Should_ThrowAnInvalidOperationException(),
                Verify_Delete_ByKey_WithAValidKeyInTheData_Should_RemoveAndSaveChanges(),
                Verify_Delete_ByKey_WithAKeyNotInTheData_Should_ReturnTrue(),
                Verify_Delete_ByKey_WithAnInvalidKey_Should_ThrowAnInvalidOperationException());
        }
    }
}
