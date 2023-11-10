// <copyright file="ImporterProviderBase.V2.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the importer provider base class</summary>
// ReSharper disable BadSwitchBracesIndent, StringLiteralTypo
// #define UAG
// ReSharper disable MultipleStatementsOnOneLine
namespace Clarity.Ecommerce.Providers.Importer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Models.Import;
    using Inventory;
    using Models;
    using Models.Import;
    using Utilities;

    public abstract partial class ImporterProviderBase
    {
        /// <summary>Information describing the enum any with predicate method.</summary>
        private static readonly MethodInfo EnumAnyWithPredicateMethodInfo = typeof(Enumerable).GetMethods()
            .Single(x => x.Name == "Any" && x.GetParameters().Length == 2);

        /// <summary>List of names of the properties.</summary>
        private List<string> propertyNames = null!;

        /// <summary>Gets or sets the identifier of the package type.</summary>
        /// <value>The identifier of the package type.</value>
        private static int PackageTypeID { get; set; }

        /// <summary>Gets or sets the identifier of the master pack type.</summary>
        /// <value>The identifier of the master pack type.</value>
        private static int MasterPackTypeID { get; set; }

        /// <summary>Gets or sets the identifier of the pallet type.</summary>
        /// <value>The identifier of the pallet type.</value>
        private static int PalletTypeID { get; set; }

        /// <summary>Gets or sets the identifier of the general type.</summary>
        /// <value>The identifier of the generic type.</value>
        private static int GeneralTypeID { get; set; }

        /// <summary>Gets or sets the identifier of the normal status.</summary>
        /// <value>The identifier of the normal status.</value>
        private static int NormalStatusID { get; set; }

        /// <inheritdoc/>
        public Task<List<string>> GetParsingErrorAsync()
        {
            return Task.FromResult(ParsingErrorList);
        }

        /// <inheritdoc/>
        public virtual Task<bool> LoadAsync(ISpreadsheetImportModel spsModel)
        {
            return Task.FromResult(false);
        }

        /// <inheritdoc/>
        public virtual Task<IEnumerable<string>?> GetHeadersAsync()
        {
            return Task.FromResult<IEnumerable<string>?>(null);
        }

        /// <inheritdoc/>
        public virtual Task<IEnumerable<IImportItem>?> ParseAsync(string? contextProfileName)
        {
            return Task.FromResult<IEnumerable<IImportItem>?>(null);
        }

        /// <inheritdoc/>
        public virtual async Task<List<(IProductModel? model, IRawProductPricesModel? pricing, IUpdateInventoryForProduct? inventory)>> ResolveAsync(
            IEnumerable<IImportItem> items,
            Func<string, Task<IProductModel?>> getModelByKey,
            string? contextProfileName)
        {
            var models = new List<(IProductModel?, IRawProductPricesModel?, IUpdateInventoryForProduct?)>();
            propertyNames = typeof(ProductModel)
                .GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance)
                .Select(p => p.Name)
                .ToList();
            foreach (var item in items)
            {
                // ReSharper disable once StringLiteralTypo
                var customKey = !item.Fields.Any(x => string.Equals(x.Name, "itemnumber", StringComparison.OrdinalIgnoreCase))
                    ? item.Fields!.FirstOrDefault(x => string.Equals(x.Name, "customkey", StringComparison.OrdinalIgnoreCase))?.Value
                    : item.Fields!.FirstOrDefault(x => string.Equals(x.Name, "itemnumber", StringComparison.OrdinalIgnoreCase))?.Value;
                var model = RegistryLoaderWrapper.GetInstance<IProductModel>(contextProfileName);
                // Preset active to true, in case it's not specified on the sheet
                // But do so before applying fields so it gets overwritten if it is on the sheet
                // Also apply before reading the product out of the database (if it exists)
                // so we don't unintentionally modify the active state on existing products.
                model.Active = true;
                if (!string.IsNullOrEmpty(customKey))
                {
                    var product = await getModelByKey(customKey!).ConfigureAwait(false);
                    if (product != null)
                    {
                        model = product;
                    }
                }
                var (ensurePricingGetsSent, rawPricingModel, ensureInventoryGetsSent, rawInventoryModel)
                    = await ResolveItemAsync(item, model, contextProfileName).ConfigureAwait(false);
                models.Add((
                    model,
                    ensurePricingGetsSent ? rawPricingModel : null,
                    ensureInventoryGetsSent ? rawInventoryModel : null));
            }
            return models;
        }

        /// <summary>Adds a coma separated items.</summary>
        /// <param name="columnName"> Name of the column.</param>
        /// <param name="value">      The value.</param>
        /// <param name="currentItem">The current item.</param>
        protected static void AddComaSeparatedItems(string columnName, string value, ImportItem currentItem)
        {
            foreach (var cat in value.Split(','))
            {
                currentItem.Fields!.Add(new ImportField { Name = columnName, Value = cat });
            }
        }

        /// <summary>Downloads the images.</summary>
        /// <param name="value">             The value.</param>
        /// <param name="currentItem">       The current item.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected static async Task DownloadImagesAsync(string value, ImportItem currentItem, string? contextProfileName)
        {
            // setup images path
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            var provider = RegistryLoaderWrapper.GetFilesProvider(contextProfileName);
            if (provider is null)
            {
                return;
            }
            var path = await provider.GetFileSaveRootPathFromFileEntityTypeAsync(
                    Enums.FileEntityType.ImageProduct)
                .ConfigureAwait(false);
            if (!string.IsNullOrEmpty(path))
            {
                var cdnDirectory = new DirectoryInfo(path);
                if (!cdnDirectory.Exists)
                {
                    cdnDirectory.Create();
                }
            }
            var urls = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            using var webClient = new WebClient();
            foreach (var imageUrl in urls)
            {
                var filename = Path.GetFileName(imageUrl);
                var destination = path + "\\" + filename;
                try
                {
                    var file = new FileInfo(destination);
                    if (!file.Exists)
                    {
                        await webClient.DownloadFileTaskAsync(new Uri(imageUrl), destination).ConfigureAwait(false);
                    }
                    currentItem.Fields!.Add(new ImportField { Name = "Image", Value = filename });
                }
                catch (Exception e)
                {
                    await Logger.LogErrorAsync("ProductImport.Error", e.Message, e, contextProfileName).ConfigureAwait(false);
                }
            }
        }

        /// <summary>Gets inner name.</summary>
        /// <param name="innerFieldName">Name of the inner field.</param>
        /// <returns>The inner name.</returns>
        // ReSharper disable once CyclomaticComplexity
        private static string? GetPackageInnerName(string innerFieldName)
        {
            var innerFieldNameToUse = innerFieldName;
            if (innerFieldNameToUse.StartsWith("."))
            {
                innerFieldNameToUse = innerFieldNameToUse[1..];
            }
            return innerFieldNameToUse switch
            {
                "name" => nameof(Package.Name),
                "description" => nameof(Package.Description),
                "customkey" => nameof(Package.CustomKey),
                "key" => nameof(Package.CustomKey),
                "iscustom" => nameof(Package.IsCustom),
                "width" => nameof(Package.Width),
                "widthuofm" => nameof(Package.WidthUnitOfMeasure),
                "widthunitofmeasure" => nameof(Package.WidthUnitOfMeasure),
                "depth" => nameof(Package.Depth),
                "depthuofm" => nameof(Package.DepthUnitOfMeasure),
                "depthunitofmeasure" => nameof(Package.DepthUnitOfMeasure),
                "height" => nameof(Package.Height),
                "heightuofm" => nameof(Package.HeightUnitOfMeasure),
                "heightunitofmeasure" => nameof(Package.HeightUnitOfMeasure),
                "weight" => nameof(Package.Weight),
                "weightuofm" => nameof(Package.WeightUnitOfMeasure),
                "weightunitofmeasure" => nameof(Package.WeightUnitOfMeasure),
                "dimensionalweight" => nameof(Package.DimensionalWeight),
                "dimensionalweightuofm" => nameof(Package.DimensionalWeightUnitOfMeasure),
                "dimensionalweightunitofmeasure" => nameof(Package.DimensionalWeightUnitOfMeasure),
                _ => null,
            };
        }

        /// <summary>Gets inner name.</summary>
        /// <param name="innerFieldName">Name of the inner field.</param>
        /// <returns>The inner name.</returns>
        private static string? GetRawPricingInnerName(string innerFieldName)
        {
            var innerFieldNameToUse = innerFieldName;
            if (innerFieldNameToUse.StartsWith("."))
            {
                innerFieldNameToUse = innerFieldNameToUse[1..];
            }
            return innerFieldNameToUse switch
            {
                "pricebase" => nameof(IRawProductPricesModel.PriceBase),
                "pricesale" => nameof(IRawProductPricesModel.PriceSale),
                "pricemsrp" => nameof(IRawProductPricesModel.PriceMsrp),
                "pricereduction" => nameof(IRawProductPricesModel.PriceReduction),
                _ => null,
            };
        }

        /// <summary>Gets inner name.</summary>
        /// <param name="innerFieldName">Name of the inner field.</param>
        /// <returns>The inner name.</returns>
        private static string? GetRawInventoryInnerName(string innerFieldName)
        {
            var innerFieldNameToUse = innerFieldName;
            if (innerFieldNameToUse.StartsWith("."))
            {
                innerFieldNameToUse = innerFieldNameToUse[1..];
            }
            return innerFieldNameToUse switch
            {
                "quantity" => nameof(UpdateInventoryForProduct.Quantity),
                "quantityallocated" => nameof(UpdateInventoryForProduct.QuantityAllocated),
                "quantitypresold" => nameof(UpdateInventoryForProduct.QuantityPreSold),
                "relevantlocationid" => nameof(UpdateInventoryForProduct.RelevantLocationID),
                "relevanthash" => nameof(UpdateInventoryForProduct.RelevantHash),
                _ => null,
            };
        }

        /// <summary>Sets associated object.</summary>
        /// <typeparam name="TModel">      Type of the model.</typeparam>
        /// <typeparam name="TAssocModel"> Type of the associated model.</typeparam>
        /// <typeparam name="TTargetModel">Type of the target model.</typeparam>
        /// <param name="value">         The value.</param>
        /// <param name="assocPropName"> Name of the associated property.</param>
        /// <param name="targetPropName">Name of the target property.</param>
        /// <param name="model">         The model.</param>
        /// <param name="existsCheck">   The exists check.</param>
        // assocPropName - VendorProducts
        // targetPropName - Vendor
        private static void SetAssociatedObject<TModel, TAssocModel, TTargetModel>(
            string? value,
            string assocPropName,
            string targetPropName,
            TModel model,
            Func<TAssocModel, bool> existsCheck)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            var prop = model!.GetType()
                .GetProperty(
                    assocPropName,
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
            if (prop?.CanWrite != true)
            {
                return;
            }
            var type = prop.PropertyType;
            if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(List<>))
            {
                return;
            }
            var itemType = type.GetGenericArguments()[0];
            // If list is null -- create new list
            var isNew = prop.GetValue(model) == null;
            if (isNew)
            {
                var listType = typeof(List<>);
                var constructedListType = listType.MakeGenericType(itemType);
                var instance = Activator.CreateInstance(constructedListType);
                prop.SetValue(model, instance);
            }
            // Get the list
            var list = prop.GetValue(model);
            // Cast the list to List<itemType>
            // ReSharper disable once PossibleNullReferenceException
            var newList = typeof(Enumerable)
                .GetMethod(nameof(Enumerable.Cast))!
                .MakeGenericMethod(itemType)
                .Invoke(null, new[] { list });
            if (newList == null)
            {
                return;
            }
            // Here -- we have list that is a list of TAssocModel -- new to add the TTargetModel to the object
            // Create obj
            var splitValue = value!.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Distinct();
            foreach (var subValue in splitValue)
            {
                // Check if it's already there
                var exists = (bool)EnumAnyWithPredicateMethodInfo.MakeGenericMethod(typeof(TAssocModel))!
                    .Invoke(null, new[] { newList, existsCheck })!;
                if (exists)
                {
                    continue;
                }
                // It's not, add it
                var assocObj = Activator.CreateInstance(typeof(TAssocModel))!;
                var targetModel = Activator.CreateInstance(typeof(TTargetModel))!;
                // SET TARGET ACTIVE AND NAME
                var activeProp = targetModel.GetType()
                    .GetProperty(
                        nameof(IBase.Active),
                        BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
                if (activeProp?.CanWrite != true)
                {
                    continue;
                }
                var nameProp = targetModel.GetType()
                    .GetProperty(
                        nameof(INameableBase.Name),
                        BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
                if (nameProp?.CanWrite != true)
                {
                    continue;
                }
                activeProp.SetValue(targetModel, true, null);
                nameProp.SetValue(targetModel, subValue, null);
                // SET ASSOC TARGET AND ACTIVE
                var activeAssocProp = assocObj.GetType()
                    .GetProperty(
                        nameof(IBase.Active),
                        BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
                var assocTargetProp = assocObj.GetType()
                    .GetProperty(
                        targetPropName,
                        BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
                if (activeAssocProp?.CanWrite != true)
                {
                    continue;
                }
                if (assocTargetProp?.CanWrite != true)
                {
                    continue;
                }
                activeAssocProp.SetValue(assocObj, true, null);
                assocTargetProp.SetValue(assocObj, targetModel, null);
                // ADD ASSOC TO LIST
                // ReSharper disable once PossibleNullReferenceException
                newList.GetType().GetMethod("Add")!.Invoke(newList, new[] { assocObj });
            }
        }

        /// <summary>Sets category to entity.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="model">The model.</param>
        // ReSharper disable once CognitiveComplexity
        private static void SetCategoryToEntity<TModel>(string? value, TModel model)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            var prop = model!.GetType()
                .GetProperty(
                    nameof(Product.Categories),
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
            if (prop?.CanWrite != true)
            {
                return;
            }
            var type = prop.PropertyType;
            if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(List<>))
            {
                return;
            }
            var itemType = type.GetGenericArguments()[0];
            // If list is null -- create new list
            if (prop.GetValue(model) == null)
            {
                var listType = typeof(List<>);
                var constructedListType = listType.MakeGenericType(itemType);
                var instance = Activator.CreateInstance(constructedListType);
                prop.SetValue(model, instance);
            }
            // Get the list
            if (prop.GetValue(model) is not List<ProductCategoryModel> list)
            {
                return;
            }
            // Here -- we have list that is a list of ProductCategoryModel -- new to add the CategoryModel to the object
            // Create obj
            var topLevelCategory = new ProductCategoryModel
            {
                Slave = new()
                {
                    CustomKey = value!.Trim().Replace(' ', '-'),
                    Name = value.Trim(),
                    Active = true,
                    IncludeInMenu = true,
                    IsVisible = true,
                },
                Active = true,
            };
            // Manage Children
            var childrenList = new List<ProductCategoryModel>();
            if (value.Contains(','))
            {
                var cats = value.Split(',');
                if (cats.Length >= 8)
                {
                    throw new ArgumentException("Importer cannot support more than 7 levels of category import.");
                }
                topLevelCategory.Slave.Name = cats[0].Trim();
                topLevelCategory.Slave.CustomKey = cats[0].Trim().Replace(' ', '-');
                topLevelCategory.Slave.Children = new();
                if (cats.Length >= 2)
                {
                    var secondLevelCategory = new CategoryModel
                    {
                        CustomKey = cats[0].Trim() + '_' + cats[1].Trim(),
                        Name = cats[1].Trim(),
                        Active = true,
                        ParentKey = topLevelCategory.Slave.CustomKey,
                        IncludeInMenu = true,
                        IsVisible = true,
                    };
                    topLevelCategory.Slave.Children.Add(secondLevelCategory);
                    var childAlreadyAssociated = list.Any(c => c.Active && c.SlaveKey == secondLevelCategory.CustomKey);
                    if (!childAlreadyAssociated)
                    {
                        childrenList.Add(new() { Slave = secondLevelCategory, Active = true });
                    }
                    if (cats.Length >= 3)
                    {
                        secondLevelCategory.Children = new();
                        var thirdLevelCategory = new CategoryModel
                        {
                            CustomKey = cats[0].Trim() + '_' + cats[1].Trim() + '_' + cats[2].Trim(),
                            Name = cats[2].Trim(),
                            Active = true,
                            ParentKey = secondLevelCategory.CustomKey,
                            IncludeInMenu = true,
                            IsVisible = true,
                        };
                        secondLevelCategory.Children.Add(thirdLevelCategory);
                        childAlreadyAssociated = list.Any(c => c.Active && c.SlaveKey == thirdLevelCategory.CustomKey);
                        if (!childAlreadyAssociated)
                        {
                            childrenList.Add(new() { Slave = thirdLevelCategory, Active = true });
                        }
                        if (cats.Length >= 4)
                        {
                            thirdLevelCategory.Children = new();
                            var fourthLevelCategory = new CategoryModel
                            {
                                CustomKey = cats[0].Trim() + '_' + cats[1].Trim() + '_' + cats[2].Trim() + '_' + cats[3].Trim(),
                                Name = cats[3].Trim(),
                                Active = true,
                                ParentKey = thirdLevelCategory.CustomKey,
                                IncludeInMenu = true,
                                IsVisible = true,
                            };
                            thirdLevelCategory.Children.Add(fourthLevelCategory);
                            childAlreadyAssociated = list.Any(c => c.Active && c.SlaveKey == fourthLevelCategory.CustomKey);
                            if (!childAlreadyAssociated)
                            {
                                childrenList.Add(new() { Slave = fourthLevelCategory, Active = true });
                            }
                            if (cats.Length >= 5)
                            {
                                fourthLevelCategory.Children = new();
                                var fifthLevelCategory = new CategoryModel
                                {
                                    CustomKey = cats[0].Trim() + '_' + cats[1].Trim() + '_' + cats[2].Trim() + '_' + cats[3].Trim() + '_' + cats[4].Trim(),
                                    Name = cats[4].Trim(),
                                    Active = true,
                                    ParentKey = fourthLevelCategory.CustomKey,
                                    IncludeInMenu = true,
                                    IsVisible = true,
                                };
                                fourthLevelCategory.Children.Add(fifthLevelCategory);
                                childAlreadyAssociated = list.Any(c => c.Active && c.SlaveKey == fifthLevelCategory.CustomKey);
                                if (!childAlreadyAssociated)
                                {
                                    childrenList.Add(new() { Slave = fifthLevelCategory, Active = true });
                                }
                                if (cats.Length >= 6)
                                {
                                    fifthLevelCategory.Children = new();
                                    var sixthLevelCategory = new CategoryModel
                                    {
                                        CustomKey = cats[0].Trim() + '_' + cats[1].Trim() + '_' + cats[2].Trim() + '_' + cats[3].Trim() + '_' + cats[4].Trim() + '_' + cats[5].Trim(),
                                        Name = cats[5].Trim(),
                                        Active = true,
                                        ParentKey = fifthLevelCategory.CustomKey,
                                        IncludeInMenu = true,
                                        IsVisible = true,
                                    };
                                    fifthLevelCategory.Children.Add(sixthLevelCategory);
                                    childAlreadyAssociated = list.Any(c => c.Active && c.SlaveKey == sixthLevelCategory.CustomKey);
                                    if (!childAlreadyAssociated)
                                    {
                                        childrenList.Add(new() { Slave = sixthLevelCategory, Active = true });
                                    }
                                    if (cats.Length >= 7)
                                    {
                                        sixthLevelCategory.Children = new();
                                        var seventhLevelCategory = new CategoryModel
                                        {
                                            CustomKey = cats[0].Trim() + '_' + cats[1].Trim() + '_' + cats[2].Trim() + '_' + cats[3].Trim() + '_' + cats[4].Trim() + '_' + cats[5].Trim() + '_' + cats[6].Trim(),
                                            Name = cats[6].Trim(),
                                            Active = true,
                                            ParentKey = sixthLevelCategory.CustomKey,
                                            IncludeInMenu = true,
                                            IsVisible = true,
                                        };
                                        sixthLevelCategory.Children.Add(seventhLevelCategory);
                                        childAlreadyAssociated = list.Any(c => c.Active && c.SlaveKey == seventhLevelCategory.CustomKey);
                                        if (!childAlreadyAssociated)
                                        {
                                            childrenList.Add(new() { Slave = seventhLevelCategory, Active = true });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            // Add object to the list
            var parentAlreadyAssociated = list.Any(c => c.Active && c.SlaveKey == topLevelCategory.Slave.CustomKey);
            if (!parentAlreadyAssociated)
            {
                list.Add(topLevelCategory);
            }
            list.AddRange(childrenList);
        }

        /// <summary>Creates an attribute.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="name"> The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="model">The model.</param>
        private static void CreateAttribute<TModel>(string name, string? value, TModel model)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
            {
                return;
            }
            var prop = model!.GetType()
                .GetProperty(
                    nameof(IBase.SerializableAttributes),
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
            if (prop == null)
            {
                return;
            }
            // ReSharper disable once PossibleNullReferenceException
            if (prop.GetValue(model) == null)
            {
                var instance = Activator.CreateInstance(typeof(SerializableAttributesDictionary));
                prop.SetValue(model, instance);
            }
            if (prop.GetValue(model) is not SerializableAttributesDictionary serial)
            {
                return;
            }
            serial[name.Trim()] = new()
            {
                Key = name.Trim(),
                Value = value!,
            };
            serial.SerializeAttributesDictionary();
            prop.SetValue(model, serial);
        }

        /// <summary>Sets the associations.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="value">     The value.</param>
        /// <param name="model">     The model.</param>
        /// <param name="relateKind">The relate kind.</param>
        private static void SetAssociations<TModel>(string? value, TModel model, string relateKind = "Related Product")
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            var prop = model!.GetType()
                .GetProperty(
                    nameof(Product.ProductAssociations),
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
            if (prop == null)
            {
                return;
            }
            if (prop.GetValue(model) == null)
            {
                prop.SetValue(model, new List<ProductAssociationModel>());
            }
            var productKeys = value!.Split(',').Select(c => c.Trim()).ToArray();
            if (prop.GetValue(model) is not List<ProductAssociationModel> list)
            {
                return;
            }
            foreach (var productKey in productKeys)
            {
                var alreadyAssociated = list.Any(x => x.Active && x.SlaveKey == productKey && x.TypeName == relateKind);
                if (alreadyAssociated)
                {
                    continue;
                }
                var assocProduct = new ProductAssociationModel
                {
                    Active = true,
                    Type = new() { Name = relateKind },
                    TypeName = relateKind,
                    SlaveKey = productKey,
                };
                list.Add(assocProduct);
            }
            prop.SetValue(model, list);
        }

        /// <summary>Sets package property.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="model">The model.</param>
        private static void SetPackageProperty<TModel>(string field, string? value, TModel model)
        {
            SetPackagePropertyInner(field, value, model, nameof(Product.Package));
        }

        /// <summary>Sets master pack property.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="model">The model.</param>
        private static void SetMasterPackProperty<TModel>(string field, string? value, TModel model)
        {
            SetPackagePropertyInner(field, value, model, nameof(Product.MasterPack));
        }

        /// <summary>Sets pallet property.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="model">The model.</param>
        private static void SetPalletProperty<TModel>(string field, string? value, TModel model)
        {
            SetPackagePropertyInner(field, value, model, nameof(Product.Pallet));
        }

        /// <summary>Sets package property inner.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="model">The model.</param>
        /// <param name="name"> The name.</param>
        private static void SetPackagePropertyInner<TModel>(string field, string? value, TModel model, string name)
        {
            var propertyOfMainModel = model!.GetType()
                .GetProperty(
                    name,
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
            if (propertyOfMainModel == null)
            {
                return;
            }
            var instanceOfPackageModel = propertyOfMainModel.GetValue(model);
            if (instanceOfPackageModel == null)
            {
                return;
            }
            var propertyOfPackageModel = instanceOfPackageModel.GetType()
                .GetProperty(
                    field,
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
            if (propertyOfPackageModel == null)
            {
                return;
            }
            SetValueByParsingValueType(propertyOfPackageModel, instanceOfPackageModel, value);
        }

        /// <summary>Sets raw pricing property.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="model">The model.</param>
        private static void SetRawPricingProperty<TModel>(string field, string? value, TModel model)
        {
            var propertyToSet = model!.GetType()
                .GetProperty(
                    field,
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
            if (propertyToSet == null)
            {
                return;
            }
            SetValueByParsingValueType(propertyToSet, model, value);
        }

        /// <summary>Sets raw inventory property.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="model">The model.</param>
        private static void SetRawInventoryProperty<TModel>(string field, string? value, TModel model)
        {
            var prop = model!.GetType()
                .GetProperty(
                    field,
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
            if (prop == null)
            {
                return;
            }
            SetValueByParsingValueType(prop, model, value);
        }

        /// <summary>Sets a type.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="model">The model.</param>
        private static void SetType<TModel>(string? value, TModel model)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            var propertyOfMainModel = model!.GetType()
                .GetProperty(
                    nameof(Product.Type),
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
            // Create TypeModel object
            if (propertyOfMainModel == null)
            {
                return;
            }
            if (propertyOfMainModel.GetValue(model) != null)
            {
                var propID = model.GetType().GetProperty(nameof(IProductModel.TypeID))!;
                // ReSharper disable once PossibleNullReferenceException
                propID.SetValue(model, 0);
                var propKey = model.GetType().GetProperty(nameof(IProductModel.TypeKey))!;
                // ReSharper disable once PossibleNullReferenceException
                propKey.SetValue(model, null);
            }
            var instanceOfTypeModel = Activator.CreateInstance(propertyOfMainModel.PropertyType)!;
            var namePropertyOfTypeModel = instanceOfTypeModel.GetType()
                .GetProperty(
                    nameof(INameableBase.Name),
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
            var customKeyPropertyOfTypeModel = instanceOfTypeModel.GetType()
                .GetProperty(
                    nameof(IBase.CustomKey),
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
            var activePropertyOfTypeModel = instanceOfTypeModel.GetType()
                .GetProperty(
                    nameof(IBase.Active),
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
            var createdDatePropertyOfTypeModel = instanceOfTypeModel.GetType()
                .GetProperty(
                    nameof(IBase.CreatedDate),
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
            namePropertyOfTypeModel?.SetValue(instanceOfTypeModel, value);
            customKeyPropertyOfTypeModel?.SetValue(instanceOfTypeModel, value);
            activePropertyOfTypeModel?.SetValue(instanceOfTypeModel, true);
            createdDatePropertyOfTypeModel?.SetValue(instanceOfTypeModel, DateExtensions.GenDateTime);
            propertyOfMainModel.SetValue(model, instanceOfTypeModel);
        }

        /// <summary>Sets a status.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="model">The model.</param>
        private static void SetStatus<TModel>(string? value, TModel model)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            var propertyOfMainModel = model!.GetType()
                .GetProperty(
                    nameof(Product.Status),
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
            // Create TypeModel object
            if (propertyOfMainModel == null)
            {
                return;
            }
            if (propertyOfMainModel.GetValue(model) != null)
            {
                return;
            }
            var instanceOfStatusModel = Activator.CreateInstance(propertyOfMainModel.PropertyType)!;
            var namePropertyOfStatusModel = instanceOfStatusModel.GetType()
                .GetProperty(
                    nameof(INameableBase.Name),
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
            var customKeyPropertyOfStatusModel = instanceOfStatusModel.GetType()
                .GetProperty(
                    nameof(IBase.CustomKey),
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
            var activePropertyOfStatusModel = instanceOfStatusModel.GetType()
                .GetProperty(
                    nameof(IBase.Active),
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
            var createdDatePropertyOfStatusModel = instanceOfStatusModel.GetType()
                .GetProperty(
                    nameof(IBase.CreatedDate),
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
            namePropertyOfStatusModel?.SetValue(instanceOfStatusModel, value);
            customKeyPropertyOfStatusModel?.SetValue(instanceOfStatusModel, value);
            activePropertyOfStatusModel?.SetValue(instanceOfStatusModel, true);
            createdDatePropertyOfStatusModel?.SetValue(instanceOfStatusModel, DateExtensions.GenDateTime);
            propertyOfMainModel.SetValue(model, instanceOfStatusModel);
        }

        /// <summary>Sets an image.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="model">The model.</param>
        private static void SetImage<TModel>(string? value, TModel model)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            var images = value!.Split(',').Select(i => i.Trim()).ToArray(); // allowing for multiple images
            var propertyOfMainModel = model!.GetType()
                .GetProperty(
                    nameof(Product.Images),
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
            if (propertyOfMainModel == null)
            {
                return;
            }
            if (propertyOfMainModel.GetValue(model) == null)
            {
                propertyOfMainModel.SetValue(model, new List<ProductImageModel>());
            }
            var valueAsList = propertyOfMainModel.GetValue(model) as List<ProductImageModel> ?? new List<ProductImageModel>();
            var customKeyPropertyOfMainModel = model.GetType()
                .GetProperty(
                    nameof(IBase.CustomKey),
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance)!;
            // ReSharper disable once PossibleNullReferenceException
            var mainModelCustomKeyValue = customKeyPropertyOfMainModel.GetValue(model)!;
            foreach (var imageName in images)
            {
                if (valueAsList.Any(i => i.Active && i.OriginalFileName == imageName))
                {
                    continue;
                }
                valueAsList.Add(new()
                {
                    Active = true,
                    OriginalFileName = imageName,
                    Name = imageName,
                    TypeKey = "GENERAL",
                    TypeName = "General",
                    MasterKey = mainModelCustomKeyValue.ToString(),
                    IsPrimary = valueAsList.All(i => !i.IsPrimary),
                });
            }
            propertyOfMainModel.SetValue(model, valueAsList);
        }

        /// <summary>Sets a file.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="model">The model.</param>
        private static void SetFile<TModel>(string? value, TModel model)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            var propertyOfMainModel = model!.GetType()
                .GetProperty(
                    nameof(Product.StoredFiles),
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
            if (propertyOfMainModel == null)
            {
                return;
            }
            var valueAsList = propertyOfMainModel.GetValue(model) ?? new List<ProductFileModel>();
            // ReSharper disable once PossibleNullReferenceException
            var fileList = typeof(Enumerable)
                .GetMethod(nameof(Enumerable.Cast))!
                .MakeGenericMethod(typeof(ProductFileModel))
                .Invoke(null, new[] { valueAsList })!;
            var productFile = new ProductFileModel
            {
                Active = true,
                Name = value,
                Slave = new()
                {
                    Active = true,
                    Name = value,
                    FileName = value,
                },
            };
            // ReSharper disable once PossibleNullReferenceException
            fileList.GetType().GetMethod("Add")!.Invoke(fileList, new object[] { productFile });
            propertyOfMainModel.SetValue(model, fileList);
        }

        /// <summary>Sets value to property.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">       The value.</param>
        /// <param name="model">       The model.</param>
        /// <param name="altName">     Name of the alternate.</param>
        // ReSharper disable once CyclomaticComplexity
        private static void SetValueToProperty<TModel>(
            string propertyName,
            string? value,
            TModel model,
            string? altName = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            var prop = model!.GetType()
                .GetProperty(
                    propertyName,
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
            if (prop == null && Contract.CheckValidKey(altName))
            {
                // Try the alternate name
                prop = Array.Find(
                    model.GetType().GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance),
                    x => string.Equals(x.Name, altName, StringComparison.CurrentCultureIgnoreCase));
            }
            if (prop?.CanWrite != true)
            {
                return;
            }
            SetValueByParsingValueType(prop, model, value);
        }

        /// <summary>Sets value by parsing value type.</summary>
        /// <param name="propertyToSet">          Set the property to belongs to.</param>
        /// <param name="modelContainingProperty">The instance of model containing property.</param>
        /// <param name="value">                  The value.</param>
        // ReSharper disable once CognitiveComplexity, CyclomaticComplexity
        private static void SetValueByParsingValueType(
            PropertyInfo propertyToSet,
            object modelContainingProperty,
            string? value)
        {
            if (propertyToSet.PropertyType == typeof(bool) || propertyToSet.PropertyType == typeof(bool?))
            {
                var valueToUse = value;
                switch (valueToUse)
                {
                    case "N":
                    case "0":
                    {
                        valueToUse = "false";
                        break;
                    }
                    case "Y":
                    case "1":
                    {
                        valueToUse = "true";
                        break;
                    }
                }
                propertyToSet.SetValue(modelContainingProperty, bool.TryParse(valueToUse, out var test) && test);
                return;
            }
            if (propertyToSet.PropertyType == typeof(int) || propertyToSet.PropertyType == typeof(int?))
            {
                var valueToUse = value;
                if (valueToUse == "-")
                {
                    valueToUse = "0";
                }
                if (int.TryParse(valueToUse, out var test))
                {
                    propertyToSet.SetValue(modelContainingProperty, test);
                }
                return;
            }
            if (propertyToSet.PropertyType == typeof(long) || propertyToSet.PropertyType == typeof(long?))
            {
                var valueToUse = value;
                if (valueToUse == "-")
                {
                    valueToUse = "0";
                }
                if (long.TryParse(valueToUse, out var test))
                {
                    propertyToSet.SetValue(modelContainingProperty, test);
                }
                return;
            }
            if (propertyToSet.PropertyType == typeof(double) || propertyToSet.PropertyType == typeof(double?))
            {
                var valueToUse = value;
                if (valueToUse == "-")
                {
                    valueToUse = "0";
                }
                if (double.TryParse(valueToUse, out var test))
                {
                    propertyToSet.SetValue(modelContainingProperty, test);
                }
                return;
            }
            if (propertyToSet.PropertyType == typeof(decimal) || propertyToSet.PropertyType == typeof(decimal?))
            {
                var valueToUse = value;
                if (valueToUse == "-")
                {
                    valueToUse = "0";
                }
                if (decimal.TryParse(valueToUse, out var test))
                {
                    propertyToSet.SetValue(modelContainingProperty, test);
                }
                return;
            }
            if (propertyToSet.PropertyType == typeof(DateTime) || propertyToSet.PropertyType == typeof(DateTime?))
            {
                if (DateTime.TryParse(value, out var test))
                {
                    propertyToSet.SetValue(modelContainingProperty, test);
                }
                return;
            }
            if (propertyToSet.PropertyType == typeof(string))
            {
                propertyToSet.SetValue(modelContainingProperty, value);
                return;
            }
            throw new InvalidOperationException(
                $"Unable to determine type of property to parse: {propertyToSet.Name} [{propertyToSet.PropertyType}] '{value}'");
        }

        /// <summary>Ensures that package.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task EnsurePackageAsync<TModel>(TModel model, string? contextProfileName)
        {
            var propertyOfMainModel = model!.GetType()
                .GetProperty(
                    nameof(Product.Package),
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
            if (propertyOfMainModel == null)
            {
                return;
            }
            // Check for existing
            if (propertyOfMainModel.GetValue(model) != null)
            {
                return;
            }
            // Create New
            if (Contract.CheckInvalidID(PackageTypeID))
            {
                PackageTypeID = await Workflows.PackageTypes.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "Package",
                        byName: "Package",
                        byDisplayName: "Package",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            var instanceOfPackageModel = Activator.CreateInstance(propertyOfMainModel.PropertyType);
            propertyOfMainModel.SetValue(model, instanceOfPackageModel);
            SetPackageProperty("IsCustom", true.ToString(), model);
            SetPackageProperty("TypeID", PackageTypeID.ToString(), model);
        }

        /// <summary>Ensures that master pack.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task EnsureMasterPackAsync<TModel>(TModel model, string? contextProfileName)
        {
            var propertyOfMainModel = model!.GetType()
                .GetProperty(
                    nameof(Product.MasterPack),
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
            if (propertyOfMainModel == null)
            {
                return;
            }
            // Check for existing
            if (propertyOfMainModel.GetValue(model) != null)
            {
                return;
            }
            // Create New
            if (Contract.CheckInvalidID(MasterPackTypeID))
            {
                MasterPackTypeID = await Workflows.PackageTypes.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "Master Pack",
                        byName: "Master Pack",
                        byDisplayName: "Master Pack",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            var instanceOfPackageModel = Activator.CreateInstance(propertyOfMainModel.PropertyType);
            propertyOfMainModel.SetValue(model, instanceOfPackageModel);
            SetMasterPackProperty("IsCustom", true.ToString(), model);
            SetMasterPackProperty("TypeID", MasterPackTypeID.ToString(), model);
        }

        /// <summary>Ensures that pallet.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task EnsurePalletAsync<TModel>(TModel model, string? contextProfileName)
        {
            var propertyOfMainModel = model!.GetType()
                .GetProperty(
                    nameof(Product.Pallet),
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
            if (propertyOfMainModel == null)
            {
                return;
            }
            // Check for existing
            if (propertyOfMainModel.GetValue(model) != null)
            {
                return;
            }
            // Create New
            if (Contract.CheckInvalidID(PalletTypeID))
            {
                PalletTypeID = await Workflows.PackageTypes.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "Pallet",
                        byName: "Pallet",
                        byDisplayName: "Pallet",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            var instanceOfPackageModel = Activator.CreateInstance(propertyOfMainModel.PropertyType);
            propertyOfMainModel.SetValue(model, instanceOfPackageModel);
            SetPalletProperty("IsCustom", true.ToString(), model);
            SetPalletProperty("TypeID", PalletTypeID.ToString(), model);
        }
    }
}
