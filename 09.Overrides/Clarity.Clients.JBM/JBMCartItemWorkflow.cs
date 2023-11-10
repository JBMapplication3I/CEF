// <copyright file="JBMWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the jbm workflow class</summary>
namespace Clarity.Clients.JBM
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading.Tasks;
    using Clarity.Ecommerce.Interfaces.DataModel;
    using Clarity.Ecommerce.Mapper;
    using Clarity.Ecommerce.Workflow;
    using Ecommerce;
    using Ecommerce.DataModel;
    using Ecommerce.Interfaces.Models;
    using Ecommerce.Models;
    using Ecommerce.Utilities;

    public partial class JBMCartItemWorkflow : CartItemWorkflow
    {
        ///// <summary>Relate Optional Product.</summary>
        ///// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        ///// <param name="entity">   The entity that has a Optional Product.</param>
        ///// <param name="model">    The model that has a Optional Product.</param>
        ///// <param name="timestamp">The timestamp Date/Time.</param>
        ///// <param name="context">  The context.</param>
        //// ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        //protected override async Task RelateOptionalProductAsync(
        //    ICartItem entity,
        //    ISalesItemBaseModel<IAppliedCartItemDiscountModel> model,
        //    DateTime timestamp,
        //    string? contextProfileName)
        //{
        //    // Must have the core objects on both sides
        //    Contract.RequiresNotNull(entity);
        //    Contract.RequiresNotNull(model);
        //    // Look up the Object Model
        //    // Allowed to auto-generate if not found
        //    //var resolved = await Workflows.Products.ResolveWithAutoGenerateOptionalAsync(
        //    //        byID: model.ProductID, // By Other ID
        //    //        byKey: model.ProductKey, // By Flattened Other Key
        //    //        byName: model.ProductName, // By Flattened Other Name
        //    //        model: null, // Skip sending Product on ISalesItemBase as it's not on the interface
        //    //        context: context)
        //    //    .ConfigureAwait(false);
        //    // Check for IDs and objects
        //    var entityIDIsNull = !Contract.CheckValidID(entity.ProductID);
        //    var modelIDIsNull = !Contract.CheckValidID(model.ProductID);
        //    var entityObjectIsNull = entity.Product == null;
        //    var modelObjectIsNull = model.Product == null;
        //    if (entityIDIsNull && modelIDIsNull && entityObjectIsNull && modelObjectIsNull)
        //    {
        //        // [Optional] Scenario 1: Nothing we can do with/to either end, so do nothing
        //        return;
        //    }
        //    var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.ProductID == model.ProductID;
        //    var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.ProductID == model.Product!.ID;
        //    if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
        //    {
        //        // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
        //        if (!entityObjectIsNull && !modelObjectIsNull)
        //        {
        //            entity.Product!.UpdateProductFromModel(model.Product!, timestamp, timestamp);
        //        }
        //        return;
        //    }
        //    if (!modelIDIsNull)
        //    {
        //        // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
        //        entity.ProductID = model.ProductID;
        //        if (!modelObjectIsNull)
        //        {
        //            if (!entityObjectIsNull)
        //            {
        //                // [Optional/Required] Scenario 3a: We can't update the existing object because it's the wrong one
        //            }
        //            else
        //            {
        //                // [Optional/Required] Scenario 3b: We can't assign a new object in on this entity because it would duplicate the record
        //            }
        //        }
        //        return;
        //    }
        //    var modelObjectIDIsNull = !modelObjectIsNull && !Contract.CheckValidID(model.Product?.ID);
        //    var modelObjectIsActive = !modelObjectIsNull && (model.Product?.Active ?? false);
        //    if (!modelObjectIsNull && !modelObjectIDIsNull)
        //    {
        //        if (modelObjectIsActive)
        //        {
        //            // [Optional] Scenario 4: We have IDs but they don't match, assign the model's ID to the entity's ID (from the model object)
        //            entity.ProductID = model.Product!.ID;
        //            return;
        //        }
        //        // [Optional] Scenario 5: We have IDs but they don't match and the model has been deactivated, remove the entity from it's master
        //        entity.ProductID = null;
        //        entity.Product = null;
        //        return;
        //    }
        //    if (!entityIDIsNull && modelObjectIsActive)
        //    {
        //        // [Optional] Scenario 6: We have an entity id, but a new model, remove the id on the entity and assign the new model
        //        entity.ProductID = null;
        //        entity.Product = (Product)model.Product!.CreateProductEntity(timestamp, contextProfileName);
        //        return;
        //    }
        //    if (entityIDIsNull && modelObjectIsActive)
        //    {
        //        // [Optional] Scenario 7: We don't have an entity id, and we have a new model, assign the new model
        //        entity.Product = (Product)model.Product!.CreateProductEntity(timestamp, contextProfileName);
        //        return;
        //    }
        //    if (!entityIDIsNull && modelIDIsNull && !modelObjectIsActive)
        //    {
        //        // [Optional] Scenario 8: We were removing or deactivating the object, clear it from the entity
        //        entity.ProductID = null;
        //        entity.Product = null;
        //        return;
        //    }
        //    // Note: We would do this if it wasn't just a base typable entity (like ReportType, which has extra values to adjust on the model)
        //    // if (!entityObjectIsNull && modelObjectIsActive && entityObjectAndModelObjectHaveSameID)
        //    // {
        //    //     // TODO: Determine 'Equals' object between the objects so we only update if different
        //    //     // [Optional] Scenario 9: We have data on both sides, update the object, assign the values using the Update action
        //    //     entity.Product.UpdateProductFromModel(resolved.Result, updateTimestamp);
        //    //     return;
        //    // }
        //    // [Optional/Required] Scenario 10: Could not figure out what to do
        //    throw new InvalidOperationException(
        //        "Couldn't figure out how to relate the given Product to the CartItem entity");
        //}
    }
}