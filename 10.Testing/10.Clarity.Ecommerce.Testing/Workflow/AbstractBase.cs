// <copyright file="AbstractBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the abstract base class</summary>
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using Mapper;
    using Models;
    using Moq;
#if NET5_0_OR_GREATER
    using Lamar;
#else
    using StructureMap;
    using StructureMap.Pipeline;
#endif
    using Xunit;

    public abstract class AbstractBase<TIEntity, TEntity, TIModel, TModel, TISearchModel, TSearchModel, TWorkflow> : XUnitLogHelper
        where TIEntity : IBase
        where TEntity : class, TIEntity, new()
        where TIModel : IBaseModel
        where TModel : class, TIModel, new()
        where TISearchModel : IBaseSearchModel
        where TSearchModel : class, TISearchModel, new()
        where TWorkflow : IWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>, new()
    {
        protected AbstractBase(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            BaseModelMapper.Initialize();
        }

        protected static void Verify_CEFAR_Failed_WithSingleMessage(CEFActionResponse result, string expectMessage)
        {
            Assert.NotNull(result);
            Assert.False(result.ActionSucceeded);
            Assert.Single(result.Messages);
            Assert.Equal(expectMessage, result.Messages[0]);
        }

        protected static void Verify_CEFAR_Passed_WithNoMessages(CEFActionResponse result)
        {
            Assert.NotNull(result);
            Assert.True(
                result.ActionSucceeded,
                result.Messages?.DefaultIfEmpty("No Messages").Aggregate((c, n) => c + "\r\n" + n));
            Assert.Empty(result.Messages!);
        }

        [DebuggerStepThrough]
        protected static async Task DoMockingSetupAsync(MockingSetup mockingSetup, string contextProfileName)
        {
            if (mockingSetup.MockContext?.Object == null || !mockingSetup.SetupComplete)
            {
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
            }
        }

        [DebuggerStepThrough]
        protected async Task DoSetupAsync(IContainer childContainer, MockingSetup mockingSetup, string contextProfileName)
        {
            await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
            RegistryLoader.RootContainer.Configure(
                x => x.For<ILogger>().UseInstance(
                    new ObjectInstance(new Logger
                    {
                        ExtraLogger = s =>
                        {
                            try
                            {
                                TestOutputHelper.WriteLine(s);
                            }
                            catch
                            {
                                // Do nothing
                            }
                        },
                    })));
            childContainer.Configure(x =>
            {
                x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
            });
            RegistryLoader.OverrideContainer(childContainer, contextProfileName);
        }

        [DebuggerStepThrough]
        protected virtual async Task<int> GetIDAsync(MockingSetup mockingSetup, string contextProfileName)
        {
            await DoMockingSetupAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
            return mockingSetup.TableFirstRecordIDs[typeof(TEntity)];
        }

        [DebuggerStepThrough]
        protected virtual async Task<string?> GetCustomKeyAsync(MockingSetup mockingSetup, string contextProfileName)
        {
            await DoMockingSetupAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
            return mockingSetup.TableFirstRecordCustomKeys[typeof(TEntity)];
        }

        [DebuggerStepThrough]
        protected virtual async Task<int> GetCacheCountAsync(MockingSetup mockingSetup, string contextProfileName)
        {
            await DoMockingSetupAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
            return mockingSetup.TableRecordCounts[typeof(TEntity)];
        }

        [DebuggerStepThrough]
        protected virtual async Task<int> GetNewIDAsync(MockingSetup mockingSetup, string contextProfileName)
        {
            await DoMockingSetupAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
            return await GetCacheCountAsync(mockingSetup, contextProfileName).ConfigureAwait(false) + 1;
        }

        [DebuggerStepThrough]
        protected abstract Mock<DbSet<TEntity>>? GetMockSet(MockingSetup mockingSetup);

        [DebuggerStepThrough]
        protected abstract List<Mock<TEntity>>? GetRawSet(MockingSetup mockingSetup);

        [DebuggerStepThrough]
        protected abstract Task<TModel> GenNewModelAsync(MockingSetup mockingSetup, string contextProfileName);

        [DebuggerStepThrough]
        protected virtual Task AssignNameIfNameableAsync(MockingSetup mockingSetup, TIModel result, string newName)
        {
            // Do Nothing by default
            return Task.CompletedTask;
        }

        [DebuggerStepThrough]
        protected virtual Task AssignNameIfNameableAsync(MockingSetup mockingSetup, TIModel result, bool add2)
        {
            // Do Nothing by default
            return Task.CompletedTask;
        }

        [DebuggerStepThrough]
        protected virtual Task AssignDisplayNameIfDisplayableAsync(MockingSetup mockingSetup, TIModel result, string newName)
        {
            // Do Nothing by default
            return Task.CompletedTask;
        }

        [DebuggerStepThrough]
        protected virtual Task AssignDisplayNameIfDisplayableAsync(MockingSetup mockingSetup, TIModel result, bool add2)
        {
            // Do Nothing by default
            return Task.CompletedTask;
        }

        protected virtual Task VerifyNameIfNameableAsync(MockingSetup mockingSetup, string contextProfileName, TIModel result, bool add2)
        {
            // Do Nothing by default
            return Task.CompletedTask;
        }

        protected virtual Task VerifyNameIfNameableAsync(MockingSetup mockingSetup, TIModel result, string newName)
        {
            // Do Nothing by default
            return Task.CompletedTask;
        }

        protected virtual Task VerifyDisplayNameIfDisplayableAsync(MockingSetup mockingSetup, string contextProfileName, TIModel result, bool add2)
        {
            // Do Nothing by default
            return Task.CompletedTask;
        }

        protected virtual Task VerifyDisplayNameIfDisplayableAsync(MockingSetup mockingSetup, TIModel result, string newName)
        {
            // Do Nothing by default
            return Task.CompletedTask;
        }

        protected virtual void Verify_Get_ByID_Should_ReturnAModelWithFullMap_Results(TIModel result) { }

        protected virtual void Verify_Get_ByKey_Should_ReturnAModelWithFullMap_Results(TIModel result) { }

        protected virtual void Verify_Search_WithAsListingTrue_Should_ReturnAListOfModelsWithListingMapping_Results(List<TIModel> result) { }

        protected virtual void Verify_Search_Should_ReturnAListOfModelsWithLiteMapping_Results(List<TIModel> result) { }

        protected virtual TModel? Create_WithValidData_ModelHook() { return null; }

        protected virtual void Verify_Create_WithValidData_Should_AddToTheDbSetAndSaveChangesAndReturnAModelWithFullMapAndNewID_Results(TIModel? result) { }

        protected virtual TModel? Update_WithValidData_ModelHook() { return null; }

        protected virtual void Verify_Update_WithValidData_Should_UpdateValuesAndUpdatedDateAndReturnAModelWithFullMap_Results(TIModel result) { }

        protected abstract MockingSetup GetMockingSetupWithExistingDataForThisTable();

        protected abstract MockingSetup GetMockingSetupWithExistingDataForThisTableAndExpandedTables();

        protected virtual async Task Verify_Get_ByID_Should_ReturnAModelWithFullMap()
        {
            // Arrange
            var contextProfileName = GenContextProfileName("Verify_Get_ByID_Should_ReturnAModelWithFullMap");
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                // Act
                var result = await new TWorkflow().GetAsync(
                        await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false),
                        contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.NotNull(result);
                Assert.IsType<TModel>(result);
                Assert.Equal(await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result!.ID);
                Assert.True(result.Active);
                Assert.Equal(new(2023, 1, 1), result.CreatedDate);
                Assert.Null(result.UpdatedDate);
                Assert.Equal(await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result.CustomKey);
                await VerifyNameIfNameableAsync(mockingSetup, contextProfileName, result, false).ConfigureAwait(false);
                await VerifyDisplayNameIfDisplayableAsync(mockingSetup, contextProfileName, result, false).ConfigureAwait(false);
                Verify_Get_ByID_Should_ReturnAModelWithFullMap_Results(result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Get_ByKey_Should_ReturnAModelWithFullMap()
        {
            var contextProfileName = GenContextProfileName("Verify_Get_ByKey_Should_ReturnAModelWithFullMap");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                // Act
                var result = await new TWorkflow().GetAsync(
                        (await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false))!,
                        contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.NotNull(result);
                Assert.IsType<TModel>(result);
                Assert.Equal(await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result!.ID);
                Assert.True(result.Active);
                Assert.Equal(new(2023, 1, 1), result.CreatedDate);
                Assert.Null(result.UpdatedDate);
                Assert.Equal(await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result.CustomKey);
                await VerifyNameIfNameableAsync(mockingSetup, contextProfileName, result, false).ConfigureAwait(false);
                await VerifyDisplayNameIfDisplayableAsync(mockingSetup, contextProfileName, result, false).ConfigureAwait(false);
                Verify_Get_ByKey_Should_ReturnAModelWithFullMap_Results(result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_CheckExistsByID_ThatExists_Should_ReturnAnID()
        {
            var contextProfileName = GenContextProfileName("Verify_CheckExistsByID_ThatExists_Should_ReturnAnID");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                // Act
                var result = await new TWorkflow().CheckExistsAsync(
                        await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false),
                        contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.IsType<int>(result);
                Assert.Equal(await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_CheckExistsByKey_ThatExists_Should_ReturnAnID()
        {
            var contextProfileName = GenContextProfileName("Verify_CheckExistsByKey_ThatExists_Should_ReturnAnID");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                // Act
                var result = await new TWorkflow().CheckExistsAsync(
                        (await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false))!,
                        contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.IsType<int>(result);
                Assert.Equal(await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_CheckExistsByID_ThatDoesntExist_Should_ReturnNull()
        {
            var contextProfileName = GenContextProfileName("Verify_CheckExistsByID_ThatDoesntExist_Should_ReturnNull");
            // Arrange/Act/Assert
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                await DoSetupAsync(childContainer, GetMockingSetupWithExistingDataForThisTable(), contextProfileName).ConfigureAwait(false);
                Assert.Null(await new TWorkflow().CheckExistsAsync(IDNotInTheData, contextProfileName).ConfigureAwait(false));
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_CheckExistsByKey_ThatDoesntExist_Should_ReturnNull()
        {
            var contextProfileName = GenContextProfileName("Verify_CheckExistsByKey_ThatDoesntExist_Should_ReturnNull");
            // Arrange/Act/Assert
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                Assert.Null(await new TWorkflow().CheckExistsAsync(KeyNotInTheData, contextProfileName).ConfigureAwait(false));
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_CheckExistsByID_WithAnInvalidID_Should_ThrowAnInvalidOperationException()
        {
            var contextProfileName = GenContextProfileName("Verify_CheckExistsByID_WithAnInvalidID_Should_ThrowAnInvalidOperationException");
            // Arrange/Act/Assert
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                await DoSetupAsync(childContainer, GetMockingSetupWithExistingDataForThisTable(), contextProfileName).ConfigureAwait(false);
                await Assert.ThrowsAsync<InvalidOperationException>(() => new TWorkflow().CheckExistsAsync(0, contextProfileName)).ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_CheckExistsByKey_WithAnInvalidKey_Should_ThrowAnInvalidOperationException()
        {
            var contextProfileName = GenContextProfileName("Verify_CheckExistsByKey_WithAnInvalidKey_Should_ThrowAnInvalidOperationException");
            // Arrange/Act/Assert
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                await DoSetupAsync(childContainer, GetMockingSetupWithExistingDataForThisTable(), contextProfileName).ConfigureAwait(false);
                await Assert.ThrowsAsync<InvalidOperationException>(() => new TWorkflow().CheckExistsAsync(string.Empty, contextProfileName)).ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Resolve_WithAnIDThatExists_Should_ReturnAModelWithFullMap()
        {
            var contextProfileName = GenContextProfileName("Verify_Resolve_WithAnIDThatExists_Should_ReturnAModelWithFullMap");
            // Arrange/Act
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var result = await new TWorkflow().ResolveAsync(await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false), null, (TModel?)null, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.NotNull(result);
                Assert.NotNull(result.Result);
                Assert.IsType<CEFActionResponse<TIModel>>(result);
                Assert.IsType<TModel>(result.Result);
                Assert.Equal(await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result.Result!.ID);
                Assert.True(result.Result.Active);
                Assert.Equal(new(2023, 1, 1), result.Result.CreatedDate);
                Assert.Null(result.Result.UpdatedDate);
                Assert.Equal(await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result.Result.CustomKey);
                Verify_Get_ByID_Should_ReturnAModelWithFullMap_Results(result.Result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Resolve_WithAKeyThatExists_Should_ReturnAModelWithFullMap()
        {
            var contextProfileName = GenContextProfileName("Verify_Resolve_WithAKeyThatExists_Should_ReturnAModelWithFullMap");
            // Arrange/Act
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var result = await new TWorkflow().ResolveAsync(
                        null,
                        await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false),
                        (TModel?)null,
                        contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.NotNull(result);
                Assert.NotNull(result.Result);
                Assert.IsType<CEFActionResponse<TIModel>>(result);
                Assert.IsType<TModel>(result.Result);
                Assert.Equal(await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result.Result!.ID);
                Assert.True(result.Result.Active);
                Assert.Equal(new(2023, 1, 1), result.Result.CreatedDate);
                Assert.Null(result.Result.UpdatedDate);
                Assert.Equal(await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false), result.Result.CustomKey);
                Verify_Get_ByID_Should_ReturnAModelWithFullMap_Results(result.Result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_ResolveWithAutoGenerate_WithAValidModelThatDoesntExist_Should_CreateAndReturnAModelWithFullMap()
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
                model.CustomKey = "NEW KEY";
                await AssignNameIfNameableAsync(mockingSetup, model, "New Name").ConfigureAwait(false);
                await AssignDisplayNameIfDisplayableAsync(mockingSetup, model, "New Display Name").ConfigureAwait(false);
                var newID = GetRawSet(mockingSetup)!.OrderBy(x => x.Object.ID).Last().Object.ID + 1;
                // Act
                var result = await new TWorkflow().ResolveWithAutoGenerateAsync(null, null, model, contextProfileName).ConfigureAwait(false);
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
                await VerifyNameIfNameableAsync(mockingSetup, result.Result, "New Name").ConfigureAwait(false);
                await VerifyDisplayNameIfDisplayableAsync(mockingSetup, result.Result, "New Display Name").ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_ResolveWithAutoGenerate_WithNoDataAndANullModel_Should_ThrowAnInvalidDataException()
        {
            var contextProfileName = GenContextProfileName("Verify_ResolveWithAutoGenerate_WithNoDataAndANullModel_Should_ThrowAnInvalidDataException");
            // Arrange/Act/Assert
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                await DoSetupAsync(childContainer, GetMockingSetupWithExistingDataForThisTable(), contextProfileName).ConfigureAwait(false);
                await Assert.ThrowsAsync<System.IO.InvalidDataException>(() => new TWorkflow().ResolveWithAutoGenerateAsync(null, null, (TModel?)null, contextProfileName)).ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_ResolveWithAutoGenerateOptional_WithNoDataAndANullModel_Should_ReturnNull()
        {
            var contextProfileName = GenContextProfileName("Verify_ResolveWithAutoGenerateOptional_WithNoDataAndANullModel_Should_ReturnNull");
            // Arrange/Act/Assert
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                await DoSetupAsync(childContainer, GetMockingSetupWithExistingDataForThisTable(), contextProfileName).ConfigureAwait(false);
                var response = await new TWorkflow().ResolveWithAutoGenerateOptionalAsync(null, null, (TModel?)null, contextProfileName).ConfigureAwait(false);
                Verify_CEFAR_Failed_WithSingleMessage(response, "WARNING! Unable to auto-generate with the provided information");
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Search_WithAsListingTrue_Should_ReturnAListOfModelsWithListingMapping()
        {
            var contextProfileName = GenContextProfileName("Verify_Search_WithAsListingTrue_Should_ReturnAListOfModelsWithListingMapping");
            // Arrange/Act
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var results = (await new TWorkflow().SearchAsync(new TSearchModel(), true, contextProfileName).ConfigureAwait(false)).results;
                // Assert
                Assert.IsType<List<TIModel>>(results);
                Assert.True(results[0].Active);
                Assert.Equal(await GetCacheCountAsync(mockingSetup, contextProfileName).ConfigureAwait(false), results.Count);
                Verify_Search_WithAsListingTrue_Should_ReturnAListOfModelsWithListingMapping_Results(results);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Search_Should_ReturnAListOfModelsWithLiteMapping()
        {
            var contextProfileName = GenContextProfileName("Verify_Search_Should_ReturnAListOfModelsWithLiteMapping");
            // Arrange/Act
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var results = (await new TWorkflow().SearchAsync(new TSearchModel(), false, contextProfileName).ConfigureAwait(false)).results;
                // Assert
                Assert.IsType<List<TIModel>>(results);
                Assert.True(results[0].Active);
                Assert.Equal(await GetCacheCountAsync(mockingSetup, contextProfileName).ConfigureAwait(false), results.Count);
                Verify_Search_Should_ReturnAListOfModelsWithLiteMapping_Results(results);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_SearchForConnect_Should_ReturnAListOfModelsWithLiteMapping()
        {
            var contextProfileName = GenContextProfileName("Verify_SearchForConnect_Should_ReturnAListOfModelsWithLiteMapping");
            // Arrange/Act
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var results = (await new TWorkflow()
                    .SearchForConnectAsync(new TSearchModel(), contextProfileName).ConfigureAwait(false))
                    ?.ToList();
                // Assert
                Assert.NotNull(results);
                Assert.True(results![0].Active);
                Assert.Equal(await GetCacheCountAsync(mockingSetup, contextProfileName).ConfigureAwait(false), results.Count);
                Verify_Search_Should_ReturnAListOfModelsWithLiteMapping_Results(results);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_GetDigest_Should_ReturnAListOfModelsWithLiteMapping()
        {
            var contextProfileName = GenContextProfileName("Verify_GetDigest_Should_ReturnAListOfModelsWithLiteMapping");
            // Arrange/Act
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTable();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var results = (await new TWorkflow()
                    .GetDigestAsync(contextProfileName).ConfigureAwait(false))
                    ?.ToList();
                // Assert
                Assert.NotNull(results);
                Assert.True(results![0].ID > 0);
                Assert.NotNull(results[0].Key);
                Assert.Equal(
                    await GetCacheCountAsync(mockingSetup, contextProfileName).ConfigureAwait(false)
                    - mockingSetup.MockContext.Object.Set<TEntity>().Count(x => !x.Active),
                    results.Count);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Create_WithValidData_Should_AddToTheDbSetAndSaveChangesAndReturnAModelWithFullMapAndNewID()
        {
            var contextProfileName = GenContextProfileName("Verify_Create_WithValidData_Should_AddToTheDbSetAndSaveChangesAndReturnAModelWithFullMapAndNewID");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                mockingSetup.SaveChangesResult = 1;
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                await DoMockingSetupAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
                var model = Create_WithValidData_ModelHook()
                    ?? await GenNewModelAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
                model.CustomKey = "XR";
                var workflow = new TWorkflow();
                // Act
                var result = await workflow.CreateAsync(model, contextProfileName).ConfigureAwait(false);
                Verify_CEFAR_Passed_WithNoMessages(result);
                if (typeof(TModel) == typeof(CartModel))
                {
                    // Skip this test
                    var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => new TWorkflow().GetAsync(result.Result, contextProfileName)).ConfigureAwait(false);
                    Assert.Equal("Getting a cart requires additional information.", ex.Message);
                    return;
                }
                var resultModel = await workflow.GetAsync(result.Result, contextProfileName).ConfigureAwait(false);
                // Assert
                GetMockSet(mockingSetup)!.Verify(m => m.Add(It.IsAny<TEntity>()), Times.Once());
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce());
                Assert.NotNull(resultModel);
                Assert.IsType<TModel>(resultModel);
                Verify_Create_WithValidData_Should_AddToTheDbSetAndSaveChangesAndReturnAModelWithFullMapAndNewID_Results(resultModel);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Create_WithADuplicateKey_Should_ThrowAnInvalidOperationException()
        {
            var contextProfileName = GenContextProfileName("Verify_Create_WithADuplicateKey_Should_ThrowAnInvalidOperationException");
            // Arrange/Act/Assert
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                await Assert.ThrowsAsync<InvalidOperationException>(async () => await new TWorkflow()
                        .CreateAsync(
                            await GenNewModelAsync(mockingSetup, contextProfileName).ConfigureAwait(false),
                            contextProfileName)
                        .ConfigureAwait(false))
                    .ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Create_WithDataThatCantBeSaved_Should_ThrowAnInvalidDataException()
        {
            var contextProfileName = GenContextProfileName("Verify_Create_WithDataThatCantBeSaved_Should_ThrowAnInvalidDataException");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                mockingSetup.SaveChangesResult = -1;
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                await DoMockingSetupAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
                var model = Create_WithValidData_ModelHook()
                    ?? await GenNewModelAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
                model.CustomKey = "XR";
                // Act/Assert
                await Assert.ThrowsAsync<System.IO.InvalidDataException>(() => new TWorkflow().CreateAsync(model, contextProfileName)).ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Create_WithAPositiveNonMaximumId_Should_ThrowAnInvalidOperationException()
        {
            var contextProfileName = GenContextProfileName("Verify_Create_WithAPositiveNonMaximumId_Should_ThrowAnInvalidOperationException");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var workflow = new TWorkflow();
                foreach (var id in PositiveNonMaxIDs)
                {
                    // Act/Assert
                    await Assert.ThrowsAsync<InvalidOperationException>(async () => await workflow.CreateAsync(
                                new TModel { ID = id },
                                contextProfileName)
                            .ConfigureAwait(false))
                        .ConfigureAwait(false);
                }
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Update_WithValidData_Should_UpdateValuesAndUpdatedDateAndReturnAModelWithFullMap()
        {
            var contextProfileName = GenContextProfileName("Verify_Update_WithValidData_Should_UpdateValuesAndUpdatedDateAndReturnAModelWithFullMap");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                mockingSetup.SaveChangesResult = 1;
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var model = Update_WithValidData_ModelHook()
                    ?? await GenNewModelAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
                model.ID = await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
                await AssignNameIfNameableAsync(mockingSetup, model, true).ConfigureAwait(false);
                await AssignDisplayNameIfDisplayableAsync(mockingSetup, model, true).ConfigureAwait(false);
                var workflow = new TWorkflow();
                // Act
                var result = await workflow.UpdateAsync(model, contextProfileName).ConfigureAwait(false);
                var resultModel = await workflow.GetAsync(result.Result, contextProfileName).ConfigureAwait(false);
                // Assert
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce());
                Assert.NotNull(resultModel);
                Assert.NotNull(resultModel!.UpdatedDate);
                await VerifyNameIfNameableAsync(mockingSetup, contextProfileName, resultModel, true).ConfigureAwait(false);
                await VerifyDisplayNameIfDisplayableAsync(mockingSetup, contextProfileName, resultModel, true).ConfigureAwait(false);
                Verify_Update_WithValidData_Should_UpdateValuesAndUpdatedDateAndReturnAModelWithFullMap_Results(resultModel);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Update_WithDataThatCantBeSaved_Should_ThrowAnInvalidDataException()
        {
            var contextProfileName = GenContextProfileName("Verify_Update_WithDataThatCantBeSaved_Should_ThrowAnInvalidDataException");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                mockingSetup.SaveChangesResult = -1;
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var model = Update_WithValidData_ModelHook()
                    ?? await GenNewModelAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
                model.ID = await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
                // Act/Assert
                await Assert.ThrowsAsync<System.IO.InvalidDataException>(async () => await new TWorkflow()
                        .UpdateAsync(
                            model,
                            contextProfileName)
                        .ConfigureAwait(false))
                    .ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Update_WithAnIDLessThanOrEqualToZeroOrMinOrMax_Should_ThrowAnInvalidOperationException()
        {
            var contextProfileName = GenContextProfileName("Verify_Update_WithAnIDLessThanOrEqualToZeroOrMinOrMax_Should_ThrowAnInvalidOperationException");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var workflow = new TWorkflow();
                foreach (var id in InvalidIDs)
                {
                    // Act/Assert
                    await Assert.ThrowsAsync<InvalidOperationException>(
                            async () => await workflow.UpdateAsync(
                                    new TModel { ID = id },
                                    contextProfileName)
                                .ConfigureAwait(false))
                        .ConfigureAwait(false);
                }
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Update_WithAnIDNotInTheData_Should_ThrowAnArgumentException()
        {
            var contextProfileName = GenContextProfileName("Verify_Update_WithAnIDNotInTheData_Should_ThrowAnArgumentException");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var workflow = new TWorkflow();
                await DoMockingSetupAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
                foreach (var id in IDsNotInTheData)
                {
                    // Act/Assert
                    await Assert.ThrowsAsync<ArgumentException>(
                            async () => await workflow.UpdateAsync(
                                    new TModel { ID = id },
                                    contextProfileName)
                                .ConfigureAwait(false))
                        .ConfigureAwait(false);
                }
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Upsert_WithValidData_ThatShouldCreate_Should_AddToTheDbSetAndSaveChangesAndReturnAModelWithFullMapAndNewID()
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
                model.CustomKey = "XR";
                await AssignNameIfNameableAsync(mockingSetup, model, "TEST").ConfigureAwait(false);
                await AssignDisplayNameIfDisplayableAsync(mockingSetup, model, "TEST").ConfigureAwait(false);
                var workflow = new TWorkflow();
                // Act
                var result = await workflow.UpsertAsync(model, contextProfileName).ConfigureAwait(false);
                var resultModel = await workflow.GetAsync(result.Result, contextProfileName).ConfigureAwait(false);
                // Assert
                GetMockSet(mockingSetup)!.Verify(m => m.Add(It.IsAny<TEntity>()), Times.Once());
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce());
                Assert.NotNull(resultModel);
                Assert.Null(resultModel!.UpdatedDate);
                Assert.Equal("XR", resultModel.CustomKey);
                await VerifyNameIfNameableAsync(mockingSetup, resultModel, "TEST").ConfigureAwait(false);
                await VerifyDisplayNameIfDisplayableAsync(mockingSetup, resultModel, "TEST").ConfigureAwait(false);
                Verify_Create_WithValidData_Should_AddToTheDbSetAndSaveChangesAndReturnAModelWithFullMapAndNewID_Results(resultModel);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Upsert_WithValidData_ThatShouldUpdate_Should_UpdateValuesAndUpdatedDateAndReturnAModelWithFullMap()
        {
            var contextProfileName = GenContextProfileName("Verify_Upsert_WithValidData_ThatShouldUpdate_Should_UpdateValuesAndUpdatedDateAndReturnAModelWithFullMap");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                mockingSetup.SaveChangesResult = 1;
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var model = Update_WithValidData_ModelHook()
                    ?? await GenNewModelAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
                await AssignNameIfNameableAsync(mockingSetup, model, true).ConfigureAwait(false);
                await AssignDisplayNameIfDisplayableAsync(mockingSetup, model, true).ConfigureAwait(false);
                var workflow = new TWorkflow();
                // Act
                var result = await workflow.UpsertAsync(model, contextProfileName).ConfigureAwait(false);
                if (typeof(TModel) == typeof(CartModel))
                {
                    // Skip this test
                    var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => new TWorkflow().GetAsync(result.Result, contextProfileName)).ConfigureAwait(false);
                    Assert.Equal("Getting a cart requires additional information.", ex.Message);
                    return;
                }
                var resultModel = await workflow.GetAsync(result.Result, contextProfileName).ConfigureAwait(false);
                // Assert
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce());
                Assert.NotNull(resultModel);
                Assert.NotNull(resultModel!.UpdatedDate);
                await VerifyNameIfNameableAsync(mockingSetup, contextProfileName, resultModel, true).ConfigureAwait(false);
                await VerifyDisplayNameIfDisplayableAsync(mockingSetup, contextProfileName, resultModel, true).ConfigureAwait(false);
                Verify_Update_WithValidData_Should_UpdateValuesAndUpdatedDateAndReturnAModelWithFullMap_Results(resultModel);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Upsert_WithValidDataButNoID_ThatShouldUpdate_Should_UpdateValuesAndUpdatedDateAndReturnAModelWithFullMap()
        {
            var contextProfileName = GenContextProfileName("Verify_Upsert_WithValidDataButNoID_ThatShouldUpdate_Should_UpdateValuesAndUpdatedDateAndReturnAModelWithFullMap");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                mockingSetup.SaveChangesResult = 1;
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var model = Update_WithValidData_ModelHook()
                    ?? await GenNewModelAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
                await AssignNameIfNameableAsync(mockingSetup, model, true).ConfigureAwait(false);
                await AssignDisplayNameIfDisplayableAsync(mockingSetup, model, true).ConfigureAwait(false);
                var workflow = new TWorkflow();
                // Act
                var result = await workflow.UpsertAsync(model, contextProfileName).ConfigureAwait(false);
                if (typeof(TModel) == typeof(CartModel))
                {
                    var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => new TWorkflow().GetAsync(result.Result, contextProfileName)).ConfigureAwait(false);
                    Assert.Equal("Getting a cart requires additional information.", ex.Message);
                    return;
                }
                var resultModel = await workflow.GetAsync(result.Result, contextProfileName).ConfigureAwait(false);
                // Assert
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce());
                Assert.NotNull(resultModel);
                Assert.NotNull(resultModel!.UpdatedDate);
                await VerifyNameIfNameableAsync(mockingSetup, contextProfileName, resultModel, true).ConfigureAwait(false);
                await VerifyDisplayNameIfDisplayableAsync(mockingSetup, contextProfileName, resultModel, true).ConfigureAwait(false);
                Verify_Update_WithValidData_Should_UpdateValuesAndUpdatedDateAndReturnAModelWithFullMap_Results(resultModel);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Deactivate_ByID_WithAValidIDInTheData_Should_UpdateActiveAndUpdatedDateValuesAndSaveChanges()
        {
            var contextProfileName = GenContextProfileName("Verify_Deactivate_ByID_WithAValidIDInTheData_Should_UpdateActiveAndUpdatedDateValuesAndSaveChanges");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                mockingSetup.SaveChangesResult = 1;
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var workflow = new TWorkflow();
                var id = await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
                // Act
                var result1 = await workflow.DeactivateAsync(id, contextProfileName).ConfigureAwait(false);
                var result2 = await workflow.GetAsync(id, contextProfileName).ConfigureAwait(false);
                // Assert
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce());
                Verify_CEFAR_Passed_WithNoMessages(result1);
                Assert.NotNull(result2);
                Assert.NotNull(result2!.UpdatedDate);
                Assert.False(result2.Active);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Deactivate_ByID_WithAnIDNotInTheData_Should_ReturnFalse()
        {
            var contextProfileName = GenContextProfileName("Verify_Deactivate_ByID_WithAnIDNotInTheData_Should_ReturnFalse");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTable();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var workflow = new TWorkflow();
                foreach (var id in IDsNotInTheData)
                {
                    // Act/Assert
                    Verify_CEFAR_Failed_WithSingleMessage(
                        await workflow.DeactivateAsync(id, contextProfileName).ConfigureAwait(false),
                        "ERROR! Cannot Deactivate a null record");
                }
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Deactivate_ByID_WithAnInvalidID_Should_ThrowAnInvalidOperationException()
        {
            var contextProfileName = GenContextProfileName("Verify_Deactivate_ByID_WithAnInvalidID_Should_ThrowAnInvalidOperationException");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                await DoSetupAsync(childContainer, new(), contextProfileName).ConfigureAwait(false);
                var workflow = new TWorkflow();
                foreach (var id in InvalidIDs)
                {
                    // Act/Assert
                    await Assert.ThrowsAsync<InvalidOperationException>(
                            () => workflow.DeactivateAsync(id, contextProfileName))
                        .ConfigureAwait(false);
                }
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Deactivate_ByID_ThatIsNotActive_Should_NotUpdateItemAndReturnTrue()
        {
            var contextProfileName = GenContextProfileName("Verify_Deactivate_ByID_ThatIsNotActive_Should_NotUpdateItemAndReturnTrue");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                mockingSetup.DoInactives = true;
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var workflow = new TWorkflow();
                // Act
                var result1 = await workflow.DeactivateAsync(await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false), contextProfileName).ConfigureAwait(false);
                var result2 = await workflow.GetAsync(await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false), contextProfileName).ConfigureAwait(false);
                // Assert
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.Never); // we didn't save any changes
                Verify_CEFAR_Passed_WithNoMessages(result1);
                Assert.NotNull(result2);
                Assert.Null(result2!.UpdatedDate); // We didn't update anything
                Assert.False(result2.Active); // It was inactive without edit
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Deactivate_ByKey_WithAValidKeyInTheData_Should_UpdateActiveAndUpdatedDateValuesAndSaveChangesAndNoLongerBeGettable()
        {
            var contextProfileName = GenContextProfileName("Verify_Deactivate_ByKey_WithAValidKeyInTheData_Should_UpdateActiveAndUpdatedDateValuesAndSaveChangesAndNoLongerBeGettable");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                mockingSetup.SaveChangesResult = 1;
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var workflow = new TWorkflow();
                // Act
                var result1 = await workflow.DeactivateAsync((await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false))!, contextProfileName).ConfigureAwait(false);
                var result2 = await workflow.GetAsync((await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false))!, contextProfileName).ConfigureAwait(false);
                // Assert
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce());
                Verify_CEFAR_Passed_WithNoMessages(result1);
                Assert.Null(result2);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Deactivate_ByKey_WithAKeyNotInTheData_Should_ReturnFalse()
        {
            var contextProfileName = GenContextProfileName("Verify_Deactivate_ByKey_WithAKeyNotInTheData_Should_ReturnFalse");
            // Arrange/Act/Assert
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                await DoSetupAsync(childContainer, GetMockingSetupWithExistingDataForThisTable(), contextProfileName).ConfigureAwait(false);
                Verify_CEFAR_Failed_WithSingleMessage(await new TWorkflow().DeactivateAsync("TEST", contextProfileName).ConfigureAwait(false), "ERROR! Cannot Deactivate a null record");
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Deactivate_ByKey_WithAnInvalidKey_Should_ThrowAnInvalidOperationException()
        {
            var contextProfileName = GenContextProfileName("Verify_Deactivate_ByKey_WithAnInvalidKey_Should_ThrowAnInvalidOperationException");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                await DoSetupAsync(childContainer, new(), contextProfileName).ConfigureAwait(false);
                var workflow = new TWorkflow();
                foreach (var key in InvalidKeys)
                {
                    // Act/Assert
                    await Assert.ThrowsAsync<InvalidOperationException>(
                            () => workflow.DeactivateAsync(key!, contextProfileName))
                        .ConfigureAwait(false);
                }
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Deactivate_ByKey_ThatIsNotActive_Should_NotUpdateItemAndReturnTrueAndNoLongerBeGettable()
        {
            var contextProfileName = GenContextProfileName("Verify_Deactivate_ByKey_ThatIsNotActive_Should_NotUpdateItemAndReturnTrueAndNoLongerBeGettable");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                mockingSetup.DoInactives = true;
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var workflow = new TWorkflow();
                // Act
                var result1 = await workflow.DeactivateAsync((await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false))!, contextProfileName).ConfigureAwait(false);
                var result2 = await workflow.GetAsync((await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false))!, contextProfileName).ConfigureAwait(false);
                // Assert
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.Never); // we didn't save any changes
                Verify_CEFAR_Passed_WithNoMessages(result1);
                Assert.Null(result2); // We didn't update anything
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Reactivate_ByID_WithAValidIDInTheData_Should_UpdateActiveAndUpdatedDateValuesAndSaveChanges()
        {
            var contextProfileName = GenContextProfileName("Verify_Reactivate_ByID_WithAValidIDInTheData_Should_UpdateActiveAndUpdatedDateValuesAndSaveChanges");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                mockingSetup.DoInactives = true;
                mockingSetup.SaveChangesResult = 1;
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var workflow = new TWorkflow();
                // Act
                var result1 = await workflow.ReactivateAsync(await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false), contextProfileName).ConfigureAwait(false);
                var result2 = await workflow.GetAsync(await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false), contextProfileName).ConfigureAwait(false);
                // Assert
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
                Verify_CEFAR_Passed_WithNoMessages(result1);
                Assert.NotNull(result2);
                Assert.NotNull(result2!.UpdatedDate);
                Assert.True(result2.Active);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Reactivate_ByID_WithAnIDNotInTheData_Should_ReturnFalse()
        {
            var contextProfileName = GenContextProfileName("Verify_Reactivate_ByID_WithAnIDNotInTheData_Should_ReturnFalse");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                await DoSetupAsync(childContainer, GetMockingSetupWithExistingDataForThisTable(), contextProfileName).ConfigureAwait(false);
                var workflow = new TWorkflow();
                foreach (var id in IDsNotInTheData)
                {
                    // Act/Assert
                    Verify_CEFAR_Failed_WithSingleMessage(
                        await workflow.ReactivateAsync(id, contextProfileName).ConfigureAwait(false),
                        "ERROR! Cannot Reactivate a null record");
                }
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Reactivate_ByID_WithAnInvalidID_Should_ThrowAnInvalidOperationException()
        {
            var contextProfileName = GenContextProfileName("Verify_Reactivate_ByID_WithAnInvalidID_Should_ThrowAnInvalidOperationException");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                await DoSetupAsync(childContainer, new(), contextProfileName).ConfigureAwait(false);
                var workflow = new TWorkflow();
                foreach (var id in InvalidIDs)
                {
                    // Act/Assert
                    await Assert.ThrowsAsync<InvalidOperationException>(
                            () => workflow.ReactivateAsync(id, contextProfileName))
                        .ConfigureAwait(false);
                }
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Reactivate_ByID_ThatIsActive_Should_NotUpdateItemAndReturnTrue()
        {
            var contextProfileName = GenContextProfileName("Verify_Reactivate_ByID_ThatIsActive_Should_NotUpdateItemAndReturnTrue");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var workflow = new TWorkflow();
                // Act
                var result1 = await workflow.ReactivateAsync(await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false), contextProfileName).ConfigureAwait(false);
                var result2 = await workflow.GetAsync(await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false), contextProfileName).ConfigureAwait(false);
                // Assert
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.Never); // we didn't save any changes
                Verify_CEFAR_Passed_WithNoMessages(result1);
                Assert.NotNull(result2);
                Assert.Null(result2!.UpdatedDate); // We didn't update anything
                Assert.True(result2.Active); // It was active without edit
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Reactivate_ByKey_WithAValidKeyInTheData_Should_UpdateActiveAndUpdatedDateValuesAndSaveChanges()
        {
            var contextProfileName = GenContextProfileName("Verify_Reactivate_ByKey_WithAValidKeyInTheData_Should_UpdateActiveAndUpdatedDateValuesAndSaveChanges");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                mockingSetup.DoInactives = true;
                mockingSetup.SaveChangesResult = 1;
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var workflow = new TWorkflow();
                // Act
                var result1 = await workflow.ReactivateAsync((await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false))!, contextProfileName).ConfigureAwait(false);
                var result2 = await workflow.GetAsync((await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false))!, contextProfileName).ConfigureAwait(false);
                // Assert
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
                Verify_CEFAR_Passed_WithNoMessages(result1);
                Assert.NotNull(result2);
                Assert.NotNull(result2!.UpdatedDate);
                Assert.True(result2.Active);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Reactivate_ByKey_WithAKeyNotInTheData_Should_ReturnFalse()
        {
            var contextProfileName = GenContextProfileName("Verify_Reactivate_ByKey_WithAKeyNotInTheData_Should_ReturnFalse");
            // Arrange/Act/Assert
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                await DoSetupAsync(childContainer, GetMockingSetupWithExistingDataForThisTable(), contextProfileName).ConfigureAwait(false);
                Verify_CEFAR_Failed_WithSingleMessage(await new TWorkflow().ReactivateAsync(KeyNotInTheData, contextProfileName).ConfigureAwait(false), "ERROR! Cannot Reactivate a null record");
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Reactivate_ByKey_WithAnInvalidKey_Should_ThrowAnInvalidOperationException()
        {
            var contextProfileName = GenContextProfileName("Verify_Reactivate_ByKey_WithAnInvalidKey_Should_ThrowAnInvalidOperationException");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                await DoSetupAsync(childContainer, new(), contextProfileName).ConfigureAwait(false);
                var workflow = new TWorkflow();
                foreach (var key in InvalidKeys)
                {
                    // Act/Assert
                    await Assert.ThrowsAsync<InvalidOperationException>(
                            () => workflow.ReactivateAsync(key!, contextProfileName))
                        .ConfigureAwait(false);
                }
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Reactivate_ByKey_ThatIsActive_Should_NotUpdateItemAndReturnTrue()
        {
            var contextProfileName = GenContextProfileName("Verify_Reactivate_ByKey_ThatIsActive_Should_NotUpdateItemAndReturnTrue");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var workflow = new TWorkflow();
                // Act
                var result1 = await workflow.ReactivateAsync((await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false))!, contextProfileName).ConfigureAwait(false);
                var result2 = await workflow.GetAsync((await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false))!, contextProfileName).ConfigureAwait(false);
                // Assert
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.Never); // we didn't save any changes
                Verify_CEFAR_Passed_WithNoMessages(result1);
                Assert.NotNull(result2);
                Assert.Null(result2!.UpdatedDate); // We didn't update anything
                Assert.True(result2.Active); // It was active without edit
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Delete_ByID_WithAValidIDInTheData_Should_RemoveAndSaveChanges()
        {
            var contextProfileName = GenContextProfileName("Verify_Delete_ByID_WithAValidIDInTheData_Should_RemoveAndSaveChanges");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTable();
                mockingSetup.SaveChangesResult = 1;
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                // Act/Assert
                Verify_CEFAR_Passed_WithNoMessages(await new TWorkflow().DeleteAsync(await GetIDAsync(mockingSetup, contextProfileName).ConfigureAwait(false), contextProfileName).ConfigureAwait(false));
                GetMockSet(mockingSetup)!.Verify(m => m.Remove(It.IsAny<TEntity>()), Times.Once());
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Delete_ByID_WithAnIDNotInTheData_Should_ReturnTrue()
        {
            var contextProfileName = GenContextProfileName("Verify_Delete_ByID_WithAnIDNotInTheData_Should_ReturnTrue");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                await DoSetupAsync(childContainer, GetMockingSetupWithExistingDataForThisTable(), contextProfileName).ConfigureAwait(false);
                var workflow = new TWorkflow();
                foreach (var id in IDsNotInTheData)
                {
                    // Act/Assert
                    Verify_CEFAR_Passed_WithNoMessages(
                        await workflow.DeleteAsync(id, contextProfileName).ConfigureAwait(false));
                }
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Delete_ByID_WithAnInvalidID_Should_ThrowAnInvalidOperationException()
        {
            var contextProfileName = GenContextProfileName("Verify_Delete_ByID_WithAnInvalidID_Should_ThrowAnInvalidOperationException");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                await DoSetupAsync(childContainer, new(), contextProfileName).ConfigureAwait(false);
                var workflow = new TWorkflow();
                foreach (var id in InvalidIDs)
                {
                    // Act/Assert
                    await Assert.ThrowsAsync<InvalidOperationException>(
                            () => workflow.DeleteAsync(id, contextProfileName))
                        .ConfigureAwait(false);
                }
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Delete_ByKey_WithAValidKeyInTheData_Should_RemoveAndSaveChanges()
        {
            var contextProfileName = GenContextProfileName("Verify_Delete_ByKey_WithAValidKeyInTheData_Should_RemoveAndSaveChanges");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTable();
                mockingSetup.SaveChangesResult = 1;
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                // Act/Assert
                Verify_CEFAR_Passed_WithNoMessages(await new TWorkflow().DeleteAsync((await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false))!, contextProfileName).ConfigureAwait(false));
                GetMockSet(mockingSetup)!.Verify(m => m.Remove(It.IsAny<TEntity>()), Times.Once());
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Delete_ByKey_WithAKeyNotInTheData_Should_ReturnTrue()
        {
            var contextProfileName = GenContextProfileName("Verify_Delete_ByKey_WithAKeyNotInTheData_Should_ReturnTrue");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                await DoSetupAsync(childContainer, GetMockingSetupWithExistingDataForThisTable(), contextProfileName).ConfigureAwait(false);
                // Act/Assert
                Verify_CEFAR_Passed_WithNoMessages(await new TWorkflow().DeleteAsync(KeyNotInTheData, contextProfileName).ConfigureAwait(false));
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected virtual async Task Verify_Delete_ByKey_WithAnInvalidKey_Should_ThrowAnInvalidOperationException()
        {
            var contextProfileName = GenContextProfileName("Verify_Delete_ByKey_WithAnInvalidKey_Should_ThrowAnInvalidOperationException");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                await DoSetupAsync(childContainer, new(), contextProfileName).ConfigureAwait(false);
                var workflow = new TWorkflow();
                foreach (var key in InvalidKeys)
                {
                    // Act/Assert
                    await Assert.ThrowsAsync<InvalidOperationException>(
                            () => workflow.DeleteAsync(key!, contextProfileName))
                        .ConfigureAwait(false);
                }
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [DebuggerStepThrough]
        protected string GenContextProfileName(string functionName)
        {
            return new StringBuilder().Append(GetType().Name).Append('|').Append(functionName).ToString();
        }

        protected virtual string KeyNotInTheData => "BOB";

        protected virtual int IDNotInTheData => int.MaxValue - 1;

        protected virtual int[] IDsNotInTheData { get; } = { 501, 1000, 10_000, 100_000, int.MaxValue - 1 };

        protected virtual int?[] InvalidIDsNullable { get; } = { null, 0, -1, -2, -100_000, int.MinValue, int.MaxValue };

        protected virtual int[] PositiveNonMaxIDs { get; } = { 5, 10, 501, 1000, 10_000, 100_000, int.MaxValue - 1 };

        protected virtual int[] InvalidIDs { get; } = { 0, -1, -2, -100_000, int.MinValue, int.MaxValue };

        protected virtual string?[] InvalidKeys { get; } = { null, " ", "  ", "   ", "\r", "\r\n", "\n" };
    }
}
