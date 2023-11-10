// <copyright file="CartStandardWorkflowTests.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the shopping carts standard workflow tests class</summary>
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System.Threading.Tasks;

    public partial class Shopping_Carts_StandardWorkflowTests
    {
        protected override Task Verify_Deactivate_ByID_WithAValidIDInTheData_Should_UpdateActiveAndUpdatedDateValuesAndSaveChanges()
        {
            // Carts cannot call GetAsync
            return Task.CompletedTask;
        }

        protected override Task Verify_Deactivate_ByID_ThatIsNotActive_Should_NotUpdateItemAndReturnTrue()
        {
            // Carts cannot call GetAsync
            return Task.CompletedTask;
        }

        protected override Task Verify_Deactivate_ByKey_WithAValidKeyInTheData_Should_UpdateActiveAndUpdatedDateValuesAndSaveChangesAndNoLongerBeGettable()
        {
            // Carts cannot call GetAsync
            return Task.CompletedTask;
        }

        protected override Task Verify_Deactivate_ByKey_ThatIsNotActive_Should_NotUpdateItemAndReturnTrueAndNoLongerBeGettable()
        {
            // Carts cannot call GetAsync
            return Task.CompletedTask;
        }

        protected override Task Verify_Reactivate_ByID_WithAValidIDInTheData_Should_UpdateActiveAndUpdatedDateValuesAndSaveChanges()
        {
            // Carts cannot call GetAsync
            return Task.CompletedTask;
        }

        protected override Task Verify_Reactivate_ByID_ThatIsActive_Should_NotUpdateItemAndReturnTrue()
        {
            // Carts cannot call GetAsync
            return Task.CompletedTask;
        }

        protected override Task Verify_Reactivate_ByKey_WithAValidKeyInTheData_Should_UpdateActiveAndUpdatedDateValuesAndSaveChanges()
        {
            // Carts cannot call GetAsync
            return Task.CompletedTask;
        }

        protected override Task Verify_Reactivate_ByKey_ThatIsActive_Should_NotUpdateItemAndReturnTrue()
        {
            // Carts cannot call GetAsync
            return Task.CompletedTask;
        }

        protected override Task Verify_Get_ByID_Should_ReturnAModelWithFullMap()
        {
            // Carts cannot call GetAsync
            return Task.CompletedTask;
        }

        protected override Task Verify_Get_ByKey_Should_ReturnAModelWithFullMap()
        {
            // Carts cannot call GetAsync
            return Task.CompletedTask;
        }

        protected override Task Verify_Resolve_WithAnIDThatExists_Should_ReturnAModelWithFullMap()
        {
            // Carts cannot call GetAsync
            return Task.CompletedTask;
        }

        protected override Task Verify_Update_WithValidData_Should_UpdateValuesAndUpdatedDateAndReturnAModelWithFullMap()
        {
            // Carts cannot call GetAsync
            return Task.CompletedTask;
        }

        protected override Task Verify_Upsert_WithValidData_ThatShouldCreate_Should_AddToTheDbSetAndSaveChangesAndReturnAModelWithFullMapAndNewID()
        {
            // Carts cannot call GetAsync
            return Task.CompletedTask;
        }

        protected override Task Verify_Resolve_WithAKeyThatExists_Should_ReturnAModelWithFullMap()
        {
            // Carts cannot call GetAsync
            return Task.CompletedTask;
        }

        protected override Task Verify_ResolveWithAutoGenerate_WithAValidModelThatDoesntExist_Should_CreateAndReturnAModelWithFullMap()
        {
            // Carts cannot call GetAsync
            return Task.CompletedTask;
        }

        protected override Task Verify_Update_WithDataThatCantBeSaved_Should_ThrowAnInvalidDataException()
        {
            // Carts cannot call GetAsync
            return Task.CompletedTask;
        }
    }
}
