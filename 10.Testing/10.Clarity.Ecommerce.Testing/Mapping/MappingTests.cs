// <copyright file="MappingTests.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the mapping tests class</summary>
namespace Clarity.Ecommerce.Mapper.Testing
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Clarity.Ecommerce.DataModel;
    using Clarity.Ecommerce.Utilities;
    using Ecommerce.Testing;
    using Interfaces.Models;
    using Mapper;
    using Newtonsoft.Json;
    using Xunit;
    using Xunit.Abstractions;

    [Trait("Category", "Mapping")]
    public class MappingTests : XUnitLogHelper
    {
        public MappingTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact(Skip = "Don't run automatically")]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task Validate_MappingWithExpressions()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            BaseModelMapper.Initialize();
            var selectorFull = ModelMapperForAccount.PreBuiltAccountSQLSelectorFull!;
            var selectorList = ModelMapperForAccount.PreBuiltAccountSQLSelectorList!;
            var selectorFullCompiled = selectorFull.Compile();
            var selectorListCompiled = selectorList.Compile();
            var jsonSettings = new JsonSerializerSettings
            {
                // NullValueHandling = NullValueHandling.Ignore, // Get rid of NULLs
                // DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, // Get rid of default values
                // Formatting = Formatting.None, // no whitespace, keeps the size down
                DateFormatHandling = DateFormatHandling.IsoDateFormat, // Use a legible format
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                // ContractResolver = new SkipEmptyContractResolver(),
            };
