// <copyright file="IAppliedDiscountBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAppliedDiscountBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    /// <summary>Interface for applied discount base.</summary>
    /// <typeparam name="TMaster"> Type of the master.</typeparam>
    /// <typeparam name="TApplied">Type of the applied.</typeparam>
    /// <seealso cref="IAppliedDiscountBase"/>
    /// <seealso cref="IAmARelationshipTable{TMaster,Discount}"/>
    public interface IAppliedDiscountBase<out TMaster, TApplied>
        : IAppliedDiscountBase,
            IAmARelationshipTable<TMaster, Discount>
        where TMaster : IHaveAppliedDiscountsBase<TMaster, TApplied>
        where TApplied : IAppliedDiscountBase<TMaster, TApplied>
    {
    }

    /// <summary>Interface for applied discount base.</summary>
    /// <seealso cref="IBase"/>
    public interface IAppliedDiscountBase : IBase
    {
        /// <summary>Gets or sets the discount total.</summary>
        /// <value>The discount total.</value>
        decimal DiscountTotal { get; set; }

        /// <summary>Gets or sets the applications used.</summary>
        /// <value>The applications used.</value>
        int? ApplicationsUsed { get; set; }

        /// <summary>Gets or sets target applications used.</summary>
        /// <value>The target applications used.</value>
        int? TargetApplicationsUsed { get; set; }
    }
}
