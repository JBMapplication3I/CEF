// <copyright file="AssociateJsonAttributesWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate JSON attributes workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Collections.Concurrent;
    using System.Data.Entity.Infrastructure;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using Models;
    using Utilities;

    /// <summary>An associate JSON attributes workflow.</summary>
    /// <seealso cref="IAssociateJsonAttributesWorkflow"/>
    public class AssociateJsonAttributesWorkflow : IAssociateJsonAttributesWorkflow
    {
        /// <summary>The workflows.</summary>
        private readonly IWorkflowsController workflows
            = RegistryLoaderWrapper.GetInstance<IWorkflowsController>();

        /// <summary>Gets Store IDs as you resolve them in memory, they won't change.</summary>
        private static ConcurrentDictionary<string, int> ResolvedIDs { get; }
            = new();

        /// <inheritdoc/>
        public async Task AssociateObjectsAsync(
            IHaveJsonAttributesBase entity,
            IHaveJsonAttributesBaseModel model,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await AssociateObjectsAsync(entity, model, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task AssociateObjectsAsync(
            IHaveJsonAttributesBase entity,
            IHaveJsonAttributesBaseModel model,
            IClarityEcommerceEntities context)
        {
            // New Entity may not have the object list initialized yet
            InitializeObjectListIfNull(entity);
            // Update/Add
            if (model.SerializableAttributes == null!)
            {
                return;
            }
            var entityDictionary = entity.JsonAttributes.DeserializeAttributesDictionary();
            foreach (var kvp in model.SerializableAttributes)
            {
                if (!Contract.CheckValidKey(kvp.Value?.Value))
                {
                    entityDictionary.TryRemove(kvp.Key, out _);
                    continue;
                }
                // if kvp.ID == 0 -> find or create GeneralAttribute based on kvp.Key ?
                if (kvp.Value!.ID <= 0)
                {
                    if (ResolvedIDs.ContainsKey(kvp.Key))
                    {
                        kvp.Value.ID = ResolvedIDs[kvp.Key];
                    }
                    else
                    {
                        await RetryHelper.RetryOnExceptionAsync<DbUpdateException>(
                                async () =>
                                {
                                    kvp.Value.ID = ResolvedIDs[kvp.Key] = await workflows.GeneralAttributes.ResolveWithAutoGenerateToIDAsync(
                                            byID: null,
                                            byKey: kvp.Key,
                                            byName: kvp.Key,
                                            byDisplayName: null,
                                            model: new GeneralAttributeModel
                                            {
                                                Active = true,
                                                CustomKey = kvp.Key,
                                                TypeKey = "General",
                                            },
                                            context: context)
                                        .ConfigureAwait(false);
                                })
                            .ConfigureAwait(false);
                    }
                }
                if (!Contract.CheckValidKey(kvp.Value.Key))
                {
                    kvp.Value.Key = kvp.Key;
                }
                entityDictionary[kvp.Key] = kvp.Value;
            }
            entity.JsonAttributes = entityDictionary.SerializeAttributesDictionary();
            if (!Contract.CheckValidKey(entity.JsonAttributes))
            {
                entity.JsonAttributes = "{}";
            }
        }

        /// <summary>Initializes the object list if null.</summary>
        /// <param name="entity">The entity.</param>
        private static void InitializeObjectListIfNull(IHaveJsonAttributesBase entity)
        {
            if (!string.IsNullOrEmpty(entity.JsonAttributes))
            {
                return;
            }
            entity.JsonAttributes = new SerializableAttributesDictionary().SerializeAttributesDictionary();
        }
    }
}
