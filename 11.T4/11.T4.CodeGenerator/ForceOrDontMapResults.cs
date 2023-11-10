// <copyright file="ForceOrDontMapResults.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the force or don't map results class</summary>
// ReSharper disable InconsistentNaming, MemberCanBePrivate.Global, UnusedAutoPropertyAccessor.Global, UnusedMember.Global
namespace CodeGenerator
{
    using System.Linq;
    using System.Reflection;
    using Clarity.Ecommerce.DataModel;

    /// <summary>A force or don't map results.</summary>
    public class ForceOrDontMapResults
    {
        /// <summary>Initializes a new instance of the <seealso cref="ForceOrDontMapResults"/> class.</summary>
        /// <param name="property">The property.</param>
        public ForceOrDontMapResults(MemberInfo property)
        {
            DontMapInEver = property.GetCustomAttributes<DontMapInEverAttribute>().Any();
            DontMapOutEver = property.GetCustomAttributes<DontMapOutEverAttribute>().Any();
            DontMapInWithAssociateWorkflows = property.GetCustomAttributes<DontMapInWithAssociateWorkflowsAttribute>().Any();
            if (DontMapOutEver)
            {
                ForceMapOutWithList = ForceMapOutWithLite = false;
                DontMapOutWithList = DontMapOutWithLite = true;
            }
            else
            {
                ForceMapOutWithList = property.GetCustomAttributes<ForceMapOutWithListingAttribute>().Any();
                ForceMapOutWithLite = ForceMapOutWithList
                    || property.GetCustomAttributes<ForceMapOutWithLiteAttribute>().Any();
                DontMapOutWithLite = !ForceMapOutWithLite
                    && (property.GetCustomAttributes<DontMapOutWithLiteAttribute>().Any()
                        || property.GetCustomAttributes<OnlyMapOutFlattenedValuesInsteadOfObjectWithLiteAttribute>().Any());
                DontMapOutWithList = !ForceMapOutWithList
                    && (DontMapOutWithLite
                        || property.GetCustomAttributes<DontMapOutWithListingAttribute>().Any());
                OnLiteOnlyMapFlattenedValues = property.GetCustomAttributes<OnlyMapOutFlattenedValuesInsteadOfObjectWithLiteAttribute>().Any();
            }
        }

        /// <summary>Gets a value indicating whether the map list should be forced.</summary>
        /// <value>true if force map list, false if not.</value>
        public bool ForceMapOutWithList { get; }

        /// <summary>Gets a value indicating whether the map lite should be forced.</summary>
        /// <value>true if force map lite, false if not.</value>
        public bool ForceMapOutWithLite { get; }

        /// <summary>Gets a value indicating whether the dont map in with associate workflows.</summary>
        /// <value>True if dont map in with associate workflows, false if not.</value>
        public bool DontMapInWithAssociateWorkflows { get; }

        /// <summary>Gets a value indicating whether the dont map ever.</summary>
        /// <value>true if dont map ever, false if not.</value>
        public bool DontMapOutEver { get; }

        /// <summary>Gets a value indicating whether the dont map lite.</summary>
        /// <value>true if dont map lite, false if not.</value>
        public bool DontMapOutWithLite { get; }

        /// <summary>Gets a value indicating whether the on lite only map flattened values.</summary>
        /// <value>True if on lite only map flattened values, false if not.</value>
        public bool OnLiteOnlyMapFlattenedValues { get; }

        /// <summary>Gets a value indicating whether the dont map list.</summary>
        /// <value>true if dont map list, false if not.</value>
        public bool DontMapOutWithList { get; }

        /// <summary>Gets a value indicating whether the dont assign.</summary>
        /// <value>true if dont assign, false if not.</value>
        public bool DontMapInEver { get; }
    }
}
