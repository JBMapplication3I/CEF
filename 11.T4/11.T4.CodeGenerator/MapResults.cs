// <copyright file="MapResults.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the map results class</summary>
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global, InconsistentNaming, MemberCanBePrivate.Global, MissingXmlDoc, UnusedAutoPropertyAccessor.Global, UnusedMember.Global
namespace CodeGenerator
{
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>A map results.</summary>
    public class MapResults
    {
        /// <summary>Initializes a new instance of the <see cref="MapResults"/> class.</summary>
        /// <param name="groupBy">Describes who group this MapResults.</param>
        public MapResults(string groupBy)
        {
            GroupBy = groupBy;
        }

        /// <summary>Gets or sets who group this MapResults.</summary>
        /// <value>Describes who group this MapResults.</value>
        public string GroupBy { get; set; }

        /// <summary>Gets or sets a value indicating whether the do safe anon cast.</summary>
        /// <value>True if do safe anon cast, false if not.</value>
        public bool DoSafeAnonCast { get; set; } = false;

        /// <summary>Gets or sets to model maps new in SQL simple full.</summary>
        /// <value>to model maps new in SQL simple full.</value>
        public List<string> ToModelMapsNewInSQLSimpleFull { get; set; } = new();

        /// <summary>Gets or sets to model maps new in SQL simple lite.</summary>
        /// <value>to model maps new in SQL simple lite.</value>
        public List<string> ToModelMapsNewInSQLSimpleLite { get; set; } = new();

        /// <summary>Gets or sets a list of to model maps new in SQL simples.</summary>
        /// <value>A List of to model maps new in SQL simples.</value>
        public List<string> ToModelMapsNewInSQLSimpleList { get; set; } = new();

        /// <summary>Gets or sets to model maps new in model simple full.</summary>
        /// <value>to model maps new in model simple full.</value>
        public List<string> ToModelMapsNewInModelSimpleFull { get; set; } = new();

        /// <summary>Gets or sets to model maps new in model simple lite.</summary>
        /// <value>to model maps new in model simple lite.</value>
        public List<string> ToModelMapsNewInModelSimpleLite { get; set; } = new();

        /// <summary>Gets or sets a list of to model maps new in model simples.</summary>
        /// <value>A List of to model maps new in model simples.</value>
        public List<string> ToModelMapsNewInModelSimpleList { get; set; } = new();

        /// <summary>Gets or sets to model maps new in SQL related full.</summary>
        /// <value>to model maps new in SQL related full.</value>
        public List<string> ToModelMapsNewInSQLRelatedFull { get; set; } = new();

        /// <summary>Gets or sets to model maps new in SQL related lite.</summary>
        /// <value>to model maps new in SQL related lite.</value>
        public List<string> ToModelMapsNewInSQLRelatedLite { get; set; } = new();

        /// <summary>Gets or sets a list of to model maps new in SQL related.</summary>
        /// <value>A List of to model maps new in SQL related.</value>
        public List<string> ToModelMapsNewInSQLRelatedList { get; set; } = new();

        /// <summary>Gets or sets to model maps new in model related full.</summary>
        /// <value>to model maps new in model related full.</value>
        public List<string> ToModelMapsNewInModelRelatedFull { get; set; } = new();

        /// <summary>Gets or sets to model maps new in model related lite.</summary>
        /// <value>to model maps new in model related lite.</value>
        public List<string> ToModelMapsNewInModelRelatedLite { get; set; } = new();

        /// <summary>Gets or sets a list of to model maps new in model related.</summary>
        /// <value>A List of to model maps new in model related.</value>
        public List<string> ToModelMapsNewInModelRelatedList { get; set; } = new();

        /// <summary>Gets or sets to model maps new in SQL associated full.</summary>
        /// <value>to model maps new in SQL associated full.</value>
        public List<string> ToModelMapsNewInSQLAssociatedFull { get; set; } = new();

        /// <summary>Gets or sets to model maps new in SQL associated lite.</summary>
        /// <value>to model maps new in SQL associated lite.</value>
        public List<string> ToModelMapsNewInSQLAssociatedLite { get; set; } = new();

