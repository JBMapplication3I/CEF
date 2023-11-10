// <copyright file="DbContextConfiguration.cs" company="Richard Lawley">
// Copyright (c) 2016-2022 Richard Lawley. All rights reserved.
// </copyright>
// <summary>Implements the database context configuration class</summary>

namespace RichardLawley.EF.AttributeConfig
{
    using System;
    using System.Data.Entity;
    using System.Reflection;

    /// <summary>A database context configuration.</summary>
    public static class DbContextConfiguration
    {
        /// <summary>(This method is obsolete) applies the configuration attributes.</summary>
        /// <param name="modelBuilder">The model builder.</param>
        /// <param name="assemblies">  A variable-length parameters list containing assemblies.</param>
        [Obsolete("Use AttributeConventions instead")]
        public static void ApplyConfigurationAttributes(DbModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            throw new InvalidOperationException(
                "This method is no longer supported. Instead, use modelBuilder.Conventions.Add(new DecimalPrecisionAttributeConvention()) and other equivalents");
        }

        /// <summary>
        ///     (This method is obsolete) a DbModelBuilder extension method that applies the configuration
        ///     attributes.
        /// </summary>
        /// <param name="modelBuilder">The modelBuilder to act on.</param>
        /// <param name="assembly">    The assembly.</param>
        /// <param name="typeFilter">  A filter specifying the type.</param>
        [Obsolete("Use AttributeConventions instead")]
        public static void ApplyConfigurationAttributes(
            this DbModelBuilder modelBuilder,
            Assembly assembly,
            Func<Type, bool> typeFilter)
        {
            throw new InvalidOperationException(
                "This method is no longer supported. Instead, use modelBuilder.Conventions.Add(new DecimalPrecisionAttributeConvention()) and other equivalents");
        }
    }
}