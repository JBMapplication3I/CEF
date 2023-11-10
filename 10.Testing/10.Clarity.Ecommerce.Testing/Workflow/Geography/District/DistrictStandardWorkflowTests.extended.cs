// <copyright file="DistrictStandardWorkflowTests.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the district workflows tests class</summary>
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System.Threading.Tasks;

    public partial class Geography_Districts_StandardWorkflowTests
    {
        protected override Task Verify_Get_ByName_Should_ReturnAModelWithFullMap()
        {
            // Districts cannot call get by name
            return Task.CompletedTask;
        }

        protected override Task Verify_ResolveNameable_WithANameThatExists_Should_ReturnAModelWithFullMap()
        {
            // Districts cannot call get by name
            return Task.CompletedTask;
        }

        protected override Task Verify_ResolveNameableWithAutoGenerate_WithANameThatExists_Should_ReturnAModelWithFullMap()
        {
            // Districts cannot call get by name
            return Task.CompletedTask;
        }

        protected override Task Verify_ResolveNameableWithAutoGenerate_WithAValidModelThatDoesntExist_Should_CreateAndReturnAModelWithFullMap()
        {
            // Districts cannot call get by name
            return Task.CompletedTask;
        }
    }
}