        /// <summary>Gets or sets a list of to model maps new in SQL associated.</summary>
        /// <value>A List of to model maps new in SQL associated.</value>
        public List<string> ToModelMapsNewInSQLAssociatedList { get; set; } = new();

        /// <summary>Gets or sets to model maps new in model associated full.</summary>
        /// <value>to model maps new in model associated full.</value>
        public List<string> ToModelMapsNewInModelAssociatedFull { get; set; } = new();

        /// <summary>Gets or sets to model maps new in model associated lite.</summary>
        /// <value>to model maps new in model associated lite.</value>
        public List<string> ToModelMapsNewInModelAssociatedLite { get; set; } = new();

        /// <summary>Gets or sets a list of to model maps new in model associated.</summary>
        /// <value>A List of to model maps new in model associated.</value>
        public List<string> ToModelMapsNewInModelAssociatedList { get; set; } = new();

        /// <summary>Gets or sets to model maps new 2 full.</summary>
        /// <value>to model maps new 2 full.</value>
        public List<string> ToModelMapsNew2Full { get; set; } = new();

        /// <summary>Gets or sets to model maps new 1 lite.</summary>
        /// <value>to model maps new 1 lite.</value>
        public List<string> ToModelMapsNew1Lite { get; set; } = new();

        /// <summary>Gets or sets to model maps new 2 lite.</summary>
        /// <value>to model maps new 2 lite.</value>
        public List<string> ToModelMapsNew2Lite { get; set; } = new();

        /// <summary>Gets or sets a list of to model maps new 1s.</summary>
        /// <value>A List of to model maps new 1s.</value>
        public List<string> ToModelMapsNew1List { get; set; } = new();

        /// <summary>Gets or sets a list of to model maps new 2s.</summary>
        /// <value>A List of to model maps new 2s.</value>
        public List<string> ToModelMapsNew2List { get; set; } = new();

        /// <summary>Gets or sets options for controlling the operation.</summary>
        /// <value>The parameters.</value>
        public List<string> Parameters { get; set; } = new();

        /// <summary>Gets or sets to entity maps.</summary>
        /// <value>to entity maps.</value>
        public List<string> ToEntityMaps { get; set; } = new();

        /// <summary>Gets or sets to model maps full.</summary>
        /// <value>to model maps full.</value>
        public List<string> ToModelMapsFull { get; set; } = new();

        /// <summary>Gets or sets to model maps lite.</summary>
        /// <value>to model maps lite.</value>
        public List<string> ToModelMapsLite { get; set; } = new();

        /// <summary>Gets or sets a list of to model maps.</summary>
        /// <value>A List of to model maps.</value>
        public List<string> ToModelMapsList { get; set; } = new();

        /// <summary>Gets or sets as expression maps full.</summary>
        /// <value>as expression maps full.</value>
        public List<string> AsExprMapsFull { get; set; } = new();

        /// <summary>Gets or sets as expression maps lite.</summary>
        /// <value>as expression maps lite.</value>
        public List<string> AsExprMapsLite { get; set; } = new();

        /// <summary>Gets or sets a list of as expression maps.</summary>
        /// <value>A List of as expression maps.</value>
        public List<string> AsExprMapsList { get; set; } = new();

        /// <summary>Gets or sets options for controlling the relate functions shared normal.</summary>
        /// <value>Options that control the relate functions shared normal.</value>
        public List<string[]> RelateFunctionsSharedNormal_Params { get; set; } = new();

        /// <summary>Gets or sets options for controlling the relate functions shared special.</summary>
        /// <value>Options that control the relate functions shared special.</value>
        public List<string[]> RelateFunctionsSharedSpecial_Params { get; set; } = new();

        /// <summary>Gets or sets options for controlling the relate functions non shared normal.</summary>
        /// <value>Options that control the relate functions non shared normal.</value>
        public List<string[]> RelateFunctionsNonSharedNormal_Params { get; set; } = new();

        /// <summary>Gets or sets options for controlling the relate functions non shared special.</summary>
        /// <value>Options that control the relate functions non shared special.</value>
        public List<string[]> RelateFunctionsNonSharedSpecial_Params { get; set; } = new();

        /// <summary>Gets or sets the processed properties.</summary>
        /// <value>The processed properties.</value>
        public List<PropertyInfo> ProcessedProperties { get; set; } = new();
    }
}
