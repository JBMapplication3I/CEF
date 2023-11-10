// <copyright file="TypescriptCodeGenerator.DTOs.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the typescript code generator. dt operating system class</summary>
// ReSharper disable CognitiveComplexity, CyclomaticComplexity
namespace ServiceStack.CodeGenerator.TypeScript
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using Clarity.Ecommerce.Interfaces.Models;
    using Clarity.Ecommerce.Models;
    using Clarity.Ecommerce.Service;
    using global::CodeGenerator;
    using Newtonsoft.Json;

    /// <content>A typescript code generator.</content>
    public partial class TypescriptCodeGenerator
    {
        private static readonly List<Type> AlwaysSkipTypes = new()
        {
            // ReSharper disable BuiltInTypeReferenceStyle, ConvertNullableToShortForm
#pragma warning disable IDE0001,IDE0012,IDE0049,RCS1020
            typeof(ValueType),
            typeof(Nullable),
            typeof(Nullable<>),
            typeof(Object),
            typeof(String),
            typeof(String[]),
            typeof(Byte[]),
            typeof(Char),
            typeof(Nullable<Char>),
            typeof(Double),
            typeof(Nullable<Double>),
            typeof(Boolean),
            typeof(Nullable<Boolean>),
            typeof(DateTime),
            typeof(Nullable<DateTime>),
            typeof(Int32),
            typeof(Nullable<Int32>),
            typeof(Int32[]),
            typeof(Int64),
            typeof(Nullable<Int64>),
            typeof(Decimal),
            typeof(Nullable<Decimal>),
            typeof(TimeSpan),
            typeof(Nullable<TimeSpan>),
            typeof(CEFActionResponse<>),
            typeof(SalesCollectionBaseModel<,,,,,,,,,,,>),
            typeof(Array),
            typeof(List<>),
            typeof(Dictionary<,>),
            typeof(Dictionary<,>.KeyCollection),
            typeof(Dictionary<,>.ValueCollection),
            typeof(ConcurrentDictionary<,>),
            typeof(DayOfWeek),
            typeof(DateTimeKind),
            typeof(PagedResultsBase<>),
            typeof(Type),
            typeof(MemberTypes),
            typeof(ImplementsIDOnQueryBase),
#pragma warning restore IDE0001,IDE0012,IDE0049,RCS1020
            // ReSharper restore BuiltInTypeReferenceStyle, ConvertNullableToShortForm
        };

        private void WriteDTOClasses(IndentedTextWriter writer)
        {
            ProcessFullDtoListToExtensions();
            writer.WriteLine();
            var orderedDTOs = DTOs.OrderBy(x => x.Name).ToList();
            for (var i = 0; i < orderedDTOs.Count; i++)
            {
                var dto = orderedDTOs.ElementAt(i);
                if (AlwaysSkipTypes.Any(x => x.Name == dto.Name))
                {
                    ////writer.WriteLine("// AlwaysSkipTypes: " + dto.Name);
                    continue;
                }
                if (dto.Name == "Nullable`1[]")
                {
                    ////writer.WriteLine("// Nullable`1[]: " + dto.Name);
                    continue;
                }
                if (dto.Name == "ResponseStatus[]")
                {
                    ////writer.WriteLine("// ResponseStatus[]: " + dto.Name);
                    continue;
                }
                if (dto.HasAttribute<RouteAttribute>()
                    && (usedInType == null || dto.HasAttributeNamed(usedInType.Name)))
                {
                    ////writer.WriteLine("// IsRoute: " + dto.Name);
                    continue;
                }
                var dtoProperties = dto
                    .GetProperties()
                    .Where(prop => prop.CanRead
                        && !prop.HasAttribute<IgnoreDataMemberAttribute>()
                        && !prop.HasAttribute<JsonIgnoreAttribute>()
                        && prop.Name != "noCache")
                    .ToArray();
                var isInheritedClass = dto.BaseType != null && dto.BaseType != typeof(object);
                var td = new TypeDeterminer(dto);
                if (dto.IsInterface)
                {
                    ////writer.WriteLine("// IsInterface: " + dto.Name);
                    continue;
                }
                if (WriteBlockHeader(writer, dto, isInheritedClass, td, isReact: false, isDtoFile: true))
                {
                    ////writer.WriteLine("// WriteBlockHeader true: " + dto.Name);
                    continue;
                }
                GenerateJsDoc(writer, dto, dtoProperties);
                writer.Indent++;
                if (dto.IsEnum)
                {
                    WriteEnumValues(writer, dto);
                }
                else
                {
                    WriteProperties(
                        writer,
                        dtoProperties,
                        isInheritedClass,
                        dto,
                        DetermineProcessedPropertiesBasedOnInterfaces(td, dto));
                }
                writer.Indent--;
                writer.WriteLine("}");
                writer.WriteLine(string.Empty);
            }
        }

        private void WriteDTOClassesForReact(IndentedTextWriter writer)
        {
            // NOTE: bodyWriter is to the cvApi._DtoClasses.ts, not the individual cvApi schema grouped files
            var importWriter = new IndentedTextWriter(new StringWriter(), Tab) { Indent = 0 };
            var bodyWriter = new IndentedTextWriter(new StringWriter(), Tab) { Indent = 0 };
            var requiredImports = new List<string>();
            ProcessFullDtoListToExtensions();
            bodyWriter.WriteLine();
            var orderedDTOs = DTOs.OrderBy(x => x.Name).ToList();
            var allDTONames = DTOs.Select(x => x.Name).ToList();
            for (var i = 0; i < orderedDTOs.Count; i++)
            {
                var dto = orderedDTOs.ElementAt(i);
                if (AlwaysSkipTypes.Any(x => x.Name == dto.Name))
                {
                    ////writer.WriteLine("// AlwaysSkipTypes: " + dto.Name);
                    continue;
                }
                if (dto.Name == "Nullable`1[]")
                {
                    ////writer.WriteLine("// Nullable`1[]: " + dto.Name);
                    continue;
                }
                if (dto.Name == "ResponseStatus[]")
                {
                    ////writer.WriteLine("// ResponseStatus[]: " + dto.Name);
                    continue;
                }
                if (dto.HasAttribute<RouteAttribute>()
                    && (usedInType == null || dto.HasAttributeNamed(usedInType.Name)))
                {
                    ////writer.WriteLine("// IsRoute: " + dto.Name);
                    continue;
                }
                var dtoProperties = dto
                    .GetProperties()
                    .Where(prop => prop.CanRead
                        && !prop.HasAttribute<IgnoreDataMemberAttribute>()
                        && !prop.HasAttribute<JsonIgnoreAttribute>()
                        && prop.Name != "noCache")
                    .ToArray();
                var isInheritedClass = dto.BaseType != null && dto.BaseType != typeof(object);
                var td = new TypeDeterminer(dto);
                if (dto.IsInterface)
                {
                    ////writer.WriteLine("// IsInterface: " + dto.Name);
                    continue;
                }
                var bases = new List<string>();
                if (WriteBlockHeader(bodyWriter, dto, isInheritedClass, td, addDTOToName: false, isReact: true, imports: bases))
                {
                    ////writer.WriteLine("// WriteBlockHeader true: " + dto.Name);
                    continue;
                }
                requiredImports.AddRange(bases.Where(x => !allDTONames.Contains(x)));
                GenerateJsDoc(bodyWriter, dto, dtoProperties);
                bodyWriter.Indent++;
                if (dto.IsEnum)
                {
                    WriteEnumValues(bodyWriter, dto);
                }
                else
                {
                    WriteProperties(
                        bodyWriter,
                        dtoProperties,
                        isInheritedClass,
                        dto,
                        DetermineProcessedPropertiesBasedOnInterfaces(td, dto));
                }
                bodyWriter.Indent--;
                bodyWriter.WriteLine("}");
                bodyWriter.WriteLine();
            }
            requiredImports = ParseRequiredImports(requiredImports.Distinct())
                .Where(x => !allDTONames.Contains(x))
                .ToList();
            // Add some unhandled values
            requiredImports.AddRange(new[]
            {
                "Guid",
                "KeyValuePair",
                "SalesItemBaseModel",
                "IAuthTokens",
            });
            if (requiredImports.Count > 0)
            {
                importWriter.WriteLine("import {");
                importWriter.Indent++;
                foreach (var requiredImport in requiredImports.Distinct())
                {
                    importWriter.WriteLine($"{requiredImport},");
                }
                importWriter.Indent--;
                importWriter.WriteLine("} from \"./cvApi.shared\";");
            }
            writer.Write(importWriter.InnerWriter.ToString());
            writer.Write(bodyWriter.InnerWriter.ToString()!.Replace("cefalt.store.", string.Empty).TrimEnd());
        }

        private List<string> ParseRequiredImports(IEnumerable<string> rawRequiredImports)
        {
            var result = new HashSet<string>();
            foreach (var rawImport in rawRequiredImports)
            {
                var value = rawImport.Replace("cefalt.store.", string.Empty);
                if (result.Contains(value))
                {
                    continue;
                }
                var importResult = GetRequiredTypesFromShared(value);
                foreach (var import in importResult)
                {
                    result.Add(import);
                }
            }
            return result.ToList();
        }

        private List<string> GetRequiredTypesFromShared(
            string tsType)
        {
            var result = new List<string>();
            if (tsType.Contains('<'))
            {
                // Recurse through its children
                var index = tsType.IndexOf('<');
                var end = tsType.LastIndexOf('>');
                var genericType = tsType.Substring(index + 1, end - index - 1);
                result.AddRange(GetRequiredTypesFromShared(genericType));
                // Cut out the generic part (ie "<ProductModel>") and just parse the rest
                var genericSubsection = tsType.Substring(index, end - index + 1);
                tsType = tsType.Replace(genericSubsection, string.Empty);
            }
            // Break apart comma-delimited stuff (ie from something like Dictionary<string, number>)
            if (tsType.Contains(','))
            {
                var subTypes = tsType.Split(',')
                    .Select(x => x.Trim());
                foreach (var subType in subTypes)
                {
                    result.AddRange(GetRequiredTypesFromShared(subType));
                }
            }
            else
            {
                result.Add(tsType);
            }
            return result;
        }

        private void ProcessFullDtoListToExtensions()
        {
            for (var i = 0; i < DTOs.Count; i++)
            {
                var dto = DTOs.ElementAt(i);
                if (AlwaysSkipTypes.Any(x => x.Name == dto.Name)
                    || dto.Name is "Nullable`1[]" or "ResponseStatus[]" || dto.HasAttribute<RouteAttribute>()
                        && (usedInType == null || dto.HasAttributeNamed(usedInType.Name)))
                {
                    continue;
                }
                var dtoProperties = dto
                    .GetProperties()
                    .Where(prop => prop.CanRead
                        && !prop.HasAttribute<IgnoreDataMemberAttribute>()
                        && !prop.HasAttribute<JsonIgnoreAttribute>()
                        && prop.Name != "noCache")
                    .ToArray();
                var isInheritedClass = dto.BaseType != null && dto.BaseType != typeof(object);
                var td = new TypeDeterminer(dto);
                if (dto.IsInterface || CheckDTOForBlockHeader(dto, isInheritedClass, td) || dto.IsEnum)
                {
                    continue;
                }
                var processedProperties = DetermineProcessedPropertiesBasedOnInterfaces(td, dto);
                foreach (var property in dtoProperties)
                {
                    try
                    {
                        // Don't re-declare inherited properties
                        if (isInheritedClass && dto.BaseType?.GetProperty(property.Name) != null
                            || processedProperties.Contains(property))
                        {
                            continue;
                        }
                        // Property on this class
                        ExamineDTOForMoreDTOWeNeed(property.GetMethod!.ReturnType, true);
                        DetermineTSType(property.GetMethod.ReturnType, true);
                    }
                    catch
                    {
                        // Do Nothing TODO: Report error somewhere?
                    }
                }
            }
        }

        private bool CheckDTOForBlockHeader(
            Type dto,
            bool isInheritedClass,
            TypeDeterminer td,
            bool addDTOToName = false)
        {
            switch (dto.Name)
            {
                case "Guid":
                case "SearchViewModelBase`2":
                case "TSearchForm":
                case "TIndexModel":
                case "SalesItemBaseModel":
                {
                    // Already handled
                    return true;
                }
            }
            if (dto.IsEnum)
            {
                return false;
            }
            ExamineDTOForMoreDTOWeNeed(dto, true);
            var extendsAdded = new List<string>();
            string? forParent = null;
            if (isInheritedClass)
            {
                ExamineDTOForMoreDTOWeNeed(dto.BaseType!, true);
                var tsType = DetermineTSType(dto.BaseType);
                forParent = addDTOToName ? tsType : DetermineTSType(dto);
                tsType ??= "any";
                if (tsType != "any")
                {
                    if (dto.BaseType.HasAttribute<RouteAttribute>()
                        && (usedInType == null || dto.BaseType.HasAttributeNamed(usedInType.Name)))
                    {
                        extendsAdded.Add(dto.BaseType!.Name);
                    }
                    else
                    {
                        extendsAdded.Add(tsType);
                    }
                }
            }
            if (td.IsIAppliedDiscountBase)
            {
                if (!extendsAdded.Contains("AppliedDiscountBaseModel"))
                {
                    extendsAdded.Add("AppliedDiscountBaseModel");
                }
                if (!extendsAdded.Contains("AmARelationshipTableModel"))
                {
                    extendsAdded.Add("AmARelationshipTableModel");
                }
                if (!extendsAdded.Contains("BaseModel"))
                {
                    extendsAdded.Add("BaseModel");
                }
                if (!extendsAdded.Contains("HaveJsonAttributesBaseModel"))
                {
                    extendsAdded.Add("HaveJsonAttributesBaseModel");
                }
            }
            else if (td.IsIAmAStoredFileRelationshipTable)
            {
                if (!extendsAdded.Contains("AmAStoredFileRelationshipTableModel"))
                {
                    extendsAdded.Add("AmAStoredFileRelationshipTableModel");
                }
                if (!extendsAdded.Contains("AmARelationshipTableModel"))
                {
                    extendsAdded.Add("AmARelationshipTableModel");
                }
                if (!extendsAdded.Contains("BaseModel"))
                {
                    extendsAdded.Add("BaseModel");
                }
                if (!extendsAdded.Contains("HaveJsonAttributesBaseModel"))
                {
                    extendsAdded.Add("HaveJsonAttributesBaseModel");
                }
            }
            else if (td.IsIAmARelationshipTable)
            {
                var tsType = HelpFunctions.SwapToModelType(DetermineTSType(td.RelationshipSecondaryType));
                if (tsType?.EndsWith("Model") == false)
                {
                    tsType += "Model";
                }
                if (tsType?.StartsWith("I") == true)
                {
                    tsType = tsType[1..];
                }
                if (!extendsAdded.Contains($"AmARelationshipTableModel<{tsType}>"))
                {
                    extendsAdded.Add($"AmARelationshipTableModel<{tsType}>");
                }
                if (!extendsAdded.Contains("BaseModel"))
                {
                    extendsAdded.Add("BaseModel");
                }
                if (!extendsAdded.Contains("HaveJsonAttributesBaseModel"))
                {
                    extendsAdded.Add("HaveJsonAttributesBaseModel");
                }
            }
            else if (td.IsISalesCollectionBaseT || td.IsISalesCollectionBase)
            {
                if (td.RelatedTypeType != null)
                {
                    var tsType = DetermineTSType(td.RelatedTypeType)?[1..];
                    if (!extendsAdded.Contains("HaveATypeModel<" + tsType + ">"))
                    {
                        extendsAdded.Add("HaveATypeModel<" + tsType + ">");
                    }
                }
                if (!extendsAdded.Contains("HaveAStateModel"))
                {
                    extendsAdded.Add("HaveAStateModel");
                }
                if (!extendsAdded.Contains("HaveAStatusModel"))
                {
                    extendsAdded.Add("HaveAStatusModel");
                }
                if (!extendsAdded.Contains("BaseModel"))
                {
                    extendsAdded.Add("BaseModel");
                }
                if (!extendsAdded.Contains("HaveJsonAttributesBaseModel"))
                {
                    extendsAdded.Add("HaveJsonAttributesBaseModel");
                }
            }
            else if (td.IsISalesItemBase && dto.Name != "SalesItemBaseModel")
            {
                var tsType = DetermineTSType(td.SalesItemBaseDiscountType);
                if (tsType?.StartsWith("IApplied") == true)
                {
                    tsType = tsType[1..];
                }
                if (!extendsAdded.Contains("SalesItemBaseModel<" + tsType + ">"))
                {
                    extendsAdded.Add("SalesItemBaseModel");
                }
                if (!extendsAdded.Contains("NameableBaseModel"))
                {
                    extendsAdded.Add("NameableBaseModel");
                }
                if (!extendsAdded.Contains("BaseModel"))
                {
                    extendsAdded.Add("BaseModel");
                }
                if (!extendsAdded.Contains("HaveAStatusBaseModel"))
                {
                    extendsAdded.Add("HaveAStatusBaseModel");
                }
                if (!extendsAdded.Contains("HaveJsonAttributesBaseModel"))
                {
                    extendsAdded.Add("HaveJsonAttributesBaseModel");
                }
            }
            else if (td.IsITypableBase && dto.Name != "TypableBaseModel")
            {
                if (!extendsAdded.Contains("TypableBaseModel"))
                {
                    extendsAdded.Add("TypableBaseModel");
                }
                if (!extendsAdded.Contains("DisplayableBaseModel"))
                {
                    extendsAdded.Add("DisplayableBaseModel");
                }
                if (!extendsAdded.Contains("NameableBaseModel"))
                {
                    extendsAdded.Add("NameableBaseModel");
                }
                if (!extendsAdded.Contains("BaseModel"))
                {
                    extendsAdded.Add("BaseModel");
                }
                if (!extendsAdded.Contains("HaveJsonAttributesBaseModel"))
                {
                    extendsAdded.Add("HaveJsonAttributesBaseModel");
                }
            }
            else if (td.IsIStatusableBase && dto.Name != "StatusableBaseModel")
            {
                if (!extendsAdded.Contains("StatusableBaseModel"))
                {
                    extendsAdded.Add("StatusableBaseModel");
                }
                if (!extendsAdded.Contains("DisplayableBaseModel"))
                {
                    extendsAdded.Add("DisplayableBaseModel");
                }
                if (!extendsAdded.Contains("NameableBaseModel"))
                {
                    extendsAdded.Add("NameableBaseModel");
                }
                if (!extendsAdded.Contains("BaseModel"))
                {
                    extendsAdded.Add("BaseModel");
                }
                if (!extendsAdded.Contains("HaveJsonAttributesBaseModel"))
                {
                    extendsAdded.Add("HaveJsonAttributesBaseModel");
                }
            }
            else if (td.IsIStateableBase && dto.Name != "StateableBaseModel")
            {
                if (!extendsAdded.Contains("StateableBaseModel"))
                {
                    extendsAdded.Add("StateableBaseModel");
                }
                if (!extendsAdded.Contains("DisplayableBaseModel"))
                {
                    extendsAdded.Add("DisplayableBaseModel");
                }
                if (!extendsAdded.Contains("NameableBaseModel"))
                {
                    extendsAdded.Add("NameableBaseModel");
                }
                if (!extendsAdded.Contains("BaseModel"))
                {
                    extendsAdded.Add("BaseModel");
                }
                if (!extendsAdded.Contains("HaveJsonAttributesBaseModel"))
                {
                    extendsAdded.Add("HaveJsonAttributesBaseModel");
                }
            }
            else if (td.IsIDisplayableBase && dto.Name != "DisplayableBaseModel")
            {
                if (!extendsAdded.Contains("DisplayableBaseModel"))
                {
                    extendsAdded.Add("DisplayableBaseModel");
                }
                if (!extendsAdded.Contains("NameableBaseModel"))
                {
                    extendsAdded.Add("NameableBaseModel");
                }
                if (!extendsAdded.Contains("BaseModel"))
                {
                    extendsAdded.Add("BaseModel");
                }
                if (!extendsAdded.Contains("HaveJsonAttributesBaseModel"))
                {
                    extendsAdded.Add("HaveJsonAttributesBaseModel");
                }
            }
            else if (td.IsINameableBase && dto.Name != "NameableBaseModel")
            {
                if (!extendsAdded.Contains("NameableBaseModel"))
                {
                    extendsAdded.Add("NameableBaseModel");
                }
                if (!extendsAdded.Contains("BaseModel"))
                {
                    extendsAdded.Add("BaseModel");
                }
                if (!extendsAdded.Contains("HaveJsonAttributesBaseModel"))
                {
                    extendsAdded.Add("HaveJsonAttributesBaseModel");
                }
            }
            else if (td.IsIBase && dto.Name != "BaseModel")
            {
                if (!extendsAdded.Contains("BaseModel"))
                {
                    extendsAdded.Add("BaseModel");
                }
                if (!extendsAdded.Contains("HaveJsonAttributesBaseModel"))
                {
                    extendsAdded.Add("HaveJsonAttributesBaseModel");
                }
            }
            if (td.IsIHaveAParentBase && !extendsAdded.Contains($"HaveAParentBaseModel<{forParent}>"))
            {
                extendsAdded.Add($"HaveAParentBaseModel<{forParent}>");
            }
            if (td.IsIHaveJsonAttributesBase
                && !(td.IsISalesCollectionBase || td.IsISalesCollectionBaseT)
                && !extendsAdded.Contains("HaveJsonAttributesBaseModel"))
            {
                extendsAdded.Add("HaveJsonAttributesBaseModel");
            }
            if (td.IsIHaveSeoBase && !extendsAdded.Contains("HaveSeoBaseModel"))
            {
                extendsAdded.Add("HaveSeoBaseModel");
            }
            if (td.IsIHaveNotesBase && !extendsAdded.Contains("HaveNotesBaseModel"))
            {
                extendsAdded.Add("HaveNotesBaseModel");
            }
            if (td.IsIHaveImagesBase)
            {
                var tsType = DetermineTSType(td.IsIHaveImagesBaseImageType)?[1..];
                if (!extendsAdded.Contains($"HaveImagesBaseModel<{tsType}>"))
                {
                    extendsAdded.Add($"HaveImagesBaseModel<{tsType}>");
                }
            }
            if (td.IsIImageBase && !extendsAdded.Contains("IImageBaseModel"))
            {
                extendsAdded.Add("IImageBaseModel");
            }
            if (td.IsIHaveStoredFilesBase)
            {
                var tsType = DetermineTSType(td.IsIHaveStoredFilesBaseFileType)?[1..];
                if (!extendsAdded.Contains($"HaveStoredFilesBaseModel<{tsType}>"))
                {
                    extendsAdded.Add($"HaveStoredFilesBaseModel<{tsType}>");
                }
            }
            if (td.IsIAmFilterableByAccountT && !td.IsIAmFilterableByAccountIsTheRelationship)
            {
                var tsType = DetermineTSType(td.IAmFilterableByAccountTType)?[1..];
                if (!extendsAdded.Contains($"AmFilterableByAccountsBaseModel<{tsType}>"))
                {
                    extendsAdded.Add($"AmFilterableByAccountsBaseModel<{tsType}>");
                }
            }
            if (td.IsIAmFilterableByBrandT && !td.IsIAmFilterableByBrandIsTheRelationship)
            {
                var tsType = DetermineTSType(td.IAmFilterableByBrandTType)?[1..];
                if (!extendsAdded.Contains($"AmFilterableByBrandsBaseModel<{tsType}>"))
                {
                    extendsAdded.Add($"AmFilterableByBrandsBaseModel<{tsType}>");
                }
            }
            if (td.IsIAmFilterableByCategoryT && !td.IsIAmFilterableByCategoryIsTheRelationship)
            {
                var tsType = DetermineTSType(td.IAmFilterableByCategoryTType)?[1..];
                if (!extendsAdded.Contains($"AmFilterableByCategoriesBaseModel<{tsType}>"))
                {
                    extendsAdded.Add($"AmFilterableByCategoriesBaseModel<{tsType}>");
                }
            }
            if (td.IsIAmFilterableByFranchiseT && !td.IsIAmFilterableByFranchiseIsTheRelationship)
            {
                var tsType = DetermineTSType(td.IAmFilterableByFranchiseTType)?[1..];
                if (!extendsAdded.Contains($"AmFilterableByFranchisesBaseModel<{tsType}>"))
                {
                    extendsAdded.Add($"AmFilterableByFranchisesBaseModel<{tsType}>");
                }
            }
            if (td.IsIAmFilterableByManufacturerT && !td.IsIAmFilterableByManufacturerIsTheRelationship)
            {
                var tsType = DetermineTSType(td.IAmFilterableByManufacturerTType)?[1..];
                if (!extendsAdded.Contains($"AmFilterableByManufacturersBaseModel<{tsType}>"))
                {
                    extendsAdded.Add($"AmFilterableByManufacturersBaseModel<{tsType}>");
                }
            }
            if (td.IsIAmFilterableByProductT && !td.IsIAmFilterableByProductIsTheRelationship)
            {
                var tsType = DetermineTSType(td.IAmFilterableByProductTType)?[1..];
                if (!extendsAdded.Contains($"AmFilterableByProductsBaseModel<{tsType}>"))
                {
                    extendsAdded.Add($"AmFilterableByProductsBaseModel<{tsType}>");
                }
            }
            if (td.IsIAmFilterableByStoreT && !td.IsIAmFilterableByStoreIsTheRelationship)
            {
                var tsType = DetermineTSType(td.IAmFilterableByStoreTType)?[1..];
                if (!extendsAdded.Contains($"AmFilterableByStoresBaseModel<{tsType}>"))
                {
                    extendsAdded.Add($"AmFilterableByStoresBaseModel<{tsType}>");
                }
            }
            if (td.IsIAmFilterableByUserT && !td.IsIAmFilterableByUserIsTheRelationship)
            {
                var tsType = DetermineTSType(td.IAmFilterableByUserTType)?[1..];
                if (!extendsAdded.Contains($"AmFilterableByUsersBaseModel<{tsType}>"))
                {
                    extendsAdded.Add($"AmFilterableByUsersBaseModel<{tsType}>");
                }
            }
            if (td.IsIAmFilterableByVendorT && !td.IsIAmFilterableByVendorIsTheRelationship)
            {
                var tsType = DetermineTSType(td.IAmFilterableByVendorTType)?[1..];
                if (!extendsAdded.Contains($"AmFilterableByVendorsBaseModel<{tsType}>"))
                {
                    extendsAdded.Add($"AmFilterableByVendorsBaseModel<{tsType}>");
                }
            }
            if (td.IsIAmFilterableByAccount && !td.IsIAmFilterableByAccountIsTheRelationship && !extendsAdded.Contains("AmFilterableByAccountBaseModel"))
            {
                extendsAdded.Add("AmFilterableByAccountBaseModel");
            }
            if (td.IsIAmFilterableByBrand && !td.IsIAmFilterableByBrandIsTheRelationship && !extendsAdded.Contains("AmFilterableByBrandBaseModel"))
            {
                extendsAdded.Add("AmFilterableByBrandBaseModel");
            }
            if (td.IsIAmFilterableByCategory && !td.IsIAmFilterableByCategoryIsTheRelationship && !extendsAdded.Contains("AmFilterableByCategoryBaseModel"))
            {
                extendsAdded.Add("AmFilterableByCategoryBaseModel");
            }
            if (td.IsIAmFilterableByFranchise && !td.IsIAmFilterableByFranchiseIsTheRelationship && !extendsAdded.Contains("AmFilterableByFranchiseBaseModel"))
            {
                extendsAdded.Add("AmFilterableByFranchiseBaseModel");
            }
            if (td.IsIAmFilterableByManufacturer && !td.IsIAmFilterableByManufacturerIsTheRelationship && !extendsAdded.Contains("AmFilterableByManufacturerBaseModel"))
            {
                extendsAdded.Add("AmFilterableByManufacturerBaseModel");
            }
            if (td.IsIAmFilterableByProduct && !td.IsIAmFilterableByProductIsTheRelationship && !extendsAdded.Contains("AmFilterableByProductBaseModel"))
            {
                extendsAdded.Add("AmFilterableByProductBaseModel");
            }
            if (td.IsIAmFilterableByStore && !td.IsIAmFilterableByStoreIsTheRelationship && !extendsAdded.Contains("AmFilterableByStoreBaseModel"))
            {
                extendsAdded.Add("AmFilterableByStoreBaseModel");
            }
            if (td.IsIAmFilterableByUser && !td.IsIAmFilterableByUserIsTheRelationship && !extendsAdded.Contains("AmFilterableByUserBaseModel"))
            {
                extendsAdded.Add("AmFilterableByUserBaseModel");
            }
            if (td.IsIAmFilterableByVendor && !td.IsIAmFilterableByVendorIsTheRelationship && !extendsAdded.Contains("AmFilterableByVendorBaseModel"))
            {
                extendsAdded.Add("AmFilterableByVendorBaseModel");
            }
            if (td.IsIAmFilterableByNullableAccount && !extendsAdded.Contains("AmFilterableByNullableAccountBaseModel"))
            {
                extendsAdded.Add("AmFilterableByNullableAccountBaseModel");
            }
            if (td.IsIAmFilterableByNullableBrand && !extendsAdded.Contains("AmFilterableByNullableBrandBaseModel"))
            {
                extendsAdded.Add("AmFilterableByNullableBrandBaseModel");
            }
            if (td.IsIAmFilterableByNullableCategory && !extendsAdded.Contains("AmFilterableByNullableCategoryBaseModel"))
            {
                extendsAdded.Add("AmFilterableByNullableCategoryBaseModel");
            }
            if (td.IsIAmFilterableByNullableFranchise && !extendsAdded.Contains("AmFilterableByNullableFranchiseBaseModel"))
            {
                extendsAdded.Add("AmFilterableByNullableFranchiseBaseModel");
            }
            if (td.IsIAmFilterableByNullableManufacturer && !extendsAdded.Contains("AmFilterableByNullableManufacturerBaseModel"))
            {
                extendsAdded.Add("AmFilterableByNullableManufacturerBaseModel");
            }
            if (td.IsIAmFilterableByNullableProduct && !extendsAdded.Contains("AmFilterableByNullableProductBaseModel"))
            {
                extendsAdded.Add("AmFilterableByNullableProductBaseModel");
            }
            if (td.IsIAmFilterableByNullableStore && !extendsAdded.Contains("AmFilterableByNullableStoreBaseModel"))
            {
                extendsAdded.Add("AmFilterableByNullableStoreBaseModel");
            }
            if (td.IsIAmFilterableByNullableUser && !extendsAdded.Contains("AmFilterableByNullableUserBaseModel"))
            {
                extendsAdded.Add("AmFilterableByNullableUserBaseModel");
            }
            if (td.IsIAmFilterableByNullableVendor && !extendsAdded.Contains("AmFilterableByNullableVendorBaseModel"))
            {
                extendsAdded.Add("AmFilterableByNullableVendorBaseModel");
            }
            if (td.IsIHaveAStatusBase)
            {
                if (!extendsAdded.Contains("HaveAStatusModel"))
                {
                    extendsAdded.Add("HaveAStatusModel");
                }
            }
            if (td.IsIHaveATypeBase)
            {
                var tsType = DetermineTSType(td.RelatedTypeType)?[1..];
                if (!extendsAdded.Contains("HaveATypeModel<" + tsType + ">"))
                {
                    extendsAdded.Add("HaveATypeModel<" + tsType + ">");
                }
            }
            if (td.IsIHaveAStateBase && !extendsAdded.Contains("HaveAStateModel"))
            {
                extendsAdded.Add("HaveAStateModel");
            }
            if (td.IsIHaveAContactBase && !td.IsIHaveAContactBaseIsTheRelationship
                && !extendsAdded.Contains("HaveAContactBaseModel"))
            {
                extendsAdded.Add("HaveAContactBaseModel");
            }
            if (td.IsIHaveANullableContactBase && !extendsAdded.Contains("HaveANullableContactBaseModel"))
            {
                extendsAdded.Add("HaveANullableContactBaseModel");
            }
            if (td.IsIHaveOrderMinimumsBase && !extendsAdded.Contains("HaveOrderMinimumsBaseModel"))
            {
                extendsAdded.Add("HaveOrderMinimumsBaseModel");
            }
            if (td.IsIHaveFreeShippingMinimumsBase && !extendsAdded.Contains("HaveFreeShippingMinimumsBaseModel"))
            {
                extendsAdded.Add("HaveFreeShippingMinimumsBaseModel");
            }
            if (td.IsIHaveDimensions && !extendsAdded.Contains("HaveDimensionsBaseModel"))
            {
                extendsAdded.Add("HaveDimensionsBaseModel");
            }
            if (td.IsIHaveNullableDimensions && !extendsAdded.Contains("HaveNullableDimensionsBaseModel"))
            {
                extendsAdded.Add("HaveNullableDimensionsBaseModel");
            }
            if (td.IsIHaveRequiresRolesBase && !extendsAdded.Contains("HaveRequiresRolesBaseModel"))
            {
                extendsAdded.Add("HaveRequiresRolesBaseModel");
            }
            return false;
        }

        /// <summary>Writes a block header.</summary>
        /// <param name="writer">          The writer.</param>
        /// <param name="dto">             The data transfer object.</param>
        /// <param name="isInheritedClass">True if this TypescriptCodeGenerator is inherited class.</param>
        /// <param name="td">              The td.</param>
        /// <param name="addDTOToName">    True to add data transfer object to name.</param>
        /// <param name="isReact">         True if this TypescriptCodeGenerator is react.</param>
        /// <param name="isDtoFile">       True if this TypescriptCodeGenerator is data transfer object file.</param>
        /// <param name="imports">         The imports.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool WriteBlockHeader(
            IndentedTextWriter writer,
            Type dto,
            bool isInheritedClass,
            TypeDeterminer td,
            bool addDTOToName = false,
            bool isReact = false,
            bool isDtoFile = false,
            List<string>? imports = null)
        {
            switch (dto.Name)
            {
                case "Guid":
                case "SearchViewModelBase`2":
                case "TSearchForm":
                case "TIndexModel":
                case "SalesItemBaseModel":
                {
                    // Already handled
                    return true;
                }
            }
            if (dto.IsEnum)
            {
                writer.WriteLine("export enum " + dto.Name + " {");
                return false;
            }
            var isSAD = dto.Name == nameof(SerializableAttributesDictionary);
            var headerToWrite = "/**";
            var dontPrintIndirectInherits = false;
            if (dto.HasAttribute<RouteAttribute>()
                && (usedInType == null || dto.HasAttributeNamed(usedInType.Name)))
            {
                var attribute = dto.GetCustomAttributes<RouteAttribute>(false).First();
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * "
                    + (!string.IsNullOrWhiteSpace(attribute.Summary)
                        ? attribute.Summary
                        : "WARNING! There is no Summary value on this endpoint, please ask a Developer to add one");
                dontPrintIndirectInherits = true;
            }
            var toWrite = $"export {(isSAD ? "class" : "interface")} {dto.Name}{(addDTOToName ? "Dto" : string.Empty)}";
            var first = true;
            ExamineDTOForMoreDTOWeNeed(dto, true);
            var extendsAdded = new List<string>();
            string? forParent = null;
            if (isInheritedClass)
            {
                ExamineDTOForMoreDTOWeNeed(dto.BaseType, true);
                var tsType = DetermineTSType(dto.BaseType);
                forParent = addDTOToName ? tsType : DetermineTSType(dto);
                tsType ??= "any";
                if (tsType != "any")
                {
                    // ReSharper disable once AssignNullToNotNullAttribute
                    if (dto.BaseType.HasAttribute<RouteAttribute>()
                        && (usedInType == null || dto.BaseType.HasAttributeNamed(usedInType.Name)))
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link {dto.BaseType!.Name}Dto}}";
                        // ReSharper disable once ConditionIsAlwaysTrueOrFalse, PossibleNullReferenceException
                        toWrite += (first ? " extends " : ", ") + dto.BaseType.Name + "Dto";
                        extendsAdded.Add(dto.BaseType.Name);
                        dontPrintIndirectInherits = true;
                    }
                    else
                    {
                        headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link {tsType}}}";
                        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                        toWrite += (first ? " extends " : ", ") + tsType;
                        extendsAdded.Add(tsType);
                    }
                    first = false;
                }
            }
            if (td.IsIAppliedDiscountBase && !dontPrintIndirectInherits)
            {
                if (!extendsAdded.Contains("AppliedDiscountBaseModel"))
                {
                    headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AppliedDiscountBaseModel}}";
                    toWrite += (first ? " extends " : ", ") + "AppliedDiscountBaseModel";
                    extendsAdded.Add("AppliedDiscountBaseModel");
                    // ReSharper disable once RedundantAssignment
                    first = false;
                }
                if (!extendsAdded.Contains("AmARelationshipTableModel"))
                {
                    extendsAdded.Add("AmARelationshipTableModel");
                }
                if (!extendsAdded.Contains("BaseModel"))
                {
                    extendsAdded.Add("BaseModel");
                }
                if (!extendsAdded.Contains("HaveJsonAttributesBaseModel"))
                {
                    extendsAdded.Add("HaveJsonAttributesBaseModel");
                }
            }
            else if (td.IsIAmAStoredFileRelationshipTable && !dontPrintIndirectInherits)
            {
                if (!extendsAdded.Contains("AmAStoredFileRelationshipTableModel"))
                {
                    headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmAStoredFileRelationshipTableModel}}";
                    toWrite += (first ? " extends " : ", ") + "AmAStoredFileRelationshipTableModel";
                    extendsAdded.Add("AmAStoredFileRelationshipTableModel");
                    // ReSharper disable once RedundantAssignment
                    first = false;
                }
                if (!extendsAdded.Contains("AmARelationshipTableModel"))
                {
                    extendsAdded.Add("AmARelationshipTableModel");
                }
                if (!extendsAdded.Contains("BaseModel"))
                {
                    extendsAdded.Add("BaseModel");
                }
                if (!extendsAdded.Contains("HaveJsonAttributesBaseModel"))
                {
                    extendsAdded.Add("HaveJsonAttributesBaseModel");
                }
            }
            else if (td.IsIAmARelationshipTable && !dontPrintIndirectInherits)
            {
                var tsType = HelpFunctions.SwapToModelType(DetermineTSType(td.RelationshipSecondaryType));
                if (tsType?.EndsWith("Model") == false)
                {
                    tsType += "Model";
                }
                if (tsType?.StartsWith("I") == true)
                {
                    tsType = tsType[1..];
                }
                if (!extendsAdded.Contains($"AmARelationshipTableModel<{tsType}>"))
                {
                    headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmARelationshipTableModel<{tsType}>}}";
                    toWrite += (first ? " extends " : ", ") + $"AmARelationshipTableModel<{tsType}>";
                    extendsAdded.Add($"AmARelationshipTableModel<{tsType}>");
                    // ReSharper disable once RedundantAssignment
                    first = false;
                }
                if (!extendsAdded.Contains("BaseModel"))
                {
                    extendsAdded.Add("BaseModel");
                }
                if (!extendsAdded.Contains("HaveJsonAttributesBaseModel"))
                {
                    extendsAdded.Add("HaveJsonAttributesBaseModel");
                }
            }
            else if ((td.IsISalesCollectionBaseT || td.IsISalesCollectionBase) && !dontPrintIndirectInherits)
            {
                ////if (dto.Name == "SalesCollectionBaseModel")
                ////{
                ////    if (!extendsAdded.Any(x => x.StartsWith("SalesCollectionBaseModel")))
                ////    {
                ////        toWrite += (first ? " extends " : ", ") + "SalesCollectionBaseModel<>";
                ////        extendsAdded.Add("SalesCollectionBaseModel<>");
                ////        first = false;
                ////    }
                ////}
                ////else
                ////{
                ////    if (!extendsAdded.Any(x => x.StartsWith("SalesCollectionBaseModel")))
                ////    {
                ////        toWrite += (first ? " extends " : ", ") + "SalesCollectionBaseModel<>";
                ////        extendsAdded.Add("SalesCollectionBaseModel<>");
                ////        first = false;
                ////    }
                ////}
                if (td.RelatedTypeType != null)
                {
                    var tsType = DetermineTSType(td.RelatedTypeType)?[1..];
                    if (!extendsAdded.Contains("HaveATypeModel<" + tsType + ">"))
                    {
                        extendsAdded.Add("HaveATypeModel<" + tsType + ">");
                    }
                }
                if (!extendsAdded.Contains("HaveAStateModel"))
                {
                    extendsAdded.Add("HaveAStateModel");
                }
                if (!extendsAdded.Contains("HaveAStatusModel"))
                {
                    extendsAdded.Add("HaveAStatusModel");
                }
                if (!extendsAdded.Contains("BaseModel"))
                {
                    extendsAdded.Add("BaseModel");
                }
                if (!extendsAdded.Contains("HaveJsonAttributesBaseModel"))
                {
                    extendsAdded.Add("HaveJsonAttributesBaseModel");
                }
            }
            else if (td.IsISalesItemBase && dto.Name != "SalesItemBaseModel" && !dontPrintIndirectInherits)
            {
                var tsType = DetermineTSType(td.SalesItemBaseDiscountType);
                if (tsType?.StartsWith("IApplied") == true)
                {
                    tsType = tsType[1..];
                }
                if (!extendsAdded.Contains("SalesItemBaseModel<" + tsType + ">"))
                {
                    headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link SalesItemBaseModel}}";
                    toWrite += (first ? " extends " : ", ") + "SalesItemBaseModel";
                    extendsAdded.Add("SalesItemBaseModel");
                    first = false;
                }
                if (!extendsAdded.Contains("NameableBaseModel"))
                {
                    extendsAdded.Add("NameableBaseModel");
                }
                if (!extendsAdded.Contains("BaseModel"))
                {
                    extendsAdded.Add("BaseModel");
                }
                if (!extendsAdded.Contains("HaveAStatusBaseModel"))
                {
                    extendsAdded.Add("HaveAStatusBaseModel");
                }
                if (!extendsAdded.Contains("HaveJsonAttributesBaseModel"))
                {
                    extendsAdded.Add("HaveJsonAttributesBaseModel");
                }
            }
            else if (td.IsITypableBase && dto.Name != "TypableBaseModel" && !dontPrintIndirectInherits)
            {
                if (!extendsAdded.Contains("TypableBaseModel"))
                {
                    headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link TypableBaseModel}}";
                    toWrite += (first ? " extends " : ", ") + "TypableBaseModel";
                    extendsAdded.Add("TypableBaseModel");
                    first = false;
                }
                if (!extendsAdded.Contains("DisplayableBaseModel"))
                {
                    extendsAdded.Add("DisplayableBaseModel");
                }
                if (!extendsAdded.Contains("NameableBaseModel"))
                {
                    extendsAdded.Add("NameableBaseModel");
                }
                if (!extendsAdded.Contains("BaseModel"))
                {
                    extendsAdded.Add("BaseModel");
                }
                if (!extendsAdded.Contains("HaveJsonAttributesBaseModel"))
                {
                    extendsAdded.Add("HaveJsonAttributesBaseModel");
                }
            }
            else if (td.IsIStatusableBase && dto.Name != "StatusableBaseModel" && !dontPrintIndirectInherits)
            {
                if (!extendsAdded.Contains("StatusableBaseModel"))
                {
                    headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link StatusableBaseModel}}";
                    toWrite += (first ? " extends " : ", ") + "StatusableBaseModel";
                    extendsAdded.Add("StatusableBaseModel");
                    first = false;
                }
                if (!extendsAdded.Contains("DisplayableBaseModel"))
                {
                    extendsAdded.Add("DisplayableBaseModel");
                }
                if (!extendsAdded.Contains("NameableBaseModel"))
                {
                    extendsAdded.Add("NameableBaseModel");
                }
                if (!extendsAdded.Contains("BaseModel"))
                {
                    extendsAdded.Add("BaseModel");
                }
                if (!extendsAdded.Contains("HaveJsonAttributesBaseModel"))
                {
                    extendsAdded.Add("HaveJsonAttributesBaseModel");
                }
            }
            else if (td.IsIStateableBase && dto.Name != "StateableBaseModel" && !dontPrintIndirectInherits)
            {
                if (!extendsAdded.Contains("StateableBaseModel"))
                {
                    headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link StateableBaseModel}}";
                    toWrite += (first ? " extends " : ", ") + "StateableBaseModel";
                    extendsAdded.Add("StateableBaseModel");
                    first = false;
                }
                if (!extendsAdded.Contains("DisplayableBaseModel"))
                {
                    extendsAdded.Add("DisplayableBaseModel");
                }
                if (!extendsAdded.Contains("NameableBaseModel"))
                {
                    extendsAdded.Add("NameableBaseModel");
                }
                if (!extendsAdded.Contains("BaseModel"))
                {
                    extendsAdded.Add("BaseModel");
                }
                if (!extendsAdded.Contains("HaveJsonAttributesBaseModel"))
                {
                    extendsAdded.Add("HaveJsonAttributesBaseModel");
                }
            }
            else if (td.IsIDisplayableBase && dto.Name != "DisplayableBaseModel" && !dontPrintIndirectInherits)
            {
                if (!extendsAdded.Contains("DisplayableBaseModel"))
                {
                    headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link DisplayableBaseModel}}";
                    toWrite += (first ? " extends " : ", ") + "DisplayableBaseModel";
                    extendsAdded.Add("DisplayableBaseModel");
                    first = false;
                }
                if (!extendsAdded.Contains("NameableBaseModel"))
                {
                    extendsAdded.Add("NameableBaseModel");
                }
                if (!extendsAdded.Contains("BaseModel"))
                {
                    extendsAdded.Add("BaseModel");
                }
                if (!extendsAdded.Contains("HaveJsonAttributesBaseModel"))
                {
                    extendsAdded.Add("HaveJsonAttributesBaseModel");
                }
            }
            else if (td.IsINameableBase && dto.Name != "NameableBaseModel" && !dontPrintIndirectInherits)
            {
                if (!extendsAdded.Contains("NameableBaseModel"))
                {
                    headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link NameableBaseModel}}";
                    toWrite += (first ? " extends " : ", ") + "NameableBaseModel";
                    extendsAdded.Add("NameableBaseModel");
                    first = false;
                }
                if (!extendsAdded.Contains("BaseModel"))
                {
                    extendsAdded.Add("BaseModel");
                }
                if (!extendsAdded.Contains("HaveJsonAttributesBaseModel"))
                {
                    extendsAdded.Add("HaveJsonAttributesBaseModel");
                }
            }
            else if (td.IsIBase && dto.Name != "BaseModel" && !dontPrintIndirectInherits)
            {
                if (!extendsAdded.Contains("BaseModel"))
                {
                    headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link BaseModel}}";
                    toWrite += (first ? " extends " : ", ") + "BaseModel";
                    extendsAdded.Add("BaseModel");
                    first = false;
                }
                if (!extendsAdded.Contains("HaveJsonAttributesBaseModel"))
                {
                    extendsAdded.Add("HaveJsonAttributesBaseModel");
                }
            }
            if (td.IsIHaveAParentBase && !dontPrintIndirectInherits && !extendsAdded.Contains($"HaveAParentBaseModel<{forParent}>"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link HaveAParentBaseModel<{forParent}>}}";
                toWrite += (first ? " extends " : ", ") + $"HaveAParentBaseModel<{forParent}>";
                extendsAdded.Add($"HaveAParentBaseModel<{forParent}>");
                first = false;
            }
            if (td.IsIHaveJsonAttributesBase && !dontPrintIndirectInherits
                && !(td.IsISalesCollectionBase || td.IsISalesCollectionBaseT)
                && !extendsAdded.Contains("HaveJsonAttributesBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link HaveJsonAttributesBaseModel}}";
                toWrite += (first ? " extends " : ", ") + "HaveJsonAttributesBaseModel";
                extendsAdded.Add("HaveJsonAttributesBaseModel");
                first = false;
            }
            if (td.IsIHaveSeoBase && !dontPrintIndirectInherits && !extendsAdded.Contains("HaveSeoBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link HaveSeoBaseModel}}";
                toWrite += (first ? " extends " : ", ") + "HaveSeoBaseModel";
                extendsAdded.Add("HaveSeoBaseModel");
                first = false;
            }
            if (td.IsIHaveNotesBase && !dontPrintIndirectInherits && !extendsAdded.Contains("HaveNotesBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link HaveNotesBaseModel}}";
                toWrite += (first ? " extends " : ", ") + "HaveNotesBaseModel";
                extendsAdded.Add("HaveNotesBaseModel");
                first = false;
            }
            if (td.IsIHaveImagesBase && !dontPrintIndirectInherits)
            {
                var tsType = DetermineTSType(td.IsIHaveImagesBaseImageType)?[1..];
                if (!extendsAdded.Contains($"HaveImagesBaseModel<{tsType}>"))
                {
                    headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link HaveImagesBaseModel<{tsType}>}}";
                    toWrite += (first ? " extends " : ", ") + $"HaveImagesBaseModel<{tsType}>";
                    extendsAdded.Add($"HaveImagesBaseModel<{tsType}>");
                    first = false;
                }
            }
            if (td.IsIImageBase && !dontPrintIndirectInherits && !extendsAdded.Contains("IImageBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link IImageBaseModel}}";
                toWrite += (first ? " extends " : ", ") + "IImageBaseModel";
                extendsAdded.Add("IImageBaseModel");
                first = false;
            }
            if (td.IsIHaveStoredFilesBase && !dontPrintIndirectInherits)
            {
                var tsType = DetermineTSType(td.IsIHaveStoredFilesBaseFileType)?[1..];
                if (!extendsAdded.Contains($"HaveStoredFilesBaseModel<{tsType}>"))
                {
                    headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link HaveStoredFilesBaseModel<{tsType}>}}";
                    toWrite += (first ? " extends " : ", ") + $"HaveStoredFilesBaseModel<{tsType}>";
                    extendsAdded.Add($"HaveStoredFilesBaseModel<{tsType}>");
                    first = false;
                }
            }
            if (td.IsIAmFilterableByAccountT && !dontPrintIndirectInherits && !td.IsIAmFilterableByAccountIsTheRelationship)
            {
                var tsType = DetermineTSType(td.IAmFilterableByAccountTType)?[1..];
                if (!extendsAdded.Contains($"AmFilterableByAccountsBaseModel<{tsType}>"))
                {
                    headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByAccountsBaseModel<{tsType}>}}";
                    toWrite += $"{(first ? " extends " : ", ")}AmFilterableByAccountsBaseModel<{tsType}>";
                    extendsAdded.Add($"AmFilterableByAccountsBaseModel<{tsType}>");
                    first = false;
                }
            }
            if (td.IsIAmFilterableByBrandT && !dontPrintIndirectInherits && !td.IsIAmFilterableByBrandIsTheRelationship)
            {
                var tsType = DetermineTSType(td.IAmFilterableByBrandTType)?[1..];
                if (!extendsAdded.Contains($"AmFilterableByBrandsBaseModel<{tsType}>"))
                {
                    headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByBrandsBaseModel<{tsType}>}}";
                    toWrite += $"{(first ? " extends " : ", ")}AmFilterableByBrandsBaseModel<{tsType}>";
                    extendsAdded.Add($"AmFilterableByBrandsBaseModel<{tsType}>");
                    first = false;
                }
            }
            if (td.IsIAmFilterableByCategoryT && !dontPrintIndirectInherits && !td.IsIAmFilterableByCategoryIsTheRelationship)
            {
                var tsType = DetermineTSType(td.IAmFilterableByCategoryTType)?[1..];
                if (!extendsAdded.Contains($"AmFilterableByCategoriesBaseModel<{tsType}>"))
                {
                    headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByCategoriesBaseModel<{tsType}>}}";
                    toWrite += $"{(first ? " extends " : ", ")}AmFilterableByCategoriesBaseModel<{tsType}>";
                    extendsAdded.Add($"AmFilterableByCategoriesBaseModel<{tsType}>");
                    first = false;
                }
            }
            if (td.IsIAmFilterableByFranchiseT && !dontPrintIndirectInherits && !td.IsIAmFilterableByFranchiseIsTheRelationship)
            {
                var tsType = DetermineTSType(td.IAmFilterableByFranchiseTType)?[1..];
                if (!extendsAdded.Contains($"AmFilterableByFranchisesBaseModel<{tsType}>"))
                {
                    headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByFranchisesBaseModel<{tsType}>}}";
                    toWrite += $"{(first ? " extends " : ", ")}AmFilterableByFranchisesBaseModel<{tsType}>";
                    extendsAdded.Add($"AmFilterableByFranchisesBaseModel<{tsType}>");
                    first = false;
                }
            }
            if (td.IsIAmFilterableByManufacturerT && !dontPrintIndirectInherits && !td.IsIAmFilterableByManufacturerIsTheRelationship)
            {
                var tsType = DetermineTSType(td.IAmFilterableByManufacturerTType)?[1..];
                if (!extendsAdded.Contains($"AmFilterableByManufacturersBaseModel<{tsType}>"))
                {
                    headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByManufacturersBaseModel<{tsType}>}}";
                    toWrite += $"{(first ? " extends " : ", ")}AmFilterableByManufacturersBaseModel<{tsType}>";
                    extendsAdded.Add($"AmFilterableByManufacturersBaseModel<{tsType}>");
                    first = false;
                }
            }
            if (td.IsIAmFilterableByProductT && !dontPrintIndirectInherits && !td.IsIAmFilterableByProductIsTheRelationship)
            {
                var tsType = DetermineTSType(td.IAmFilterableByProductTType)?[1..];
                if (!extendsAdded.Contains($"AmFilterableByProductsBaseModel<{tsType}>"))
                {
                    headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByProductsBaseModel<{tsType}>}}";
                    toWrite += $"{(first ? " extends " : ", ")}AmFilterableByProductsBaseModel<{tsType}>";
                    extendsAdded.Add($"AmFilterableByProductsBaseModel<{tsType}>");
                    first = false;
                }
            }
            if (td.IsIAmFilterableByStoreT && !dontPrintIndirectInherits && !td.IsIAmFilterableByStoreIsTheRelationship)
            {
                var tsType = DetermineTSType(td.IAmFilterableByStoreTType)?[1..];
                if (!extendsAdded.Contains($"AmFilterableByStoresBaseModel<{tsType}>"))
                {
                    headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByStoresBaseModel<{tsType}>}}";
                    toWrite += $"{(first ? " extends " : ", ")}AmFilterableByStoresBaseModel<{tsType}>";
                    extendsAdded.Add($"AmFilterableByStoresBaseModel<{tsType}>");
                    first = false;
                }
            }
            if (td.IsIAmFilterableByUserT && !dontPrintIndirectInherits && !td.IsIAmFilterableByUserIsTheRelationship)
            {
                var tsType = DetermineTSType(td.IAmFilterableByUserTType)?[1..];
                if (!extendsAdded.Contains($"AmFilterableByUsersBaseModel<{tsType}>"))
                {
                    headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByUsersBaseModel<{tsType}>}}";
                    toWrite += $"{(first ? " extends " : ", ")}AmFilterableByUsersBaseModel<{tsType}>";
                    extendsAdded.Add($"AmFilterableByUsersBaseModel<{tsType}>");
                    first = false;
                }
            }
            if (td.IsIAmFilterableByVendorT && !dontPrintIndirectInherits && !td.IsIAmFilterableByVendorIsTheRelationship)
            {
                var tsType = DetermineTSType(td.IAmFilterableByVendorTType)?[1..];
                if (!extendsAdded.Contains($"AmFilterableByVendorsBaseModel<{tsType}>"))
                {
                    headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByVendorsBaseModel<{tsType}>}}";
                    toWrite += $"{(first ? " extends " : ", ")}AmFilterableByVendorsBaseModel<{tsType}>";
                    extendsAdded.Add($"AmFilterableByVendorsBaseModel<{tsType}>");
                    first = false;
                }
            }
            if (td.IsIAmFilterableByAccount && !dontPrintIndirectInherits && !td.IsIAmFilterableByAccountIsTheRelationship && !extendsAdded.Contains("AmFilterableByAccountBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByAccountBaseModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByAccountBaseModel";
                extendsAdded.Add("AmFilterableByAccountBaseModel");
                first = false;
            }
            if (td.IsIAmFilterableByBrand && !dontPrintIndirectInherits && !td.IsIAmFilterableByBrandIsTheRelationship && !extendsAdded.Contains("AmFilterableByBrandBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByBrandBaseModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByBrandBaseModel";
                extendsAdded.Add("AmFilterableByBrandBaseModel");
                first = false;
            }
            if (td.IsIAmFilterableByCategory && !dontPrintIndirectInherits && !td.IsIAmFilterableByCategoryIsTheRelationship && !extendsAdded.Contains("AmFilterableByCategoryBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByCategoryBaseModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByCategoryBaseModel";
                extendsAdded.Add("AmFilterableByCategoryBaseModel");
                first = false;
            }
            if (td.IsIAmFilterableByFranchise && !dontPrintIndirectInherits && !td.IsIAmFilterableByFranchiseIsTheRelationship && !extendsAdded.Contains("AmFilterableByFranchiseBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByFranchiseBaseModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByFranchiseBaseModel";
                extendsAdded.Add("AmFilterableByFranchiseBaseModel");
                first = false;
            }
            if (td.IsIAmFilterableByManufacturer && !dontPrintIndirectInherits && !td.IsIAmFilterableByManufacturerIsTheRelationship && !extendsAdded.Contains("AmFilterableByManufacturerBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByManufacturerBaseModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByManufacturerBaseModel";
                extendsAdded.Add("AmFilterableByManufacturerBaseModel");
                first = false;
            }
            if (td.IsIAmFilterableByProduct && !dontPrintIndirectInherits && !td.IsIAmFilterableByProductIsTheRelationship && !td.IsIAmFilterableByProductIsTheRelationship && !extendsAdded.Contains("AmFilterableByProductBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByProductBaseModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByProductBaseModel";
                extendsAdded.Add("AmFilterableByProductBaseModel");
                first = false;
            }
            if (td.IsIAmFilterableByStore && !dontPrintIndirectInherits && !td.IsIAmFilterableByStoreIsTheRelationship && !extendsAdded.Contains("AmFilterableByStoreBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByStoreBaseModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByStoreBaseModel";
                extendsAdded.Add("AmFilterableByStoreBaseModel");
                first = false;
            }
            if (td.IsIAmFilterableByUser && !dontPrintIndirectInherits && !td.IsIAmFilterableByUserIsTheRelationship && !extendsAdded.Contains("AmFilterableByUserBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByUserBaseModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByUserBaseModel";
                extendsAdded.Add("AmFilterableByUserBaseModel");
                first = false;
            }
            if (td.IsIAmFilterableByVendor && !dontPrintIndirectInherits && !td.IsIAmFilterableByVendorIsTheRelationship && !extendsAdded.Contains("AmFilterableByVendorBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByVendorBaseModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByVendorBaseModel";
                extendsAdded.Add("AmFilterableByVendorBaseModel");
                first = false;
            }
            if (td.IsIAmFilterableByNullableAccount && !dontPrintIndirectInherits && !extendsAdded.Contains("AmFilterableByNullableAccountBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByNullableAccountBaseModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByNullableAccountBaseModel";
                extendsAdded.Add("AmFilterableByNullableAccountBaseModel");
                first = false;
            }
            if (td.IsIAmFilterableByNullableBrand && !dontPrintIndirectInherits && !extendsAdded.Contains("AmFilterableByNullableBrandBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByNullableBrandBaseModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByNullableBrandBaseModel";
                extendsAdded.Add("AmFilterableByNullableBrandBaseModel");
                first = false;
            }
            if (td.IsIAmFilterableByNullableCategory && !dontPrintIndirectInherits && !extendsAdded.Contains("AmFilterableByNullableCategoryBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByNullableCategoryBaseModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByNullableCategoryBaseModel";
                extendsAdded.Add("AmFilterableByNullableCategoryBaseModel");
                first = false;
            }
            if (td.IsIAmFilterableByNullableFranchise && !dontPrintIndirectInherits && !extendsAdded.Contains("AmFilterableByNullableFranchiseBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByNullableFranchiseBaseModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByNullableFranchiseBaseModel";
                extendsAdded.Add("AmFilterableByNullableFranchiseBaseModel");
                first = false;
            }
            if (td.IsIAmFilterableByNullableManufacturer && !dontPrintIndirectInherits && !extendsAdded.Contains("AmFilterableByNullableManufacturerBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByNullableManufacturerBaseModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByNullableManufacturerBaseModel";
                extendsAdded.Add("AmFilterableByNullableManufacturerBaseModel");
                first = false;
            }
            if (td.IsIAmFilterableByNullableProduct && !dontPrintIndirectInherits && !extendsAdded.Contains("AmFilterableByNullableProductBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByNullableProductBaseModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByNullableProductBaseModel";
                extendsAdded.Add("AmFilterableByNullableProductBaseModel");
                first = false;
            }
            if (td.IsIAmFilterableByNullableStore && !dontPrintIndirectInherits && !extendsAdded.Contains("AmFilterableByNullableStoreBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByNullableStoreBaseModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByNullableStoreBaseModel";
                extendsAdded.Add("AmFilterableByNullableStoreBaseModel");
                first = false;
            }
            if (td.IsIAmFilterableByNullableUser && !dontPrintIndirectInherits && !extendsAdded.Contains("AmFilterableByNullableUserBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByNullableUserBaseModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByNullableUserBaseModel";
                extendsAdded.Add("AmFilterableByNullableUserBaseModel");
                first = false;
            }
            if (td.IsIAmFilterableByNullableVendor && !dontPrintIndirectInherits && !extendsAdded.Contains("AmFilterableByNullableVendorBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByNullableVendorBaseModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByNullableVendorBaseModel";
                extendsAdded.Add("AmFilterableByNullableVendorBaseModel");
                first = false;
            }
            if (td.IsIAmFilterableByAccountSearch && !dontPrintIndirectInherits && !extendsAdded.Contains("AmFilterableByAccountBaseSearchModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByAccountBaseSearchModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByAccountBaseSearchModel";
                extendsAdded.Add("AmFilterableByAccountBaseSearchModel");
                first = false;
            }
            if (td.IsIAmFilterableByBrandSearch && !dontPrintIndirectInherits && !extendsAdded.Contains("AmFilterableByBrandBaseSearchModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByBrandBaseSearchModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByBrandBaseSearchModel";
                extendsAdded.Add("AmFilterableByBrandBaseSearchModel");
                first = false;
            }
            if (td.IsIAmFilterableByCategorySearch && !dontPrintIndirectInherits && !extendsAdded.Contains("AmFilterableByCategoryBaseSearchModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByCategoryBaseSearchModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByCategoryBaseSearchModel";
                extendsAdded.Add("AmFilterableByCategoryBaseSearchModel");
                first = false;
            }
            if (td.IsIAmFilterableByFranchiseSearch && !dontPrintIndirectInherits && !extendsAdded.Contains("AmFilterableByFranchiseBaseSearchModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByFranchiseBaseSearchModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByFranchiseBaseSearchModel";
                extendsAdded.Add("AmFilterableByFranchiseBaseSearchModel");
                first = false;
            }
            if (td.IsIAmFilterableByManufacturerSearch && !dontPrintIndirectInherits && !extendsAdded.Contains("AmFilterableByManufacturerBaseSearchModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByManufacturerBaseSearchModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByManufacturerBaseSearchModel";
                extendsAdded.Add("AmFilterableByManufacturerBaseSearchModel");
                first = false;
            }
            if (td.IsIAmFilterableByProductSearch && !dontPrintIndirectInherits && !extendsAdded.Contains("AmFilterableByProductBaseSearchModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByProductBaseSearchModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByProductBaseSearchModel";
                extendsAdded.Add("AmFilterableByProductBaseSearchModel");
                first = false;
            }
            if (td.IsIAmFilterableByStoreSearch && !dontPrintIndirectInherits && !extendsAdded.Contains("AmFilterableByStoreBaseSearchModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByStoreBaseSearchModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByStoreBaseSearchModel";
                extendsAdded.Add("AmFilterableByStoreBaseSearchModel");
                first = false;
            }
            if (td.IsIAmFilterableByUserSearch && !dontPrintIndirectInherits && !extendsAdded.Contains("AmFilterableByUserBaseSearchModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByUserBaseSearchModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByUserBaseSearchModel";
                extendsAdded.Add("AmFilterableByUserBaseSearchModel");
                first = false;
            }
            if (td.IsIAmFilterableByVendorSearch && !dontPrintIndirectInherits && !extendsAdded.Contains("AmFilterableByVendorBaseSearchModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link AmFilterableByVendorBaseSearchModel}}";
                toWrite += $"{(first ? " extends " : ", ")}AmFilterableByVendorBaseSearchModel";
                extendsAdded.Add("AmFilterableByVendorBaseSearchModel");
                first = false;
            }
            if (td.IsIHaveAStatusBase && !dontPrintIndirectInherits && !extendsAdded.Contains("HaveAStatusModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link HaveAStatusModel}}";
                toWrite += (first ? " extends " : ", ") + "HaveAStatusModel";
                extendsAdded.Add("HaveAStatusModel");
                first = false;
            }
            if (td.IsIHaveAStatusBaseSearch && !dontPrintIndirectInherits && !extendsAdded.Contains("HaveAStatusSearchModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link HaveAStatusSearchModel}}";
                toWrite += (first ? " extends " : ", ") + "HaveAStatusSearchModel";
                extendsAdded.Add("HaveAStatusSearchModel");
                first = false;
            }
            if (td.IsIHaveAStateBaseSearch && !dontPrintIndirectInherits && !extendsAdded.Contains("HaveAStateSearchModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link HaveAStateSearchModel}}";
                toWrite += (first ? " extends " : ", ") + "HaveAStateSearchModel";
                extendsAdded.Add("HaveAStateSearchModel");
                first = false;
            }
            if (td.IsIHaveATypeBase && !dontPrintIndirectInherits)
            {
                var tsType = DetermineTSType(td.RelatedTypeType)?[1..];
                if (!extendsAdded.Contains("HaveATypeModel<" + tsType + ">"))
                {
                    headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link HaveATypeModel<{tsType}>}}";
                    toWrite += (first ? " extends " : ", ") + "HaveATypeModel<" + tsType + ">";
                    extendsAdded.Add("HaveATypeModel<" + tsType + ">");
                    first = false;
                }
            }
            if (td.IsIHaveATypeBaseSearch && !dontPrintIndirectInherits && !extendsAdded.Contains("HaveATypeSearchModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link HaveATypeSearchModel}}";
                toWrite += (first ? " extends " : ", ") + "HaveATypeSearchModel";
                extendsAdded.Add("HaveATypeSearchModel");
                first = false;
            }
            if (td.IsIHaveAStateBase && !dontPrintIndirectInherits && !extendsAdded.Contains("HaveAStateModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link HaveAStateModel}}";
                toWrite += (first ? " extends " : ", ") + "HaveAStateModel";
                extendsAdded.Add("HaveAStateModel");
                first = false;
            }
            if (td.IsIHaveAContactBase && !dontPrintIndirectInherits && !td.IsIHaveAContactBaseIsTheRelationship
                && !extendsAdded.Contains("HaveAContactBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link HaveAContactBaseModel}}";
                toWrite += (first ? " extends " : ", ") + "HaveAContactBaseModel";
                extendsAdded.Add("HaveAContactBaseModel");
                first = false;
            }
            if (td.IsIHaveANullableContactBase && !dontPrintIndirectInherits && !extendsAdded.Contains("HaveANullableContactBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link HaveANullableContactBaseModel}}";
                toWrite += (first ? " extends " : ", ") + "HaveANullableContactBaseModel";
                extendsAdded.Add("HaveANullableContactBaseModel");
                // ReSharper disable once RedundantAssignment
                first = false;
            }
            if (td.IsIHaveAContactBaseSearch && !td.IsIHaveAContactBaseSearchIsTheRelationship
                && !dontPrintIndirectInherits
                && !extendsAdded.Contains("HaveAContactBaseSearchModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link HaveAContactBaseSearchModel}}";
                toWrite += (first ? " extends " : ", ") + "HaveAContactBaseSearchModel";
                extendsAdded.Add("HaveAContactBaseSearchModel");
                // ReSharper disable once RedundantAssignment
                first = false;
            }
            if (td.IsIHaveOrderMinimumsBase && !dontPrintIndirectInherits && !extendsAdded.Contains("HaveOrderMinimumsBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link HaveOrderMinimumsBaseModel}}";
                toWrite += (first ? " extends " : ", ") + "HaveOrderMinimumsBaseModel";
                extendsAdded.Add("HaveOrderMinimumsBaseModel");
                first = false;
            }
            if (td.IsIHaveFreeShippingMinimumsBase && !dontPrintIndirectInherits && !extendsAdded.Contains("HaveFreeShippingMinimumsBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link HaveFreeShippingMinimumsBaseModel}}";
                toWrite += (first ? " extends " : ", ") + "HaveFreeShippingMinimumsBaseModel";
                extendsAdded.Add("HaveFreeShippingMinimumsBaseModel");
                first = false;
            }
            if (td.IsIHaveDimensions && !dontPrintIndirectInherits && !extendsAdded.Contains("HaveDimensionsBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link HaveDimensionsBaseModel}}";
                toWrite += (first ? " extends " : ", ") + "HaveDimensionsBaseModel";
                extendsAdded.Add("HaveDimensionsBaseModel");
                first = false;
            }
            if (td.IsIHaveNullableDimensions && !dontPrintIndirectInherits && !extendsAdded.Contains("HaveNullableDimensionsBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link HaveNullableDimensionsBaseModel}}";
                toWrite += (first ? " extends " : ", ") + "HaveNullableDimensionsBaseModel";
                extendsAdded.Add("HaveNullableDimensionsBaseModel");
                first = false;
            }
            if (td.IsIHaveRequiresRolesBase && !dontPrintIndirectInherits && !extendsAdded.Contains("HaveRequiresRolesBaseModel"))
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @see {{@link HaveRequiresRolesBaseModel}}";
                toWrite += (first ? " extends " : ", ") + "HaveRequiresRolesBaseModel";
                extendsAdded.Add("HaveRequiresRolesBaseModel");
                // ReSharper disable once RedundantAssignment
                first = false;
            }
            if (td.IsDeprecated)
            {
                headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @deprecated " + td.DeprecatedMessage;
            }
            toWrite += " {";
            headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} * @public";
            headerToWrite += $"\r\n{(isReact ? string.Empty : "\t")} */";
            if (!isReact && !isDtoFile)
            {
                writer.Indent--;
            }
            writer.WriteLine(headerToWrite);
            writer.WriteLine(toWrite);
            imports?.AddRange(extendsAdded);
            if (!isReact && !isDtoFile)
            {
                writer.Indent++;
            }
            return false;
        }

        private static void WriteEnumValues(TextWriter writer, Type dto)
        {
            foreach (var value in dto.GetEnumValues())
            {
                if (value is int)
                {
                    writer.WriteLine(dto.GetEnumName(value) + " = " + value + ",");
                }
                else
                {
                    writer.WriteLine(value + ",");
                }
            }
        }

        private static List<PropertyInfo> DetermineProcessedPropertiesBasedOnInterfaces(TypeDeterminer td, Type theType)
        {
            var processedProperties = new List<PropertyInfo>(theType.GetProperties().Where(x => x.Name == "noCache"));
            if (theType.Name == "User")
            {
                // ASP.NET Identity Properties
                var properties = new[] { "Id", "Roles", "Claims", "Logins", "PasswordHash" };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIHaveAStatusBase)
            {
                var properties = new[] { "StatusID", "Status", "StatusKey", "StatusName", "StatusDisplayName", "StatusTranslationKey", "StatusSortOrder" };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIHaveAStateBase)
            {
                var properties = new[] { "StateID", "Status", "StateKey", "StateName", "StateDisplayName", "StateTranslationKey", "StateSortOrder" };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIHaveATypeBase)
            {
                var properties = new[] { "TypeID", "Type", "TypeKey", "TypeName", "TypeDisplayName", "TypeTranslationKey", "TypeSortOrder" };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIHaveAContactBase || td.IsIHaveANullableContactBase)
            {
                var properties = new[]
                {
                    "ContactID",
                    "Contact",
                    "ContactKey",
                    "ContactPhone",
                    "ContactFax",
                    "ContactEmail",
                    "ContactFirstName",
                    "ContactLastName",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIHaveSeoBase)
            {
                var properties = new[]
                {
                    "SeoKeywords",
                    "SeoUrl",
                    "SeoDescription",
                    "SeoMetaData",
                    "SeoPageTitle",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAppliedDiscountBase)
            {
                var properties = new[]
                {
                    "MasterID",
                    "MasterKey",
                    "MasterName",
                    "Master",
                    "SlaveID",
                    "SlaveKey",
                    "SlaveName",
                    "Slave",
                    "DiscountID",
                    "DiscountKey",
                    "DiscountName",
                    "Discount",
                    "DiscountTypeID",
                    "DiscountPriority",
                    "DiscountValueType",
                    "DiscountValue",
                    "DiscountCanCombine",
                    "DiscountTotal",
                    "ApplicationsUsed",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            else if (td.IsIAmAStoredFileRelationshipTable)
            {
                var properties = new[]
                {
                    "MasterID",
                    "MasterKey",
                    "MasterName",
                    "Master",
                    "SlaveID",
                    "SlaveKey",
                    "SlaveName",
                    "Slave",
                    "FileAccessTypeID",
                    "SortOrder",
                    "StoredFileID",
                    "StoredFileKey",
                    "StoredFileName",
                    "StoredFile",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            else if (td.IsIAmARelationshipTable)
            {
                var properties = new[]
                {
                    "MasterID",
                    "MasterKey",
                    "Master",
                    "SlaveID",
                    "SlaveKey",
                    "Slave",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIImageBase)
            {
                var properties = new[]
                {
                    "SortOrder",
                    "DisplayName",
                    "SeoTitle",
                    "Author",
                    "MediaDate",
                    "Copyright",
                    "Location",
                    "Latitude",
                    "Longitude",
                    "IsPrimary",
                    "OriginalWidth",
                    "OriginalHeight",
                    "OriginalFileFormat",
                    "OriginalFileName",
                    "OriginalIsStoredInDB",
                    "OriginalBytes",
                    "ThumbnailWidth",
                    "ThumbnailHeight",
                    "ThumbnailFileFormat",
                    "ThumbnailFileName",
                    "ThumbnailIsStoredInDB",
                    "ThumbnailBytes",
                    "MasterID",
                    "MasterKey",
                    "Master",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIHaveJsonAttributesBase)
            {
                processedProperties.AddRange(theType.GetProperties().Where(x => x.Name is "JsonAttributes" or "SerializableAttributes"));
            }
            if (td.IsIHaveRequiresRolesBase)
            {
                processedProperties.AddRange(theType.GetProperties().Where(x => x.Name == "RequiresRoles"));
            }
            if (td.IsIHaveNotesBase)
            {
                processedProperties.AddRange(theType.GetProperties().Where(x => x.Name == "Notes"));
            }
            if (td.IsIHaveImagesBase)
            {
                processedProperties.AddRange(theType.GetProperties().Where(x => x.Name is "Images" or "PrimaryImageFileName"));
            }
            if (td.IsIHaveStoredFilesBase)
            {
                processedProperties.AddRange(theType.GetProperties().Where(x => x.Name == "StoredFiles"));
            }
            if (td.IsIAmADiscountFilterRelationshipTable)
            {
                var properties = new[]
                {
                    "DiscountID",
                    "Discount",
                    "MasterID",
                    "Master",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIObjectNoteBase)
            {
                var properties = new[]
                {
                    "MasterID",
                    "Master",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByAccountT)
            {
                var properties = new[] { "Accounts" };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByBrandT)
            {
                var properties = new[] { "Brands" };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByCategoryT)
            {
                var properties = new[] { "Categories" };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByFranchiseT)
            {
                var properties = new[] { "Franchises" };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByManufacturerT)
            {
                var properties = new[] { "Manufacturers" };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByProductT)
            {
                var properties = new[] { "Products" };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByStoreT)
            {
                var properties = new[] { "Stores" };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByUserT)
            {
                var properties = new[] { "Users" };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByVendorT)
            {
                var properties = new[] { "Vendors" };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByAccount || td.IsIAmFilterableByNullableAccount)
            {
                var properties = new[] { "AccountID", "Account", "AccountKey", "AccountName", "AccountSeoUrl" };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByBrand || td.IsIAmFilterableByNullableBrand)
            {
                var properties = new[] { "BrandID", "Brand", "BrandKey", "BrandName" };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByCategory || td.IsIAmFilterableByNullableCategory)
            {
                var properties = new[] { "CategoryID", "Category", "CategoryKey", "CategoryName", "CategorySeoUrl" };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByFranchise || td.IsIAmFilterableByNullableFranchise)
            {
                var properties = new[] { "FranchiseID", "Franchise", "FranchiseKey", "FranchiseName" };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByManufacturer || td.IsIAmFilterableByNullableManufacturer)
            {
                var properties = new[] { "ManufacturerID", "Manufacturer", "ManufacturerKey", "ManufacturerName" };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByProduct || td.IsIAmFilterableByNullableProduct)
            {
                var properties = new[] { "ProductID", "Product", "ProductKey", "ProductName", "ProductSeoUrl" };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByStore || td.IsIAmFilterableByNullableStore)
            {
                var properties = new[] { "StoreID", "Store", "StoreKey", "StoreName", "StoreSeoUrl" };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByUser || td.IsIAmFilterableByNullableUser)
            {
                var properties = new[] { "UserID", "User", "UserKey", "UserName" };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByVendor || td.IsIAmFilterableByNullableVendor)
            {
                var properties = new[] { "VendorID", "Vendor", "VendorKey", "VendorName" };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIHaveDimensions || td.IsIHaveNullableDimensions)
            {
                var properties = new[]
                {
                    "Width",
                    "WidthUnitOfMeasure",
                    "Depth",
                    "DepthUnitOfMeasure",
                    "Height",
                    "HeightUnitOfMeasure",
                    "Weight",
                    "WeightUnitOfMeasure",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIHaveRequiresRolesBase)
            {
                var properties = new[]
                {
                    "RequiresRoles",
                    "RequiresRolesList",
                    "RequiresRolesAlt",
                    "RequiresRolesListAlt",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIHaveAParentBase)
            {
                var properties = new[]
                {
                    "ParentID",
                    "ParentKey",
                    "Parent",
                    "Children",
                    "HasChildren",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsISalesCollectionBase)
            {
                var properties = new[]
                {
                    "DueDate",
                    "OriginalDate",
                    "BalanceDue",
                    "SubtotalItems",
                    "SubtotalShipping",
                    "SubtotalTaxes",
                    "SubtotalFees",
                    "SubtotalHandling",
                    "SubtotalDiscounts",
                    "Total",
                    "ItemQuantity",
                    "SessionID",
                    "ShippingSameAsBilling",
                    "BillingContactID",
                    "BillingContact",
                    "BillingContactKey",
                    "ShippingContactID",
                    "ShippingContact",
                    "ShippingContactKey",
                    "UserID",
                    "User",
                    "UserKey",
                    "UserUserName",
                    "UserContactEmail",
                    "UserContactFirstName",
                    "UserContactLastName",
                    "AccountID",
                    "Account",
                    "AccountKey",
                    "AccountName",
                    "TypeID",
                    "Type",
                    "TypeKey",
                    "TypeName",
                    "TypeDisplayName",
                    "TypeTranslationKey",
                    "TypeSortOrder",
                    "StatusID",
                    "Status",
                    "StatusKey",
                    "StatusName",
                    "StatusDisplayName",
                    "StatusTranslationKey",
                    "StatusSortOrder",
                    "StateID",
                    "State",
                    "StateKey",
                    "StateName",
                    "StateDisplayName",
                    "StateTranslationKey",
                    "StateSortOrder",
                    "StoreID",
                    "Store",
                    "StoreKey",
                    "StoreName",
                    "StoreSeoUrl",
                    "BrandID",
                    "Brand",
                    "BrandKey",
                    "BrandName",
                    "FranchiseID",
                    "Franchise",
                    "FranchiseKey",
                    "FranchiseName",
                    "ShipmentID",
                    "Shipment",
                    "ShipmentKey",
                    "Discounts",
                    "SalesItems",
                    "Files",
                    "Contacts",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsISalesItemBase)
            {
                var properties = new[]
                {
                    "Master",
                    "Quantity",
                    "QuantityBackOrdered",
                    "QuantityPreSold",
                    "UnitCorePrice",
                    "UnitSoldPrice",
                    "Sku",
                    "UnitOfMeasure",
                    "ProductID",
                    "Product",
                    "UserID",
                    "User",
                    "StatusID",
                    "Status",
                    "VendorProductID",
                    "VendorProduct",
                    "Attributes",
                    "Discounts",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIHaveOrderMinimumsBase)
            {
                var properties = new[]
                {
                    "MinimumOrderDollarAmount",
                    "MinimumOrderDollarAmountAfter",
                    "MinimumOrderDollarAmountWarningMessage",
                    "MinimumOrderDollarAmountOverrideFee",
                    "MinimumOrderDollarAmountOverrideFeeIsPercent",
                    "MinimumOrderDollarAmountOverrideFeeWarningMessage",
                    "MinimumOrderDollarAmountOverrideFeeAcceptedMessage",
                    "MinimumOrderQuantityAmount",
                    "MinimumOrderQuantityAmountAfter",
                    "MinimumOrderQuantityAmountWarningMessage",
                    "MinimumOrderQuantityAmountOverrideFee",
                    "MinimumOrderQuantityAmountOverrideFeeIsPercent",
                    "MinimumOrderQuantityAmountOverrideFeeWarningMessage",
                    "MinimumOrderQuantityAmountOverrideFeeAcceptedMessage",
                    "MinimumOrderDollarAmountBufferProductID",
                    "MinimumOrderDollarAmountBufferProductKey",
                    "MinimumOrderDollarAmountBufferProductName",
                    "MinimumOrderDollarAmountBufferProduct",
                    "MinimumOrderQuantityAmountBufferProductID",
                    "MinimumOrderQuantityAmountBufferProductKey",
                    "MinimumOrderQuantityAmountBufferProductName",
                    "MinimumOrderQuantityAmountBufferProduct",
                    "MinimumOrderDollarAmountBufferCategoryID",
                    "MinimumOrderDollarAmountBufferCategoryKey",
                    "MinimumOrderDollarAmountBufferCategoryName",
                    "MinimumOrderDollarAmountBufferCategory",
                    "MinimumOrderQuantityAmountBufferCategoryID",
                    "MinimumOrderQuantityAmountBufferCategoryKey",
                    "MinimumOrderQuantityAmountBufferCategoryName",
                    "MinimumOrderQuantityAmountBufferCategory",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIHaveFreeShippingMinimumsBase)
            {
                var properties = new[]
                {
                    "MinimumForFreeShippingDollarAmount",
                    "MinimumForFreeShippingDollarAmountAfter",
                    "MinimumForFreeShippingDollarAmountWarningMessage",
                    "MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage",
                    "MinimumForFreeShippingQuantityAmount",
                    "MinimumForFreeShippingQuantityAmountAfter",
                    "MinimumForFreeShippingQuantityAmountWarningMessage",
                    "MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage",
                    "MinimumForFreeShippingDollarAmountBufferProductID",
                    "MinimumForFreeShippingDollarAmountBufferProductKey",
                    "MinimumForFreeShippingDollarAmountBufferProductName",
                    "MinimumForFreeShippingDollarAmountBufferProduct",
                    "MinimumForFreeShippingQuantityAmountBufferProductID",
                    "MinimumForFreeShippingQuantityAmountBufferProductKey",
                    "MinimumForFreeShippingQuantityAmountBufferProductName",
                    "MinimumForFreeShippingQuantityAmountBufferProduct",
                    "MinimumForFreeShippingDollarAmountBufferCategoryID",
                    "MinimumForFreeShippingDollarAmountBufferCategoryKey",
                    "MinimumForFreeShippingDollarAmountBufferCategoryName",
                    "MinimumForFreeShippingDollarAmountBufferCategory",
                    "MinimumForFreeShippingQuantityAmountBufferCategoryID",
                    "MinimumForFreeShippingQuantityAmountBufferCategoryKey",
                    "MinimumForFreeShippingQuantityAmountBufferCategoryName",
                    "MinimumForFreeShippingQuantityAmountBufferCategory",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIHaveATypeBaseSearch)
            {
                var properties = new[]
                {
                    "ExcludedTypeID",
                    "TypeID",
                    "ExcludedTypeIDs",
                    "TypeIDs",
                    "ExcludedTypeDisplayName",
                    "ExcludedTypeKey",
                    "ExcludedTypeName",
                    "TypeDisplayName",
                    "TypeKey",
                    "TypeName",
                    "TypeTranslationKey",
                    "ExcludedTypeDisplayNames",
                    "ExcludedTypeKeys",
                    "ExcludedTypeNames",
                    "TypeDisplayNames",
                    "TypeKeys",
                    "TypeNames",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIHaveAStatusBaseSearch)
            {
                var properties = new[]
                {
                    "ExcludedStatusID",
                    "StatusID",
                    "ExcludedStatusIDs",
                    "StatusIDs",
                    "ExcludedStatusDisplayName",
                    "ExcludedStatusKey",
                    "ExcludedStatusName",
                    "StatusDisplayName",
                    "StatusKey",
                    "StatusName",
                    "StatusTranslationKey",
                    "ExcludedStatusDisplayNames",
                    "ExcludedStatusKeys",
                    "ExcludedStatusNames",
                    "StatusDisplayNames",
                    "StatusKeys",
                    "StatusNames",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIHaveAContactBaseSearch)
            {
                var properties = new[]
                {
                    "ContactID",
                    "ContactKey",
                    "ContactName",
                    "ContactFirstName",
                    "ContactLastName",
                    "ContactPhone",
                    "ContactFax",
                    "ContactEmail",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByAccountSearch)
            {
                var properties = new[]
                {
                    "AccountID",
                    "AccountKey",
                    "AccountName",
                    "AccountSeoUrl",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByBrandSearch)
            {
                var properties = new[]
                {
                    "BrandID",
                    "BrandKey",
                    "BrandName",
                    "BrandCategoryID",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByCategorySearch)
            {
                var properties = new[]
                {
                    "CategoryID",
                    "CategoryKey",
                    "CategoryName",
                    "CategorySeoUrl",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByFranchiseSearch)
            {
                var properties = new[]
                {
                    "FranchiseID",
                    "FranchiseKey",
                    "FranchiseName",
                    "FranchiseCategoryID",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByManufacturerSearch)
            {
                var properties = new[]
                {
                    "ManufacturerID",
                    "ManufacturerKey",
                    "ManufacturerName",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByProductSearch)
            {
                var properties = new[]
                {
                    "ProductID",
                    "ProductKey",
                    "ProductName",
                    "ProductSeoUrl",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByStoreSearch)
            {
                var properties = new[]
                {
                    "StoreID",
                    "StoreKey",
                    "StoreName",
                    "StoreSeoUrl",
                    "StoreCountryID",
                    "StoreRegionID",
                    "StoreCity",
                    "StoreAnyCountryID",
                    "StoreAnyRegionID",
                    "StoreAnyCity",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByUserSearch)
            {
                var properties = new[]
                {
                    "UserID",
                    "UserKey",
                    "UserUsername",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            if (td.IsIAmFilterableByVendorSearch)
            {
                var properties = new[]
                {
                    "VendorID",
                    "VendorKey",
                    "VendorName",
                };
                processedProperties.AddRange(theType.GetProperties().Where(x => properties.Contains(x.Name)));
            }
            return processedProperties;
        }

        private void WriteProperties(
            TextWriter writer,
            IEnumerable<PropertyInfo> dtoProperties,
            bool isInheritedClass,
            Type dto,
            ICollection<PropertyInfo> processedProperties)
        {
            foreach (var property in dtoProperties)
            {
                try
                {
                    // Don't re-declare inherited properties
                    if (isInheritedClass && dto.BaseType?.GetProperty(property.Name) != null
                        || processedProperties.Contains(property))
                    {
                        continue;
                    }
                    // Property on this class
                    var returnType = property.GetMethod!.ReturnType;
                    ExamineDTOForMoreDTOWeNeed(returnType, true);
                    var apiMemberAttr = property.GetCustomAttribute<ApiMemberAttribute>();
                    var nullable = returnType == typeof(string)
                        || returnType.IsNullableType()
#if NET5_0_OR_GREATER
                        || returnType.IsClass
                        || returnType.IsInterface
#else
                        || returnType.IsClass()
                        || returnType.IsInterface()
#endif
                        || apiMemberAttr is { IsRequired: true }
                            ? "?"
                            : string.Empty;
                    var hasApiMemberAttr = apiMemberAttr != null
                        && !string.IsNullOrWhiteSpace(apiMemberAttr.Name)
                        && apiMemberAttr.Name != property.Name;
                    var name = hasApiMemberAttr ? apiMemberAttr!.Name : property.Name;
                    writer.WriteLine(
                        "{0}{1}: {2};{3}",
                        name,
                        nullable,
                        DetermineTSType(returnType, true)?.Replace("IStoreModel", "StoreModel"),
                        hasApiMemberAttr ? " // Name format overridden" : string.Empty);
                }
                catch (Exception e)
                {
                    writer.WriteLine("// BUG ERROR! Unable to emit property: " + property.Name);
                    writer.WriteLine("//     " + e.Message);
                }
            }
        }
    }
}