#if FALSE
            Expression<Func<Account, ModelMapperForAccount.AnonAccount>> preBuiltAccountSQLSelectorFullAlt1 = x => x == null ? null : new ModelMapperForAccount.AnonAccount
            {
                TypeID = x.TypeID,
                StatusID = x.StatusID,
                IsTaxable = x.IsTaxable,
                TaxExemptionNo = x.TaxExemptionNo,
                TaxEntityUseCode = x.TaxEntityUseCode,
                IsOnHold = x.IsOnHold,
                Credit = x.Credit,
                Token = x.Token,
                SageID = x.SageID,
                CreditCurrencyID = x.CreditCurrencyID,
                Name = x.Name,
                Description = x.Description,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
            Expression<Func<AccountImage, ModelMapperForAccountImage.AnonAccountImage>> preBuiltAccountImageSQLSelectorListAlt1 = x => x == null ? null : new ModelMapperForAccountImage.AnonAccountImage
            {
                MasterID = x.MasterID,
                TypeID = x.TypeID,
                SortOrder = x.SortOrder,
                DisplayName = x.DisplayName,
                SeoTitle = x.SeoTitle,
                Author = x.Author,
                MediaDate = x.MediaDate,
                Copyright = x.Copyright,
                Location = x.Location,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                IsPrimary = x.IsPrimary,
                OriginalWidth = x.OriginalWidth,
                OriginalHeight = x.OriginalHeight,
                OriginalFileFormat = x.OriginalFileFormat,
                OriginalFileName = x.OriginalFileName,
                OriginalIsStoredInDB = x.OriginalIsStoredInDB,
                OriginalBytes = x.OriginalBytes,
                ThumbnailWidth = x.ThumbnailWidth,
                ThumbnailHeight = x.ThumbnailHeight,
                ThumbnailFileFormat = x.ThumbnailFileFormat,
                ThumbnailFileName = x.ThumbnailFileName,
                ThumbnailIsStoredInDB = x.ThumbnailIsStoredInDB,
                ThumbnailBytes = x.ThumbnailBytes,
                Name = x.Name,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
            Expression<Func<AccountImageType, ModelMapperForAccountImageType.AnonAccountImageType>> preBuiltAccountImageTypeSQLSelectorList = x => x == null ? null : new ModelMapperForAccountImageType.AnonAccountImageType
            {
                DisplayName = x.DisplayName,
                SortOrder = x.SortOrder,
                TranslationKey = x.TranslationKey,
                Name = x.Name,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
            Expression<Func<AccountFile, AnonAccountFile>> PreBuiltAccountFileSQLSelectorList = x => x == null ? null : new AnonAccountFile
            {
                SeoUrl = x.SeoUrl,
                MasterID = x.MasterID,
                SlaveID = x.SlaveID,
                ////Slave = ModelMapperForStoredFile.PreBuiltStoredFileSQLSelectorList.Expand().Compile().Invoke(x.Slave), // For Flattening Properties (List)
                FileAccessTypeID = x.FileAccessTypeID,
                SortOrder = x.SortOrder,
                Name = x.Name,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
                ////Master = ModelMapperForAccount.PreBuiltAccountSQLSelectorList.Expand().Compile().Invoke(x.Master), // For Flattening Properties
            };
            Expression<Func<StoredFile, AnonStoredFile>> PreBuiltStoredFileSQLSelectorList = x => x == null ? null : new AnonStoredFile
            {
                SortOrder = x.SortOrder,
                DisplayName = x.DisplayName,
                SeoTitle = x.SeoTitle,
                Author = x.Author,
                Copyright = x.Copyright,
                FileFormat = x.FileFormat,
                FileName = x.FileName,
                IsStoredInDB = x.IsStoredInDB,
                Name = x.Name,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
            Expression<Func<BrandAccount, AnonBrandAccount>> PreBuiltBrandAccountSQLSelectorList = x => x == null ? null : new AnonBrandAccount
            {
                MasterID = x.MasterID,
                ////Master = ModelMapperForBrand.PreBuiltBrandSQLSelectorList.Expand().Compile().Invoke(x.Master), // For Flattening Properties (List)
                SlaveID = x.SlaveID,
                ////Slave = ModelMapperForAccount.PreBuiltAccountSQLSelectorList.Expand().Compile().Invoke(x.Slave), // For Flattening Properties (List)
                HasAccessToBrand = x.HasAccessToBrand,
                PricePointID = x.PricePointID,
                ////PricePoint = ModelMapperForPricePoint.PreBuiltPricePointSQLSelectorList.Expand().Compile().Invoke(x.PricePoint), // For Flattening Properties (List)
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
            Expression<Func<Brand, AnonBrand>> PreBuiltBrandSQLSelectorList;
            const int accountID = 1;
            // ==============================================================================================
            {
                TestOutputHelper.WriteLine("By Expression theoretical chain Single");
                var startTimeByExprTheoreticalChain = DateTime.Now;
                TestOutputHelper.WriteLine(startTimeByExprTheoreticalChain.ToString("O"));

                var imageTypeIDs = context.AccountImages
                    .AsNoTracking()
                    .AsExpandable()
                    .FilterByActive(true)
                    .FilterAccountImagesByMasterID(accountID, false)
                    .Select(x => x.TypeID)
                    .Distinct()
                    .ToArray();

                var imageTypes = context.AccountImageTypes
                    .AsNoTracking()
                    .AsExpandable()
                    .FilterByActive(true)
                    .FilterByIDs(imageTypeIDs)
                    .Select(preBuiltAccountImageTypeSQLSelectorList)
                    .ToList();

                ////var imageTypeModels = imageTypes
                ////    .Select(ModelMapperForAccountImageType.MapAccountImageTypeModelFromEntityList)
                ////    .ToList();

                var images = context.AccountImages
                    .AsNoTracking()
                    .AsExpandable()
                    .FilterByActive(true)
                    .FilterAccountImagesByMasterID(accountID, false)
                    .Select(preBuiltAccountImageSQLSelectorListAlt1)
                    .ToList();

                var imagesWithTypes = images
                    .Join(
                        imageTypes,
                        x => x.TypeID,
                        x => x.ID,
                        (arg1, arg2) =>
                        {
                            arg1.Type = arg2;
                            return arg1;
                        });

                var accountTest = context.Accounts
                    .AsNoTracking()
                    .AsExpandable()
                    .FilterByActive(true)
                    .FilterByID(accountID)
                    .Select(preBuiltAccountSQLSelectorFullAlt1)
                    .SingleOrDefault();

                accountTest.Images = imagesWithTypes;
                var accountTestModel = accountTest.CreateAccountModelFromEntityFull();

                TestOutputHelper.WriteLine(JsonConvert.SerializeObject(accountTestModel));
                TestOutputHelper.WriteLine((DateTime.Now - startTimeByExprTheoreticalChain).ToString("G"));
            }
            // ==============================================================================================
            {
                TestOutputHelper.WriteLine("By Expression-Direct Single");
                var startTimeByExprDirect = DateTime.Now;
                TestOutputHelper.WriteLine(startTimeByExprDirect.ToString("O"));
                var accountByExprSingleDirect = context.Accounts
                    .AsNoTracking()
                    .AsExpandable()
                    .OrderBy(x => x.ID)
                    .Skip(0)
                    .Take(1)
                    .Select(selectorFull)
                    .FirstOrDefault();
                var accountByExprSingleDirectModel = accountByExprSingleDirect.CreateAccountModelFromEntityFull();
                TestOutputHelper.WriteLine(JsonConvert.SerializeObject(accountByExprSingleDirectModel));
                TestOutputHelper.WriteLine((DateTime.Now - startTimeByExprDirect).ToString("G"));
            }
            // ==============================================================================================
            {
                TestOutputHelper.WriteLine("By Expression-Compiled Batch");
                var startTimeByExprCompiled2 = DateTime.Now;
                TestOutputHelper.WriteLine(startTimeByExprCompiled2.ToString("O"));
                for (var i = 0; i < 1000; i++)
                {
                    var accountByExprBatchCompiled = context.Accounts
                        .AsNoTracking()
                        ////.AsExpandable()
                        .OrderBy(x => x.ID)
                        .Select(selectorFullCompiled)
                        .FirstOrDefault();
                    var accountByExprBatchCompiledModel = accountByExprBatchCompiled.CreateAccountModelFromEntityFull();
                }
                TestOutputHelper.WriteLine((DateTime.Now - startTimeByExprCompiled2).ToString("G"));
            }
            // ==============================================================================================
            {
                TestOutputHelper.WriteLine("By Expression-Direct Batch");
                var startTimeByExprDirect2 = DateTime.Now;
                TestOutputHelper.WriteLine(startTimeByExprDirect2.ToString("O"));
                for (var i = 0; i < 1000; i++)
                {
                    var accountByExprBatchDirect = context.Accounts
                        .AsNoTracking()
                        .AsExpandable()
                        .OrderBy(x => x.ID)
                        .Select(selectorFull)
                        .FirstOrDefault();
                    var accountByExprBatchDirectModel = accountByExprBatchDirect.CreateAccountModelFromEntityFull();
                }
                TestOutputHelper.WriteLine((DateTime.Now - startTimeByExprDirect2).ToString("G"));
            }
            // ==============================================================================================
            {
                TestOutputHelper.WriteLine("By Expression-Direct List");
                var startTimeByExprDirect3 = DateTime.Now;
                TestOutputHelper.WriteLine(startTimeByExprDirect3.ToString("O"));
                for (var i = 0; i < 1000; i++)
                {
                    var accountByExprListDirect = context.Accounts
                        .AsNoTracking()
                        .AsExpandable()
                        .OrderBy(x => x.ID)
                        .Select(selectorList)
                        .ToList()
                        .Select(ModelMapperForAccount.CreateAccountModelFromEntityList)
                        .ToList();
                }
                TestOutputHelper.WriteLine((DateTime.Now - startTimeByExprDirect3).ToString("G"));
            }
#endif
            using var context = RegistryLoaderWrapper.GetContext(null);
            var warmUp = context.Accounts
                .AsNoTracking()
                ////.AsExpandable()
                .OrderBy(x => x.ID)
                .Skip(0)
                .Take(1)
                .Select(selectorFullCompiled)
                .FirstOrDefault();
            // ==============================================================================================
            TestOutputHelper.WriteLine("// ==============================================================================================");
            // ==============================================================================================
            //*
            {
                TestOutputHelper.WriteLine("By Func Single");
                var startTimeByFunc = DateTime.Now;
                TestOutputHelper.WriteLine(startTimeByFunc.ToString("O"));
                var accountByFuncSingle = await context.Accounts
                    .AsNoTracking()
                    ////.AsExpandable()
                    .OrderBy(x => x.ID)
                    .Skip(0)
                    .Take(1)
                    .Select(x => ModelMapperForAccount.CreateAccountModelFromEntityFull(x, null))
                    .FirstOrDefaultAsync();
                TestOutputHelper.WriteLine(JsonConvert.SerializeObject(accountByFuncSingle, jsonSettings));
                TestOutputHelper.WriteLine((DateTime.Now - startTimeByFunc).ToString("G"));
            }
            // ==============================================================================================
            {
                TestOutputHelper.WriteLine("By Expression-Compiled Single");
                var startTimeByExprCompiled = DateTime.Now;
                TestOutputHelper.WriteLine(startTimeByExprCompiled.ToString("O"));
                var accountByExprSingleCompiled = context.Accounts
                    .AsNoTracking()
                    ////.AsExpandable()
                    .OrderBy(x => x.ID)
                    .Skip(0)
                    .Take(1)
                    .Select(selectorFullCompiled)
                    .FirstOrDefault();
                var accountByExprSingleCompiledModel = accountByExprSingleCompiled.CreateAccountModelFromEntityFull(null);
                TestOutputHelper.WriteLine(JsonConvert.SerializeObject(accountByExprSingleCompiledModel, jsonSettings));
                TestOutputHelper.WriteLine((DateTime.Now - startTimeByExprCompiled).ToString("G"));
            }
#if NEW_MAPPING
            // ==============================================================================================
            {
                TestOutputHelper.WriteLine("By NewMapper Single");
                var startTimeByNewMapperFull = DateTime.Now;
                TestOutputHelper.WriteLine(startTimeByNewMapperFull.ToString("O"));
                var accountNewMapperFullModel = await context.Accounts
                    .AsNoTracking()
                    ////.AsExpandable()
                    .OrderBy(x => x.ID)
                    .Skip(0)
                    .Take(1)
                    .MapSingleEntityToModelAsFullModeAsync(context)
                    .ConfigureAwait(false);
                TestOutputHelper.WriteLine(JsonConvert.SerializeObject(accountNewMapperFullModel, jsonSettings));
                TestOutputHelper.WriteLine((DateTime.Now - startTimeByNewMapperFull).ToString("G"));
            }
#endif
            // ==============================================================================================
            TestOutputHelper.WriteLine("// ==============================================================================================");
            // ==============================================================================================
            {
                TestOutputHelper.WriteLine("By Func Batch Full");
                var startTimeByFunc2 = DateTime.Now;
                TestOutputHelper.WriteLine(startTimeByFunc2.ToString("O"));
                for (var i = 0; i < 1000; i++)
                {
                    var accountByFuncBatch = await context.Accounts
                        .AsNoTracking()
                        ////.AsExpandable()
                        .OrderBy(x => x.ID)
                        .Take(50)
                        .Select(x => ModelMapperForAccount.CreateAccountModelFromEntityFull(x, null))
                        .FirstOrDefaultAsync();
                }
                TestOutputHelper.WriteLine((DateTime.Now - startTimeByFunc2).ToString("G"));
            }
            // ==============================================================================================
            TestOutputHelper.WriteLine("By Expression-Compiled Batch Full");
            TestOutputHelper.WriteLine("Normally x21 slower, skipping");
            /*
            // Skipping because it takes too long, ran for over an hour at medium scale
            {
                TestOutputHelper.WriteLine("By Expression-Compiled Batch Full");
                var startTimeByExprCompiled3 = DateTime.Now;
                TestOutputHelper.WriteLine(startTimeByExprCompiled3.ToString("O"));
                for (var i = 0; i < 1000; i++)
                {
                    var accountByExprListCompiled = context.Accounts
                        .AsNoTracking()
                        ////.AsExpandable()
                        .OrderBy(x => x.ID)
                        .Take(50)
                        .Select(selectorListCompiled)
                        .ToList()
                        .Select(ModelMapperForAccount.CreateAccountModelFromEntityFull)
                        .ToList();
                }
                TestOutputHelper.WriteLine((DateTime.Now - startTimeByExprCompiled3).ToString("G"));
            }
            */
            // ==============================================================================================
            TestOutputHelper.WriteLine("By NewMapper Batch Full");
            TestOutputHelper.WriteLine("Normally x24 slower, skipping");
            /*
            {
                TestOutputHelper.WriteLine("By NewMapper Batch Full");
                var startTimeByNewMapperBatchFull = DateTime.Now;
                TestOutputHelper.WriteLine(startTimeByNewMapperBatchFull.ToString("O"));
                for (var i = 0; i < 1000; i++)
                {
                    var (results, totalPages, totalCount) = await context.Accounts
                        .AsNoTracking()
                        ////.AsExpandable()
                        .OrderBy(x => x.ID)
                        .Take(50)
                        .SelectFullNewAccountAndMapToAccountModelAsync(
                            paging: null,
                            sorts: null,
                            groupings: null,
                            context: context,
                            cache: null)
                        .ConfigureAwait(false);
                }
                TestOutputHelper.WriteLine((DateTime.Now - startTimeByNewMapperBatchFull).ToString("G"));
            }
            */
            // ==============================================================================================
            TestOutputHelper.WriteLine("// ==============================================================================================");
            // ==============================================================================================
            {
                TestOutputHelper.WriteLine("By Func Batch List");
                var startTimeByFunc3 = DateTime.Now;
                TestOutputHelper.WriteLine(startTimeByFunc3.ToString("O"));
                for (var i = 0; i < 1000; i++)
                {
                    var accountsByFuncList = await context.Accounts
                        .AsNoTracking()
                        ////.AsExpandable()
                        .OrderBy(x => x.ID)
                        .Take(50)
                        .Select(x => ModelMapperForAccount.CreateAccountModelFromEntityList(x, null))
                        .ToListAsync();
                }
                TestOutputHelper.WriteLine((DateTime.Now - startTimeByFunc3).ToString("G"));
            }
            // ==============================================================================================
            TestOutputHelper.WriteLine("By Expression-Compiled Batch List");
            TestOutputHelper.WriteLine("Normally x20 slower, skipping");
            /*
            {
                TestOutputHelper.WriteLine("By Expression-Compiled Batch List");
                var startTimeByExprCompiled3 = DateTime.Now;
                TestOutputHelper.WriteLine(startTimeByExprCompiled3.ToString("O"));
                for (var i = 0; i < 1000; i++)
                {
                    var accountsByExprListCompiled = context.Accounts
                        .AsNoTracking()
                        ////.AsExpandable()
                        .OrderBy(x => x.ID)
                        .Take(50)
                        .Select(selectorListCompiled)
                        .ToList()
                        .Select(ModelMapperForAccount.CreateAccountModelFromEntityList)
                        .ToList();
                }
                TestOutputHelper.WriteLine((DateTime.Now - startTimeByExprCompiled3).ToString("G"));
            }
            */
#if NEW_MAPPING
            // ==============================================================================================
            {
                TestOutputHelper.WriteLine("By NewMapper Batch List");
                var startTimeByFunc2 = DateTime.Now;
                TestOutputHelper.WriteLine(startTimeByFunc2.ToString("O"));
                for (var i = 0; i < 1000; i++)
                {
                    var (results, totalPages, totalCount) = await context.Accounts
                        .AsNoTracking()
                        ////.AsExpandable()
                        .OrderBy(x => x.ID)
                        .Take(50)
                        .SelectListNewAccountAndMapToAccountModelAsync(
                            paging: null,
                            sorts: null,
                            groupings: null,
                            context: context,
                            cache: null)
                        .ConfigureAwait(false);
                }
                TestOutputHelper.WriteLine((DateTime.Now - startTimeByFunc2).ToString("G"));
            }
#endif
            // ==============================================================================================
            TestOutputHelper.WriteLine("// ==============================================================================================");
            // ==============================================================================================
            // return Task.CompletedTask;
        }

        [Fact(Skip = "Don't run automatically")]
        public void Validate_ExpressionMapCall()
        {
            using var context = RegistryLoaderWrapper.GetContext(null);
            var result = ModelMapperForAccount.SelectFullAccountAndMapToAccountModel(
                context.Accounts.AsNoTracking().FilterByActive(true),
                contextProfileName: null);
            ////.SelectListAccountAndMapToAccountModel();
            ////.Select(BaseModelMapper2.PreBuiltAccountSQLSelectorList.Value)
            ////.ToList();
            foreach (var model in result)
            {
                TestOutputHelper.WriteLine(model?.ToString());
                break;
            }
        }

        [Fact(Skip = "Don't run automatically")]
        public void ClearBadAttributesFromCategories()
        {
            using var context = new ClarityEcommerceEntities();
            foreach (var cat in context.Categories)
            {
                var attrs = cat.JsonAttributes.DeserializeAttributesDictionary();
                foreach (var key in attrs.Keys.ToArray())
                {
                    if (Contract.CheckAllInvalidKeys(attrs[key].Value))
                    {
                        attrs.TryRemove(key, out _);
                    }
                }
                cat.JsonAttributes = attrs.SerializeAttributesDictionary();
            }
            context.SaveUnitOfWork();
        }
    }
}
