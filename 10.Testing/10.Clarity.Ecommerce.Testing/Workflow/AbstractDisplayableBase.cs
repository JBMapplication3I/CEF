// <copyright file="AbstractDisplayableBase.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the abstract base class</summary>
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System;
    using System.Threading.Tasks;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using Models;
    using Xunit;

    public abstract class AbstractDisplayableBase<TIEntity, TEntity, TIModel, TModel, TISearchModel, TSearchModel, TWorkflow>
            : AbstractNameableBase<TIEntity, TEntity, TIModel, TModel, TISearchModel, TSearchModel, TWorkflow>
        where TIEntity : IDisplayableBase
        where TEntity : class, TIEntity, new()
        where TIModel : IDisplayableBaseModel
        where TModel : class, TIModel, new()
        where TISearchModel : IDisplayableBaseSearchModel
        where TSearchModel : class, TISearchModel, new()
        where TWorkflow : IDisplayableWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>, new()
    {
        protected AbstractDisplayableBase(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        protected override async Task VerifyDisplayNameIfDisplayableAsync(MockingSetup mockingSetup, string contextProfileName, TIModel result, bool add2)
        {
            Assert.Equal(await GetDisplayNameAsync(mockingSetup, contextProfileName).ConfigureAwait(false) + (add2 ? " (2)" : string.Empty), result.DisplayName);
        }

        protected override Task VerifyDisplayNameIfDisplayableAsync(MockingSetup mockingSetup, TIModel result, string newName)
        {
            Assert.Equal(newName, result.DisplayName);
            return Task.CompletedTask;
        }

        protected override Task AssignDisplayNameIfDisplayableAsync(MockingSetup mockingSetup, TIModel result, string newName)
        {
            result.DisplayName = newName;
            return Task.CompletedTask;
        }

        protected override Task AssignDisplayNameIfDisplayableAsync(MockingSetup mockingSetup, TIModel result, bool add2)
        {
            result.DisplayName += " (2)";
            return Task.CompletedTask;
        }

        protected virtual async Task<string?> GetDisplayNameAsync(MockingSetup mockingSetup, string contextProfileName)
        {
            await DoMockingSetupAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
            return mockingSetup.TableFirstRecordDisplayNames[typeof(TEntity)];
        }

        protected virtual void Verify_Get_ByDisplayName_Should_ReturnAModelWithFullMap_Results(TIModel result) { }

        protected virtual async Task Verify_Get_ByDisplayName_Should_ReturnAModelWithFullMap()
        {
            var contextProfileName = GenContextProfileName("Verify_Get_ByDisplayName_Should_ReturnAModelWithFullMap");
            // Arrange/Act
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var result = await new TWorkflow().GetByDisplayNameAsync((await GetDisplayNameAsync(mockingSetup, contextProfileName).ConfigureAwait(false))!, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.NotNull(result);
                Assert.IsType<TModel>(result);
                Assert.Equal(await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result!.ID);
                Assert.True(result.Active);
                Assert.Equal(new DateTime(2023, 1, 1), result.CreatedDate);
                Assert.Null(result.UpdatedDate);
                Assert.Equal(await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result.CustomKey);
                Assert.Equal(await GetNameAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result.Name);
                Assert.Equal(await GetDisplayNameAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result.DisplayName);
                Verify_Get_ByDisplayName_Should_ReturnAModelWithFullMap_Results(result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_CheckExistsByDisplayName_ThatExists_Should_ReturnAnID()
        {
            var contextProfileName = GenContextProfileName("Verify_CheckExistsByDisplayName_ThatExists_Should_ReturnAnID");
            // Arrange/Act
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var result = await new TWorkflow().CheckExistsByDisplayNameAsync((await GetDisplayNameAsync(mockingSetup, contextProfileName).ConfigureAwait(false))!, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<int>(result);
                Assert.Equal(await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_CheckExistsByDisplayName_ThatDoesntExist_Should_ReturnNull()
        {
            var contextProfileName = GenContextProfileName("Verify_CheckExistsByDisplayName_ThatDoesntExist_Should_ReturnNull");
            // Arrange/Act/Assert
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                await DoSetupAsync(childContainer, GetMockingSetupWithExistingDataForThisTableAndExpandedTables(), contextProfileName).ConfigureAwait(false);
                Assert.Null(await new TWorkflow().CheckExistsByDisplayNameAsync(KeyNotInTheData, contextProfileName).ConfigureAwait(false));
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_CheckExistsByDisplayName_WithAnInvalidName_Should_ThrowAnInvalidOperationException()
        {
            var contextProfileName = GenContextProfileName("Verify_CheckExistsByDisplayName_WithAnInvalidName_Should_ThrowAnInvalidOperationException");
            // Arrange/Act/Assert
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                await DoSetupAsync(childContainer, GetMockingSetupWithExistingDataForThisTable(), contextProfileName).ConfigureAwait(false);
                await Assert.ThrowsAsync<InvalidOperationException>(() => new TWorkflow().CheckExistsByDisplayNameAsync(string.Empty, contextProfileName)).ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Resolve_WithADisplayNameThatExists_Should_ReturnAModelWithFullMap()
        {
            var contextProfileName = GenContextProfileName("Verify_Resolve_WithADisplayNameThatExists_Should_ReturnAModelWithFullMap");
            // Arrange/Act
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTable();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var result = await new TWorkflow().ResolveAsync(
                        byID: null,
                        byKey: null,
                        byName: null,
                        byDisplayName: await GetDisplayNameAsync(mockingSetup, contextProfileName).ConfigureAwait(false),
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
                Assert.Equal(await GetNameAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result.Result.Name);
                Assert.Equal(await GetDisplayNameAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result.Result.DisplayName);
                Verify_Get_ByDisplayName_Should_ReturnAModelWithFullMap_Results(result.Result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_ResolveWithAutoGenerate_WithADisplayNameThatExists_Should_ReturnAModelWithFullMap()
        {
            var contextProfileName = GenContextProfileName("Verify_ResolveWithAutoGenerate_WithADisplayNameThatExists_Should_ReturnAModelWithFullMap");
            // Arrange/Act
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTable();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var result = await new TWorkflow().ResolveWithAutoGenerateAsync(
                        byID: null,
                        byKey: null,
                        byName: null,
                        byDisplayName: null,
                        model: new TModel
                        {
                            CustomKey = await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false),
                            Name = await GetNameAsync(mockingSetup, contextProfileName).ConfigureAwait(false),
                            DisplayName = await GetDisplayNameAsync(mockingSetup, contextProfileName).ConfigureAwait(false),
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
                Assert.Equal(await GetDisplayNameAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result.Result.DisplayName);
                Verify_Get_ByDisplayName_Should_ReturnAModelWithFullMap_Results(result.Result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
    }
}
