// <copyright file="CartItemStandardWorkflowTests.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the shopping cart items standard workflow tests class</summary>
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System.Threading.Tasks;

    public partial class Shopping_CartItems_StandardWorkflowTests
    {
        protected override Task Verify_Deactivate_ByID_WithAValidIDInTheData_Should_UpdateActiveAndUpdatedDateValuesAndSaveChanges()
        {
            // Cart Items delete instead of Deactivate
            return Task.CompletedTask;
        }

        protected override Task Verify_Deactivate_ByID_WithAnIDNotInTheData_Should_ReturnFalse()
        {
            // Cart Items delete instead of Deactivate
            return Task.CompletedTask;
        }

        protected override Task Verify_Deactivate_ByID_ThatIsNotActive_Should_NotUpdateItemAndReturnTrue()
        {
            // Cart Items delete instead of Deactivate
            return Task.CompletedTask;
        }
    }
}
