// <copyright file="ProductPricePointWorkflowsTests.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account types workflows tests class</summary>
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.Models;
    using Models;
    using Moq;
    using Workflow;
    using Xunit;

    public partial class Products_ProductPricePoints_StandardWorkflowTests
    {
        protected override ProductPricePointModel Create_WithValidData_ModelHook()
        {
            return new ProductPricePointModel
            {
                CustomKey = "PRODUCT-A|LTL|USD|EACH|1|2147483647|||store-1",
                MasterKey = "PRODUCT-A",
                SlaveKey = "LTL",
                MinQuantity = 1,
                MaxQuantity = int.MaxValue,
                UnitOfMeasure = "EACH",
                StoreKey = "store-1",
                CurrencyKey = "USD",
                From = null,
                To = null,
                Active = true,
                Price = 15m,
            };
        }

        /// <inheritdoc/>
        protected override string KeyNotInTheData => "BO|FTL|USD|EACH|1|2|||";

        protected override async Task Verify_ResolveWithAutoGenerate_WithAValidModelThatDoesntExist_Should_CreateAndReturnAModelWithFullMap()
        {
            var contextProfileName = GenContextProfileName("Verify_ResolveWithAutoGenerate_WithAValidModelThatDoesntExist_Should_CreateAndReturnAModelWithFullMap");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                mockingSetup.SaveChangesResult = 1;
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                await DoMockingSetupAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
                var model = await GenNewModelAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
                model.CustomKey = KeyNotInTheData;
                await AssignNameIfNameableAsync(mockingSetup, model, "New Name").ConfigureAwait(false);
                await AssignDisplayNameIfDisplayableAsync(mockingSetup, model, "New Display Name").ConfigureAwait(false);
                var newID = GetRawSet(mockingSetup).OrderBy(x => x.Object.ID).Last().Object.ID + 1;
                // Act
                var result = await new ProductPricePointWorkflow().ResolveWithAutoGenerateAsync(null, null, model, contextProfileName).ConfigureAwait(false);
                // Assert
                GetMockSet(mockingSetup)!.Verify(m => m.Add(It.IsAny<ProductPricePoint>()), Times.Once());
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce());
                Assert.NotNull(result);
                Assert.NotNull(result.Result);
                Assert.IsType<CEFActionResponse<IProductPricePointModel>>(result);
                Assert.IsType<ProductPricePointModel>(result.Result);
                Assert.Equal(newID, result.Result!.ID);
                Assert.True(result.Result.Active);
                //Assert.Null(result.UpdatedDate);
                Assert.Equal(KeyNotInTheData, result.Result.CustomKey);
                await VerifyNameIfNameableAsync(mockingSetup, result.Result, "New Name").ConfigureAwait(false);
                await VerifyDisplayNameIfDisplayableAsync(mockingSetup, result.Result, "New Display Name").ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected override async Task Verify_Upsert_WithValidData_ThatShouldCreate_Should_AddToTheDbSetAndSaveChangesAndReturnAModelWithFullMapAndNewID()
        {
            var contextProfileName = GenContextProfileName("Verify_Upsert_WithValidData_ThatShouldCreate_Should_AddToTheDbSetAndSaveChangesAndReturnAModelWithFullMapAndNewID");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                mockingSetup.SaveChangesResult = 1;
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                await DoMockingSetupAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
                var model = Create_WithValidData_ModelHook()
                    ?? await GenNewModelAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
                model.CustomKey = KeyNotInTheData;
                await AssignNameIfNameableAsync(mockingSetup, model, "TEST").ConfigureAwait(false);
                await AssignDisplayNameIfDisplayableAsync(mockingSetup, model, "TEST").ConfigureAwait(false);
                var workflow = new ProductPricePointWorkflow();
                // Act
                var result = await workflow.UpsertAsync(model, contextProfileName).ConfigureAwait(false);
                var resultModel = await workflow.GetAsync(result.Result, contextProfileName).ConfigureAwait(false);
                // Assert
                GetMockSet(mockingSetup)!.Verify(m => m.Add(It.IsAny<ProductPricePoint>()), Times.Once());
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce());
                Assert.Equal(KeyNotInTheData, resultModel!.CustomKey);
                await VerifyNameIfNameableAsync(mockingSetup, resultModel, "TEST").ConfigureAwait(false);
                await VerifyDisplayNameIfDisplayableAsync(mockingSetup, resultModel, "TEST").ConfigureAwait(false);
                Verify_Create_WithValidData_Should_AddToTheDbSetAndSaveChangesAndReturnAModelWithFullMapAndNewID_Results(resultModel);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
    }
}
