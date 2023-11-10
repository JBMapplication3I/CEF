// <copyright file="AbstractNameableBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the abstract nameable base class</summary>
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using Models;
    using Moq;
    using Xunit;

    public abstract class AbstractNameableBase<TIEntity, TEntity, TIModel, TModel, TISearchModel, TSearchModel, TWorkflow>
        : AbstractBase<TIEntity, TEntity, TIModel, TModel, TISearchModel, TSearchModel, TWorkflow>
        where TIEntity : INameableBase
        where TEntity : class, TIEntity, new()
        where TIModel : INameableBaseModel
        where TModel : class, TIModel, new()
        where TISearchModel : INameableBaseSearchModel
        where TSearchModel : class, TISearchModel, new()
        where TWorkflow : INameableWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>, new()
    {
        protected AbstractNameableBase(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [DebuggerStepThrough]
        protected virtual async Task<string?> GetNameAsync(MockingSetup mockingSetup, string contextProfileName)
        {
            await DoMockingSetupAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
            return mockingSetup.TableFirstRecordNames[typeof(TEntity)];
        }

        protected virtual void Verify_Get_ByName_Should_ReturnAModelWithFullMap_Results(TIModel result) { }

        protected override async Task VerifyNameIfNameableAsync(
            MockingSetup mockingSetup,
            string contextProfileName,
            TIModel result,
            bool add2)
        {
            Assert.Equal(await GetNameAsync(mockingSetup, contextProfileName).ConfigureAwait(false) + (add2 ? " (2)" : string.Empty), result.Name);
        }

        protected override Task VerifyNameIfNameableAsync(MockingSetup mockingSetup, TIModel result, string newName)
        {
            Assert.Equal(newName, result.Name);
            return Task.CompletedTask;
        }

        protected override Task AssignNameIfNameableAsync(MockingSetup mockingSetup, TIModel result, string newName)
        {
            result.Name = newName;
            return Task.CompletedTask;
        }

        protected override Task AssignNameIfNameableAsync(MockingSetup mockingSetup, TIModel result, bool add2)
        {
            result.Name += " (2)";
            return Task.CompletedTask;
        }

        protected virtual async Task Verify_Get_ByName_Should_ReturnAModelWithFullMap()
        {
            var contextProfileName = GenContextProfileName("Verify_Get_ByName_Should_ReturnAModelWithFullMap");
            // Arrange/Act
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var result = await new TWorkflow().GetByNameAsync((await GetNameAsync(mockingSetup, contextProfileName).ConfigureAwait(false))!, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.NotNull(result);
                Assert.IsType<TModel>(result);
                Assert.Equal(await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result!.ID);
                Assert.True(result.Active);
                Assert.Equal(new DateTime(2023, 1, 1), result.CreatedDate);
                Assert.Null(result.UpdatedDate);
                Assert.Equal(await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result.CustomKey);
                Assert.Equal(await GetNameAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result.Name);
                Verify_Get_ByName_Should_ReturnAModelWithFullMap_Results(result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_CheckExistsByName_ThatExists_Should_ReturnAnID()
        {
            var contextProfileName = GenContextProfileName("Verify_CheckExistsByName_ThatExists_Should_ReturnAnID");
            // Arrange/Act
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var result = await new TWorkflow().CheckExistsByNameAsync((await GetNameAsync(mockingSetup, contextProfileName).ConfigureAwait(false))!, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<int>(result);
                Assert.Equal(await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_CheckExistsByName_ThatDoesntExist_Should_ReturnNull()
        {
            var contextProfileName = GenContextProfileName("Verify_CheckExistsByName_ThatDoesntExist_Should_ReturnNull");
            // Arrange/Act/Assert
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                await DoSetupAsync(childContainer, GetMockingSetupWithExistingDataForThisTableAndExpandedTables(), contextProfileName).ConfigureAwait(false);
                Assert.Null(await new TWorkflow().CheckExistsByNameAsync(KeyNotInTheData, contextProfileName).ConfigureAwait(false));
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_CheckExistsByName_WithAnInvalidName_Should_ThrowAnInvalidOperationException()
        {
            var contextProfileName = GenContextProfileName("Verify_CheckExistsByName_WithAnInvalidName_Should_ThrowAnInvalidOperationException");
            // Arrange/Act/Assert
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                await DoSetupAsync(childContainer, GetMockingSetupWithExistingDataForThisTable(), contextProfileName).ConfigureAwait(false);
                await Assert.ThrowsAsync<InvalidOperationException>(() => new TWorkflow().CheckExistsByNameAsync(string.Empty, contextProfileName)).ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_ResolveNameable_WithANameThatExists_Should_ReturnAModelWithFullMap()
        {
            var contextProfileName = GenContextProfileName("Verify_ResolveNameable_WithANameThatExists_Should_ReturnAModelWithFullMap");
            // Arrange/Act
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var result = await new TWorkflow().ResolveAsync(
                        byID: null,
                        byKey: null,
                        byName: await GetNameAsync(mockingSetup, contextProfileName).ConfigureAwait(false),
                        model: (TModel?)null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.NotNull(result);
                Assert.NotNull(result.Result);
                Assert.IsType<CEFActionResponse<TIModel>>(result);
                Assert.IsType<TModel>(result.Result);
                Assert.Equal(await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result.Result!.ID);
                Assert.True(result.Result.Active);
                Assert.Equal(new DateTime(2023, 1, 1), result.Result.CreatedDate);
                Assert.Null(result.Result.UpdatedDate);
                Assert.Equal(await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result.Result.CustomKey);
                Verify_Get_ByID_Should_ReturnAModelWithFullMap_Results(result.Result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_ResolveNameableWithAutoGenerate_WithANameThatExists_Should_ReturnAModelWithFullMap()
        {
            var contextProfileName = GenContextProfileName("Verify_ResolveNameableWithAutoGenerate_WithANameThatExists_Should_ReturnAModelWithFullMap");
            // Arrange/Act
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var result = await new TWorkflow().ResolveWithAutoGenerateAsync(
                        byID: null,
                        byKey: null,
                        byName: null,
                        model: new TModel
                        {
                            CustomKey = await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false),
                            Name = await GetNameAsync(mockingSetup, contextProfileName).ConfigureAwait(false),
                        },
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.NotNull(result);
                Assert.NotNull(result.Result);
                Assert.IsType<CEFActionResponse<TIModel>>(result);
                Assert.IsType<TModel>(result.Result);
                Assert.Equal(await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result.Result!.ID);
                Assert.True(result.Result.Active);
                Assert.Equal(new DateTime(2023, 1, 1), result.Result.CreatedDate);
                Assert.Null(result.Result.UpdatedDate);
                Assert.Equal(await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result.Result.CustomKey);
                Assert.Equal(await GetNameAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result.Result.Name);
                Verify_Get_ByID_Should_ReturnAModelWithFullMap_Results(result.Result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_ResolveNameable_WithAnIDThatExists_Should_ReturnAModelWithFullMap()
        {
            var contextProfileName = GenContextProfileName("Verify_ResolveNameable_WithAnIDThatExists_Should_ReturnAModelWithFullMap");
            // Arrange/Act
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var result = await new TWorkflow().ResolveAsync(await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false), null, null, (TModel?)null, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.NotNull(result);
                Assert.NotNull(result.Result);
                Assert.IsType<CEFActionResponse<TIModel>>(result);
                Assert.IsType<TModel>(result.Result);
                Assert.Equal(await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result.Result!.ID);
                Assert.True(result.Result.Active);
                Assert.Equal(new DateTime(2023, 1, 1), result.Result.CreatedDate);
                Assert.Null(result.Result.UpdatedDate);
                Assert.Equal(await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result.Result.CustomKey);
                Verify_Get_ByID_Should_ReturnAModelWithFullMap_Results(result.Result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_ResolveNameable_WithAKeyThatExists_Should_ReturnAModelWithFullMap()
        {
            var contextProfileName = GenContextProfileName("Verify_ResolveNameable_WithAKeyThatExists_Should_ReturnAModelWithFullMap");
            // Arrange/Act
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var result = await new TWorkflow().ResolveAsync(null, await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false), null, (TModel?)null, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.NotNull(result);
                Assert.NotNull(result.Result);
                Assert.IsType<CEFActionResponse<TIModel>>(result);
                Assert.IsType<TModel>(result.Result);
                Assert.Equal(await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result.Result!.ID);
                Assert.True(result.Result.Active);
                Assert.Equal(new DateTime(2023, 1, 1), result.Result.CreatedDate);
                Assert.Null(result.Result.UpdatedDate);
                Assert.Equal(await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result.Result.CustomKey);
                Verify_Get_ByID_Should_ReturnAModelWithFullMap_Results(result.Result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_ResolveNameableWithAutoGenerate_WithAValidModelThatDoesntExist_Should_CreateAndReturnAModelWithFullMap()
        {
            var contextProfileName = GenContextProfileName("Verify_ResolveNameableWithAutoGenerate_WithAValidModelThatDoesntExist_Should_CreateAndReturnAModelWithFullMap");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                mockingSetup.SaveChangesResult = 1;
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                await DoMockingSetupAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
                var model = await GenNewModelAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
                model.CustomKey = "NEW KEY";
                model.Name = "New Name";
                await DoMockingSetupAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
                var newID = GetRawSet(mockingSetup).OrderBy(x => x.Object.ID).Last().Object.ID + 1;
                // Act
                var result = await new TWorkflow().ResolveWithAutoGenerateAsync(
                        byID: null,
                        byKey: null,
                        byName: null,
                        model: model,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                GetMockSet(mockingSetup)!.Verify(m => m.Add(It.IsAny<TEntity>()), Times.Once());
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce());
                Assert.NotNull(result);
                Assert.NotNull(result.Result);
                Assert.IsType<CEFActionResponse<TIModel>>(result);
                Assert.IsType<TModel>(result.Result);
                Assert.Equal(newID, result.Result!.ID);
                Assert.True(result.Result.Active);
                //Assert.Null(result.UpdatedDate);
                Assert.Equal("NEW KEY", result.Result.CustomKey);
                Assert.Equal("New Name", result.Result.Name);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_ResolveNameableWithAutoGenerate_WithNoDataAndANullModel_Should_ThrowAnInvalidDataException()
        {
            var contextProfileName = GenContextProfileName("Verify_ResolveNameableWithAutoGenerate_WithNoDataAndANullModel_Should_ThrowAnInvalidDataException");
            // Arrange/Act/Assert
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                await DoSetupAsync(childContainer, GetMockingSetupWithExistingDataForThisTable(), contextProfileName).ConfigureAwait(false);
                await Assert.ThrowsAsync<System.IO.InvalidDataException>(() => new TWorkflow().ResolveWithAutoGenerateAsync(null, null, null, (TModel?)null, contextProfileName)).ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_ResolveNameableWithAutoGenerateOptional_WithNoDataAndANullModel_Should_ReturnNull()
        {
            var contextProfileName = GenContextProfileName("Verify_ResolveNameableWithAutoGenerateOptional_WithNoDataAndANullModel_Should_ReturnNull");
            // Arrange/Act/Assert
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                await DoSetupAsync(childContainer, GetMockingSetupWithExistingDataForThisTable(), contextProfileName).ConfigureAwait(false);
                Assert.Null((await new TWorkflow().ResolveWithAutoGenerateOptionalAsync(null, null, null, (TModel?)null, contextProfileName).ConfigureAwait(false)).Result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
    }
}
