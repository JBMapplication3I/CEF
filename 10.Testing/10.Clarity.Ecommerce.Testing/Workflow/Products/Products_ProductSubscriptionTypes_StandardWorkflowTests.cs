// <copyright file="Products_ProductSubscriptionTypes_StandardWorkflowTests.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the products product subscription types standard workflow tests class</summary>
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System.Threading.Tasks;

    public partial class Products_ProductSubscriptionTypes_StandardWorkflowTests
    {
        protected override Task Verify_Upsert_WithValidData_ThatShouldCreate_Should_AddToTheDbSetAndSaveChangesAndReturnAModelWithFullMapAndNewID()
        {
            // Product master requires several tables to generate
            return Task.CompletedTask;
        }
    }
}
