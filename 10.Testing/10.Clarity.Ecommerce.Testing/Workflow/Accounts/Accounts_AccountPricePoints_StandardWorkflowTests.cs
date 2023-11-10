// <copyright file="Accounts_AccountPricePoints_StandardWorkflowTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the accounts account price points standard workflow tests class</summary>
namespace Clarity.Ecommerce.Workflow.Testing
{
    using Interfaces.Models;
    using Models;
    using Xunit;

    public partial class Accounts_AccountPricePoints_StandardWorkflowTests
    {
        protected override AccountPricePointModel Create_WithValidData_ModelHook()
        {
            return new AccountPricePointModel
            {
                Active = true,
                CustomKey = "MIBSF49682|LTL",
                SlaveID = 2,
                MasterID = 1,
            };
        }

        protected override AccountPricePointModel Update_WithValidData_ModelHook()
        {
            return new AccountPricePointModel
            {
                ID = 1,
                Active = true,
                CustomKey = "MIBSF49682|LTL",
                MasterID = 1,
                SlaveID = 4,
            };
        }

        protected override void Verify_Update_WithValidData_Should_UpdateValuesAndUpdatedDateAndReturnAModelWithFullMap_Results(IAccountPricePointModel result)
        {
            Assert.Equal(4, result.SlaveID);
        }
    }
}
