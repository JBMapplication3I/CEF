// <copyright file="SalesQuoteImporterProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales quote importer provider base class</summary>
// ReSharper disable VirtualMemberNeverOverridden.Global
namespace Clarity.Ecommerce.Providers.SalesQuoteImporter
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    ////using CodeGenerator;
    using Interfaces.Models;
    using Interfaces.Providers.Importer;
    using Models;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>The sales quote importer provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="ISalesQuoteImporterProviderBase"/>
    public abstract class SalesQuoteImporterProviderBase : ProviderBase, ISalesQuoteImporterProviderBase
    {
        /// <summary>The property flags.</summary>
        private const BindingFlags PropertyFlags = BindingFlags.Public | BindingFlags.Instance;

        // ReSharper disable InconsistentNaming, MultipleSpaces, UnusedMember.Local, StyleCop.SA1201
        ////private static readonly MethodInfo FilterByActiveMethodInfo            = typeof(BaseSearchExtensions).GetMethodInfo("FilterByActiveHashSet");
        private static readonly MethodInfo IEnumAnyMethodInfo                  = typeof(Enumerable).GetMethods().Single(x => x.Name == "Any"    && x.GetParameters().Length == 1);
        private static readonly MethodInfo IEnumAnyWithPredicateMethodInfo     = typeof(Enumerable).GetMethods().Single(x => x.Name == "Any"    && x.GetParameters().Length == 2);
        private static readonly MethodInfo IEnumWhereWithPredicateMethodInfo   = typeof(Enumerable).GetMethods().Single(x => x.Name == "Where"  && x.GetParameters().Length == 2 && x.GetParameters()[1].ParameterType.GenericTypeArguments.Length == 2);
        private static readonly MethodInfo IEnumSingleMethodInfo               = typeof(Enumerable).GetMethods().Single(x => x.Name == "Single" && x.GetParameters().Length == 1);
        private static readonly MethodInfo IEnumSingleWithPredicateMethodInfo  = typeof(Enumerable).GetMethods().Single(x => x.Name == "Single" && x.GetParameters().Length == 2);
        private static readonly MethodInfo IEnumCountMethodInfo                = typeof(Enumerable).GetMethods().Single(x => x.Name == "Count"  && x.GetParameters().Length == 1);
        private static readonly MethodInfo IEnumCountWithPredicateMethodInfo   = typeof(Enumerable).GetMethods().Single(x => x.Name == "Count"  && x.GetParameters().Length == 2);
        private static readonly MethodInfo IEnumCastMethodInfo                 = typeof(Enumerable).GetMethods().Single(x => x.Name == "Cast");
        private static readonly MethodInfo IEnumToListMethodInfo               = typeof(Enumerable).GetMethods().Single(x => x.Name == "ToList");
        private static readonly MethodInfo IQueryAnyMethodInfo                 = typeof(Queryable).GetMethods().Single(x => x.Name == "Any"    && x.GetParameters().Length == 1);
        private static readonly MethodInfo IQueryAnyWithPredicateMethodInfo    = typeof(Queryable).GetMethods().Single(x => x.Name == "Any"    && x.GetParameters().Length == 2);
        private static readonly MethodInfo IQueryWhereWithPredicateMethodInfo  = typeof(Queryable).GetMethods().Single(x => x.Name == "Where"  && x.GetParameters().Length == 2 && x.GetParameters()[1].ParameterType.GenericTypeArguments[0].GenericTypeArguments.Length == 2);
        private static readonly MethodInfo IQuerySelectWithPredicateMethodInfo = typeof(Queryable).GetMethods().Single(x => x.Name == "Select" && x.GetParameters().Length == 2 && x.GetParameters()[1].ParameterType.GenericTypeArguments[0].GenericTypeArguments.Length == 2);
        private static readonly MethodInfo IQueryFirstMethodInfo               = typeof(Queryable).GetMethods().Single(x => x.Name == "First"  && x.GetParameters().Length == 1);
        private static readonly MethodInfo IQueryFirstWithPredicateMethodInfo  = typeof(Queryable).GetMethods().Single(x => x.Name == "First"  && x.GetParameters().Length == 2);
        private static readonly MethodInfo IQuerySingleMethodInfo              = typeof(Queryable).GetMethods().Single(x => x.Name == "Single" && x.GetParameters().Length == 1);
        private static readonly MethodInfo IQuerySingleWithPredicateMethodInfo = typeof(Queryable).GetMethods().Single(x => x.Name == "Single" && x.GetParameters().Length == 2);
        private static readonly MethodInfo IQueryCountMethodInfo               = typeof(Queryable).GetMethods().Single(x => x.Name == "Count"  && x.GetParameters().Length == 1);
        private static readonly MethodInfo IQueryCountWithPredicateMethodInfo  = typeof(Queryable).GetMethods().Single(x => x.Name == "Count"  && x.GetParameters().Length == 2);
        private static readonly MethodInfo IQueryCastMethodInfo                = typeof(Queryable).GetMethods().Single(x => x.Name == "Cast");
        // ReSharper restore InconsistentNaming, MultipleSpaces, UnusedMember.Local, StyleCop.SA1201

        /// <summary>The invalid filename characters.</summary>
        private static readonly char[] InvalidFilenameChars = Path.GetInvalidFileNameChars();

        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.SalesQuoteImporter;

        /// <inheritdoc/>
        public abstract override bool HasValidConfiguration { get; }

        /// <inheritdoc/>
        public override bool HasDefaultProvider => true;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<ISalesQuoteModel>> ImportFileAsSalesQuoteAsync(
            string? contextProfileName,
            string fileName,
            int? mappingID = null,
            string mappingKey = null,
            string mappingName = null);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<object>> ExportSalesQuoteAsFileAsync(
            string? contextProfileName,
            int id,
            string format,
            int? mappingID = null,
            string mappingKey = null,
            string mappingName = null);

        /// <summary>Check column skip.</summary>
        /// <param name="config">The mapping configuration.</param>
        /// <returns>An int.</returns>
        protected static int CheckColumnSkip(MappingConfig config)
        {
            var columnSkip = 0;
            if (Contract.CheckValidID(config.ColumnSkip))
            {
                // ReSharper disable once PossibleInvalidOperationException
                columnSkip = config.ColumnSkip.Value;
            }
            return columnSkip;
        }

        /// <summary>Check row skip.</summary>
        /// <param name="config">The mapping configuration.</param>
        /// <returns>An int.</returns>
        protected static int CheckRowSkip(MappingConfig config)
        {
            var rowSkip = 0;
            if (Contract.CheckValidID(config.RowSkip))
            {
                // ReSharper disable once PossibleInvalidOperationException
                rowSkip = config.RowSkip.Value;
            }
            return rowSkip;
        }

        /// <summary>Default aggregate.</summary>
        /// <param name="array">    The array.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>A string.</returns>
        protected static string DefaultAggregate(string[] array, string delimiter = ".")
        {
            if (array?.All(string.IsNullOrWhiteSpace) != false)
            {
                return string.Empty;
            }
            return array
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .DefaultIfEmpty(string.Empty)
                .Aggregate((c, n) => c + delimiter + n);
        }

        /// <summary>Creates sales group for quote.</summary>
        /// <param name="salesQuoteID">      Identifier for the sales quote.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new sales group for quote.</returns>
        protected CEFActionResponse CreateSalesGroupForQuote(int salesQuoteID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var salesGroup = new DataModel.SalesGroup
            {
                Active = true,
                CreatedDate = DateExtensions.GenDateTime,
            };
            context.SalesGroups.Add(salesGroup);
            if (!context.SaveUnitOfWork(true))
            {
                return CEFAR.FailingCEFAR("ERROR! Something about creating and saving a Group for the Quote failed");
            }
            context.SalesQuotes.FilterByID(salesQuoteID).Single().SalesGroupAsMasterID = salesGroup.ID;
            return context.SaveUnitOfWork(true).BoolToCEFAR();
        }

        /// <summary>Ensures that root folder.</summary>
        /// <param name="root">The root.</param>
        /// <returns>The created/validated path to the folder.</returns>
        protected virtual string EnsureRootFolder(string root)
        {
            var modifiedRoot = Contract.RequiresValidKey(root);
            if (modifiedRoot.Contains("{CEF_RootPath}"))
            {
                modifiedRoot = modifiedRoot.Replace("{CEF_RootPath}", Globals.CEFRootPath);
            }
            if (modifiedRoot.EndsWith(@"\\"))
            {
                modifiedRoot = modifiedRoot.Replace(@"\\", @"\");
            }
            var folders = modifiedRoot.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            if (folders.Length == 0)
            {
                throw new ArgumentException("The root path must be a drive folder path or UNC path");
            }
            var builtPath = folders[0];
            if (folders.Length == 1)
            {
                // Only one segment
                return builtPath;
            }
            // Multiple segments
            foreach (var folder in folders.Skip(1))
            {
                builtPath = Path.Combine(builtPath + (!builtPath.EndsWith(@"\") ? @"\" : string.Empty), folder);
                if (!Directory.Exists(builtPath))
                {
                    Directory.CreateDirectory(builtPath);
                }
            }
            return builtPath;
        }

        /// <summary>Process the memory contents to entity.</summary>
        /// <typeparam name="TModel">Type of the entity model.</typeparam>
        /// <param name="coreModel">         The core entity model to assign properties to (relative to this as the root).</param>
        /// <param name="records">           The records.</param>
        /// <param name="config">            The mapping configuration.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse{TModel}.</returns>
        protected CEFActionResponse<TModel> ProcessCellDataToEntityModel<TModel>(
            TModel coreModel,
            IEnumerable<CellData[]> records,
            MappingConfig config,
            DateTime timestamp,
            string? contextProfileName)
            where TModel : class, IBaseModel, IHaveJsonAttributesBaseModel
        {
            ////var count = 0;
            var coreModelGuid = Guid.NewGuid();
            foreach (var record in records.Where(x => !x.Select(y => y.Value).All(string.IsNullOrWhiteSpace)))
            {
                // The guid acts like a record ID for the purposes of recognizing this record. It will go into the
                // SerializableAttributes of any object so it's easy to look it up while we import. Before saving all
                // records imported, wipe it (we can do this when we resolve every record's attributes at the end of
                // this function
                MapRecordCellDataToEntityModelProperties(
                    coreModel,
                    record,
                    config,
                    timestamp,
                    (core: coreModelGuid, record: Guid.NewGuid()),
                    contextProfileName);
                ////if (++count > 1) { break; }
            }
            // TODO@JTG: Process JsonAttributes on core and records using the workflow so they are resolved before save
            return coreModel.WrapInPassingCEFAR();
        }

        /// <summary>Loads mapping configuration.</summary>
        /// <param name="root">              The root.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="mappingID">         Identifier for the mapping.</param>
        /// <param name="mappingKey">        The mapping key.</param>
        /// <param name="mappingName">       Name of the mapping.</param>
        /// <returns>The mapping configuration.</returns>
        protected virtual CEFActionResponse<MappingConfig> LoadMappingConfig(
            string root,
            string? contextProfileName,
            int? mappingID = null,
            string mappingKey = null,
            string mappingName = null)
        {
            if (Contract.CheckValidID(mappingID) || Contract.CheckAnyValidKey(mappingKey, mappingName))
            {
                var result = TryGetMappingFromDb(mappingID, mappingKey, mappingName, contextProfileName);
                if (result.ActionSucceeded)
                {
                    return result;
                }
            }
            // TODO: Remove the following block as deprecated (leaving as a fallback option for now)
            var fileName = Contract.CheckValidKey(mappingKey) ? mappingKey + ".json" : "mapping.json";
            // SCS0018: Path traversal (handler) START
            if (fileName.IndexOfAny(InvalidFilenameChars) >= 0)
            {
                return CEFAR.FailingCEFAR<MappingConfig>("ERROR! Invalid characters in the file path");
            }
            // SCS0018: Path traversal (handler) END
            var fullPath = Path.Combine(EnsureRootFolder(root), fileName);
            if (!File.Exists(fullPath))
            {
                return CEFAR.FailingCEFAR<MappingConfig>($"ERROR! The file \"{fileName}\" does not exist at the expected location");
            }
            string configRaw;
#pragma warning disable SCS0018 // Path traversal (handled)
            using (var file = File.OpenText(fullPath))
#pragma warning restore SCS0018 // Path traversal (handled)
            {
                configRaw = file.ReadToEnd();
            }
            if (!Contract.CheckValidKey(configRaw))
            {
                return CEFAR.FailingCEFAR<MappingConfig>("ERROR! Mapping config file was empty.");
            }
            var config = JsonConvert.DeserializeObject<MappingConfig>(configRaw);
            if (config == null)
            {
                return CEFAR.FailingCEFAR<MappingConfig>("ERROR! Mapping config file could not deserialize into the object.");
            }
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (!Contract.CheckValidKey(config.ConfigName))
            {
                return CEFAR.FailingCEFAR<MappingConfig>("ERROR! Mapping config file configuration name was blank. "
                    + "This most likely means the file is invalid or didn't parse correctly.");
            }
            return config.WrapInPassingCEFAR();
        }

        /// <summary>Try get mapping from database.</summary>
        /// <param name="mappingID">         Identifier for the mapping.</param>
        /// <param name="mappingKey">        The mapping key.</param>
        /// <param name="mappingName">       Name of the mapping.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse{MappingConfig}.</returns>
        private static CEFActionResponse<MappingConfig> TryGetMappingFromDb(int? mappingID, string mappingKey, string mappingName, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            // Try ID first
            var validatedMappingID = Contract.CheckValidID(mappingID)
                ? context.ImportExportMappings
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByID(mappingID)
                    .Select(x => (int?)x.ID)
                    .SingleOrDefault()
                : null;
            if (Contract.CheckInvalidID(validatedMappingID))
            {
                // Fallback to CustomKey
                validatedMappingID = Contract.CheckValidKey(mappingKey)
                    ? context.ImportExportMappings
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterByCustomKey(mappingKey, true)
                        .Select(x => (int?)x.ID)
                        .SingleOrDefault()
                    : null;
            }
            if (Contract.CheckInvalidID(validatedMappingID))
            {
                // Fallback to Name
                validatedMappingID = Contract.CheckValidKey(mappingName)
                    ? context.ImportExportMappings
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterByName(mappingName, true)
                        .Select(x => (int?)x.ID)
                        .SingleOrDefault()
                    : null;
            }
            if (Contract.CheckInvalidID(validatedMappingID))
            {
                return CEFAR.FailingCEFAR<MappingConfig>(
                    "WARNING! Unable to locate a database entry against the provide mapping infos");
            }
            // We have a valid ID
            var mappingJson = context.ImportExportMappings
                .FilterByID(validatedMappingID.Value)
                .Select(x => x.MappingJson)
                .Single();
            var config = JsonConvert.DeserializeObject<MappingConfig>(mappingJson);
            if (config == null)
            {
                return CEFAR.FailingCEFAR<MappingConfig>(
                    "ERROR! Mapping config file could not deserialize into the object.");
            }
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (string.IsNullOrWhiteSpace(config.ConfigName))
            {
                return CEFAR.FailingCEFAR<MappingConfig>("ERROR! Mapping config file configuration name was blank. "
                    + "This most likely means the file is invalid or didn't parse correctly.");
            }
            return config.WrapInPassingCEFAR();
        }

        /// <summary>Gets method information.</summary>
        /// <param name="model">The model.</param>
        /// <param name="name"> The name.</param>
        /// <returns>The method information.</returns>
        private static MethodInfo GetMethodInfo(object model, string name)
        {
            Contract.RequiresNotNull(model);
            return model.GetType().GetMethod(name);
        }

        /// <summary>Gets property information.</summary>
        /// <param name="model">The model.</param>
        /// <param name="name"> The name.</param>
        /// <returns>The property information.</returns>
        private static PropertyInfo GetPropertyInfo(IBaseModel model, string name)
        {
            Contract.RequiresNotNull(model);
            return model.GetType().GetProperty(name);
        }

        /// <summary>Gets property value.</summary>
        /// <param name="model">The model.</param>
        /// <param name="name"> The name.</param>
        /// <returns>The property value.</returns>
        private static object GetPropertyValue(object model, string name)
        {
            Contract.RequiresNotNull(model);
            return model.GetType().GetProperty(name)?.GetValue(model);
        }

        ////private static void SetPropertyOnObjectIfPresent<TValue>(object model, string name, TValue value)
        ////{
        ////    var prop = model.GetType().GetProperty(name, PropertyFlags);
        ////    if (prop == null) { return; }
        ////    try
        ////    {
        ////        prop.SetValue(model, value);
        ////    }
        ////    catch { /* Do Nothing */ }
        ////}

        private static bool MapPropertyInner(
            IReadOnlyList<IBaseModel> models,
            CellData cellData,
            IEnumerable<string> ignore,
            MapToEntityProperties assignment,
            int depthLevel)
        {
            if (string.IsNullOrWhiteSpace(cellData.Value)
                || assignment.To.FirstOrDefault() == "Ignore"
                || ignore.Any(i => cellData.Value.Equals(i, StringComparison.InvariantCultureIgnoreCase)))
            {
                // Invalid, Empty or not supposed to be mapped
                return true;
            }
            var properties = models[depthLevel].GetType().GetProperties(PropertyFlags).ToList();
            var mapped = false;
            // TODO@JTG: Remove this reflection call, let the ImportAssigner take care of it
            var query = properties.Where(x => x.Name == nameof(IHaveJsonAttributesBaseModel.JsonAttributes)
                || assignment.To.Any(y => x.Name.Equals(y, StringComparison.InvariantCultureIgnoreCase)));
            foreach (var property in query)
            {
                switch (property.Name)
                {
                    case nameof(IHaveJsonAttributesBaseModel.JsonAttributes):
                    {
                        if (ImportAssigners.SetAttribute(models[depthLevel], DefaultAggregate(cellData.Header), cellData.Value))
                        {
                            mapped = true;
                        }
                        break;
                    }
                    default:
                    {
                        if (ImportAssigners.SetProperty(models[depthLevel], property.Name, cellData.Value))
                        {
                            mapped = true;
                            continue;
                        }
                        goto case nameof(IHaveJsonAttributesBaseModel.JsonAttributes);
                    }
                }
            }
            return mapped;
        }

        /// <summary>Determine unhandled mapping.</summary>
        /// <param name="config">   The mapping configuration.</param>
        /// <param name="unmapped">The unmapped handling.</param>
        private static void DetermineUnhandledMapping(
            MappingConfig config,
            out (string model, string[] to, UnhandledMappingModes mode) unmapped)
        {
            unmapped = (model: config.MapUnmappedTo.Entity,
                        to: config.MapUnmappedTo.To,
                        mode: UnhandledMappingModes.JsonAttributes);
            if (!Enum.TryParse(unmapped.to.FirstOrDefault(), out unmapped.mode))
            {
                unmapped.mode = UnhandledMappingModes.JsonAttributes;
            }
        }

        /// <summary>Map record cell data to entity properties.</summary>
        /// <param name="coreModel">         The core entity model to assign properties to (relative to this as the root).</param>
        /// <param name="record">            The record.</param>
        /// <param name="config">            The mapping configuration.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="coreAndRecordGuid"> Unique identifier for the core entity and this record entity.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        private void MapRecordCellDataToEntityModelProperties(
            IBaseModel coreModel,
            IEnumerable<CellData> record,
            MappingConfig config,
            DateTime timestamp,
            (Guid core, Guid record) coreAndRecordGuid,
            string? contextProfileName)
        {
            DetermineUnhandledMapping(config, out var unmapped);
            foreach (var cellData in record)
            {
                if (!Contract.CheckAnyValidKey(cellData.Header))
                {
                    // This is likely a blank column, ignore it
                    continue;
                }
                var models = new List<IBaseModel> { coreModel };
                var mapping = config.Mappings
                    .Single(x => x.HeaderOccurrence == cellData.HeaderOccurrence
                              && DefaultAggregate(x.Header, ">") == DefaultAggregate(cellData.Header, ">"));
                MapProperty(models, cellData, mapping, unmapped, timestamp, coreAndRecordGuid, 0, null, contextProfileName);
            }
        }

        /// <summary>Map property.</summary>
        /// <param name="models">            The models to save after being resolved against entities.</param>
        /// <param name="cellData">          Information describing the cell.</param>
        /// <param name="mapping">           The mapping.</param>
        /// <param name="unmapped">          The unmapped handling config.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="coreAndRecordGuid"> Unique identifier for the core entity and this record entity.</param>
        /// <param name="depthLevel">        The sub points.</param>
        /// <param name="overrideAssignment">The override assignment.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        private void MapProperty(
            List<IBaseModel> models,
            CellData cellData,
            MapFromIncomingProperty mapping,
            (string model, string[] to, UnhandledMappingModes mode) unmapped,
            DateTime timestamp,
            (Guid core, Guid record) coreAndRecordGuid,
            int depthLevel,
            MapToEntityProperties overrideAssignment,
            string? contextProfileName)
        {
            if (overrideAssignment != null)
            {
                MapAssignmentOrGoDeeper(
                    models,
                    cellData,
                    mapping,
                    unmapped,
                    timestamp,
                    coreAndRecordGuid,
                    depthLevel,
                    overrideAssignment,
                    contextProfileName);
                return;
            }
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var assignment in mapping.Assignments.Where(x => x.To.All(y => !y.Equals("Ignore"))))
            {
                MapAssignmentOrGoDeeper(
                    models,
                    cellData,
                    mapping,
                    unmapped,
                    timestamp,
                    coreAndRecordGuid,
                    depthLevel,
                    assignment,
                    contextProfileName);
            }
        }

        /// <summary>Map assignment or go deeper.</summary>
        /// <param name="models">            The models.</param>
        /// <param name="cellData">          Information describing the cell.</param>
        /// <param name="mapping">           The mapping.</param>
        /// <param name="unmapped">          The unmapped.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="coreAndRecordGuid"> Unique identifier for the core and record.</param>
        /// <param name="depthLevel">        The depth level.</param>
        /// <param name="assignment">        The assignment.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        private void MapAssignmentOrGoDeeper(
            List<IBaseModel> models,
            CellData cellData,
            MapFromIncomingProperty mapping,
            (string model, string[] to, UnhandledMappingModes mode) unmapped,
            DateTime timestamp,
            (Guid core, Guid record) coreAndRecordGuid,
            int depthLevel,
            MapToEntityProperties assignment,
            string? contextProfileName)
        {
            var mapped = false;
            var modelPathPoints = assignment.Entity.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            var whichModel = models[depthLevel];
            if (modelPathPoints.Length == 1
                && (modelPathPoints[0] == whichModel.GetType().Name
                    || /*HelpFunctions.SwapToModelType(*/modelPathPoints[0] /*)*/ + "Model" == whichModel.GetType().Name
                    || GetPropertyInfo(whichModel, modelPathPoints[0]) != null))
            {
                // Only one path point and it's the entity we're manipulating now
                mapped = MapPropertyInner(models, cellData, mapping.Ignore, assignment, depthLevel);
            }
            else if (depthLevel > 0 && modelPathPoints.Length == depthLevel + 1)
            {
                // We're at the last level, so just looking at an assignment here
                // Is not a collection and is not an entity kind, just deal with this property (if we can)
                mapped = MapPropertyInner(models, cellData, mapping.Ignore, assignment, depthLevel);
            }
            else if (depthLevel < modelPathPoints.Length
                     && (modelPathPoints[depthLevel] == models[depthLevel].GetType().Name
                         || /*HelpFunctions.SwapToModelType(*/modelPathPoints[depthLevel] /*)*/ + "Model" == models[depthLevel].GetType().Name))
            {
                // More than one path point, dig in a level
                var current = modelPathPoints[depthLevel + 1]; // Must be +1
                var currentInfo = GetPropertyInfo(models[depthLevel], current); // Must be +0 (to read the +1)
                if (currentInfo.PropertyType.Name == typeof(ICollection<>).Name
                    || currentInfo.PropertyType.Name == typeof(List<>).Name)
                {
                    // Is a collection, so we want to manipulate the collection using upserts
                    ProcessCollectionProperty(
                        models,
                        cellData,
                        mapping,
                        unmapped,
                        timestamp,
                        coreAndRecordGuid,
                        currentInfo,
                        current,
                        depthLevel,
                        assignment,
                        contextProfileName);
                }
            }
            else if (depthLevel > 0)
            {
                // This is somewhere in the middle of the path points
                var next = modelPathPoints[depthLevel + 1];
                var nextInfo = GetPropertyInfo(models[depthLevel], next); // Must be +0 (to read the +1)
                if (nextInfo.PropertyType.GetInterfaces().Any(x => x.Name == nameof(IBaseModel)))
                {
                    // Is not a collection, but is an entity kind, deal with this property and it's inners
                    ProcessEntityModelProperty(
                        models,
                        cellData,
                        mapping,
                        unmapped,
                        timestamp,
                        coreAndRecordGuid,
                        next,
                        nextInfo,
                        depthLevel,
                        assignment,
                        contextProfileName);
                }
            }
            if (mapped || unmapped.mode == UnhandledMappingModes.Ignore)
            {
                return;
            }
            // Map the un-mapped to using the unhandled mapping setting of JsonAttributes
            ImportAssigners.SetAttribute(models[0], DefaultAggregate(mapping.Header), cellData.Value);
        }

        /// <summary>Process the collection property.</summary>
        /// <param name="models">                     The models.</param>
        /// <param name="cellData">                   Information describing the cell.</param>
        /// <param name="mapping">                    The mapping.</param>
        /// <param name="unmapped">                   The unmapped.</param>
        /// <param name="timestamp">                  The timestamp Date/Time.</param>
        /// <param name="coreAndRecordGuid">          Unique identifier for the core and record.</param>
        /// <param name="propertyInfoOfPropertyToSet">Set the property information of property to belongs to.</param>
        /// <param name="nameOfPropertyToSet">        Set the name of property to belongs to.</param>
        /// <param name="depthLevel">                 The depth level.</param>
        /// <param name="assignment">                 The assignment.</param>
        /// <param name="contextProfileName">         Name of the context profile.</param>
        private void ProcessCollectionProperty(
            List<IBaseModel> models,
            CellData cellData,
            MapFromIncomingProperty mapping,
            (string model, string[] to, UnhandledMappingModes mode) unmapped,
            DateTime timestamp,
            (Guid core, Guid record) coreAndRecordGuid,
            PropertyInfo propertyInfoOfPropertyToSet,
            string nameOfPropertyToSet,
            int depthLevel,
            MapToEntityProperties assignment,
            string? contextProfileName)
        {
            var currentCollectionValue = ImportAssigners.GetProperty(models[depthLevel], nameOfPropertyToSet);
            ////var currentCollectionValue = GetPropertyValue(models[depthLevel], nameOfPropertyToSet);
            var currentCollectionInnerConcreteType = ImportAssigners.GetInnerCollectionTypeOfPropertyConcrete(
                models[depthLevel],
                nameOfPropertyToSet);
            ////var currentCollectionInnerInterfaceType = ImportAssigners.GetInnerCollectionTypeOfPropertyInterface(
            ////    models[depthLevel],
            ////    nameOfPropertyToSet);
            ////var currentCollectionInnerType = propertyInfoOfPropertyToSet.PropertyType.GenericTypeArguments[0];
            var isNewOrEmptyCollection = currentCollectionValue == null || (int)GetPropertyValue(currentCollectionValue, "Count") <= 0;
            var innerCollection = currentCollectionValue
                // TODO@JTG: Make ImportAssigners make this object and pre-populate minimum required data
                ?? Activator.CreateInstance(propertyInfoOfPropertyToSet.PropertyType);
            IBaseModel innerRecord = null;
            var isNewRecordForCollection = isNewOrEmptyCollection;
            if (!isNewOrEmptyCollection)
            {
                var listCasted = (IEnumerable<IBaseModel>)IEnumCastMethodInfo.MakeGenericMethod(typeof(IBaseModel))
                    .Invoke(null, new[] { innerCollection });
                innerRecord = (IBaseModel)ImportAssigners.FindFirstActiveByImportGuidInList(listCasted, coreAndRecordGuid.record);
                if (innerRecord == null)
                {
                    isNewRecordForCollection = true;
                }
            }
            if (isNewRecordForCollection)
            {
                // TODO@JTG: Make ImportAssigners make this object and pre-populate minimum required data
                innerRecord = (IBaseModel)RegistryLoader.ContainerInstance.GetInstance(currentCollectionInnerConcreteType);
                ////innerRecord = (IBaseModel)Activator.CreateInstance(currentCollectionInnerType);
                innerRecord.Active = true;
                innerRecord.CreatedDate = timestamp;
                var sad = new SerializableAttributesDictionary
                {
                    ["ImportGuid"] = new SerializableAttributeObject
                    {
                        Key = "ImportGuid",
                        Value = coreAndRecordGuid.record.ToString(),
                    },
                };
                innerRecord.JsonAttributes = sad.SerializeAttributesDictionary();
                // We can't add directly due to all the list, etc. classes requiring a type argument
                // TODO@JTG: Make this call using ImportAssigners instead
                var add = GetMethodInfo(innerCollection, "Add");
                add.Invoke(innerCollection, new object[] { innerRecord });
            }
            else
            {
                ////innerRecord = (IBaseModel)ImportAssigners.FindFirstActiveByImportGuidInDbSet(Context, currentCollectionInnerConcreteType, coreAndRecordGuid.record);
                ////var singleMethodInfoT = IEnumSingleWithPredicateMethodInfo.MakeGenericMethod(typeof(IBaseModel));
                ////innerRecord = (IBaseModel)singleMethodInfoT.Invoke(
                ////    null,
                ////    new []
                ////    {
                ////        innerCollection,
                ////        new Func<object, bool>(x => ((IBaseModel)x).Active
                ////                                 && ((IBaseModel)x).SerializableAttributes["ImportGuid"].Value
                ////                                    == coreAndRecordGuid.record.ToString())
                ////    });
                innerRecord.UpdatedDate = timestamp;
            }
            // TODO@JTG: Should this only be on new collection?
            ImportAssigners.SetProperty(models[depthLevel], nameOfPropertyToSet, innerCollection);
            ////SetPropertyOnObjectIfPresent(models[depthLevel], nameOfPropertyToSet, innerCollection);
            // Go into it (Note: Don't call this before assigning/adding the collection or you will lose the lookups
            // to find the same record)
            models.Add(innerRecord);
            MapProperty(models, cellData, mapping, unmapped, timestamp, coreAndRecordGuid, depthLevel + 1, assignment, contextProfileName);
        }

        /// <summary>Process the entity model property.</summary>
        /// <param name="models">            The models.</param>
        /// <param name="cellData">          Information describing the cell.</param>
        /// <param name="mapping">           The mapping.</param>
        /// <param name="unmapped">          The unmapped.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="coreAndRecordGuid"> Unique identifier for the core and record.</param>
        /// <param name="current">           The current.</param>
        /// <param name="currentInfo">       Information describing the current.</param>
        /// <param name="depthLevel">        The depth level.</param>
        /// <param name="assignment">        The assignment.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        private void ProcessEntityModelProperty(
            List<IBaseModel> models,
            CellData cellData,
            MapFromIncomingProperty mapping,
            (string model, string[] to, UnhandledMappingModes mode) unmapped,
            DateTime timestamp,
            (Guid core, Guid record) coreAndRecordGuid,
            string current,
            PropertyInfo currentInfo,
            int depthLevel,
            MapToEntityProperties assignment,
            string? contextProfileName)
        {
            var relatedModel = (IBaseModel)ImportAssigners.GetProperty(models[depthLevel], current);
            // TODO@JTG: check array index number from header occurrence?
            // TODO@JTG: Assign the specific model in the collection by lookups?
            var isNew = relatedModel == null;
            if (isNew)
            {
                // TODO@JTG: Make ImportAssigners make this object and pre-populate minimum required data
                relatedModel = (IBaseModel)Activator.CreateInstance(currentInfo.PropertyType);
                relatedModel.Active = true;
                relatedModel.CreatedDate = timestamp;
                // Assign it freshly now
                ImportAssigners.SetProperty(models[depthLevel], current, relatedModel);
                ////SetPropertyOnObjectIfPresent(models[depthLevel], current, relatedModel);
                // Assign required relates to the first active record so we don't get DB errors.
                // Overrides may occur later.
                if (relatedModel is IHaveATypeBaseModel relatedWithType)
                {
                    var relatedType = relatedModel.GetType()
                        .GetInterfaces()
                        .First(x => x.Name == typeof(IHaveATypeBaseModel<>).Name)
                        .GenericTypeArguments[0];
                    using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                    relatedWithType.TypeID = ImportAssigners.FindFirstActiveIDInDbSet(context, relatedType)
                        ?? throw new InvalidOperationException(
                            $"ERROR: There are no active {relatedType.Name} records in the database, please add"
                            + " one before running this import.");
                }
                if (relatedModel is IHaveAStatusBaseModel relatedWithStatus)
                {
                    var relatedStatus = relatedModel.GetType()
                        .GetInterfaces()
                        .First(x => x.Name == typeof(IHaveAStatusBaseModel<>).Name)
                        .GenericTypeArguments[0];
                    using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                    relatedWithStatus.StatusID = ImportAssigners.FindFirstActiveIDInDbSet(context, relatedStatus)
                        ?? throw new InvalidOperationException(
                            $"ERROR: There are no active {relatedStatus.Name} records in the database, please add"
                            + " one before running this import.");
                }
                // ReSharper disable once SuspiciousTypeConversion.Global
                if (relatedModel is IHaveAStateBaseModel relatedWithState)
                {
                    var relatedState = relatedModel.GetType()
                        .GetInterfaces()
                        .First(x => x.Name == typeof(IHaveAStateBaseModel<>).Name)
                        .GenericTypeArguments[0];
                    using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                    relatedWithState.StateID = ImportAssigners.FindFirstActiveIDInDbSet(context, relatedState)
                        ?? throw new InvalidOperationException(
                            $"ERROR: There are no active {relatedState.Name} records in the database, please add"
                            + " one before running this import.");
                }
            }
            else
            {
                // No need to assign, just update the timestamp
                relatedModel.UpdatedDate = timestamp;
            }
            // Map the property on this new object
            models.Add(relatedModel);
            MapProperty(models, cellData, mapping, unmapped, timestamp, coreAndRecordGuid, depthLevel + 1, assignment, contextProfileName);
        }
    }
}
