// <copyright file="SearchModuleBase.Mods.Roles.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the search module base. mods. roles class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System.Linq;
    using Interfaces.Providers.Searching;
    using Nest;
    using Utilities;

    internal abstract partial class SearchModuleBase<TSearchViewModel, TSearchForm, TIndexModel>
        where TSearchViewModel : SearchViewModelBase<TSearchForm, TIndexModel>, new()
        where TSearchForm : SearchFormBase, new()
        where TIndexModel : IndexableModelBase
    {
        #region Constant Strings
        protected const string NestedNameForRecordRoleSingle = "record-single-role";
        protected const string NestedNameForRecordRolesAny = "record-roles-any";
        protected const string NestedNameForRecordRolesAllPrefix = "record-roles-all-";
        protected const string AnonymousRole = "Anonymous";
        #endregion

        /// <summary>Roles single query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer RolesSingleQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (string.IsNullOrWhiteSpace(form.Role))
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Nested(n => n
                    .Name(NestedNameForRecordRoleSingle)
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsSingleRole)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.RequiresRolesList)
                    .Query(nq => +nq
                        .Bool(b => b
                            .Should(
                                s => s.Term(p => p.RequiresRolesList!.First().Suffix(Keyword), form.Role)
                            ).MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                        )
                    )
                    .IgnoreUnmapped()
                );
            return returnQuery;
        }

        /// <summary>Roles any query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer RolesAnyQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (ElasticSearchingProviderConfig.InvertRoles && form.RolesAny?.Any() == true)
            {
                returnQuery &= +q
                    .Nested(n => n.Name(NestedNameForRecordRolesAny)
                        .Boost(ElasticSearchingProviderConfig.SearchingBoostsAnyRoles)
                        .InnerHits(i => i.Explain())
                        .Path(p => p.RequiresRolesList)
                        .Query(nq => +nq
                            .Bool(b => b
                                .Should(
                                    s => s.Terms(t => t.Field(p => p.RequiresRolesList!.First().RoleName.Suffix(Keyword)).Terms(form.RolesAny))
                                ).MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                            )
                        )
                        .IgnoreUnmapped()
                    );
            }
            else
            {
                if (form.RolesAny?.Any() != true)
                {
                    form.RolesAny = new[] { AnonymousRole };
                }
                else if (!form.RolesAny.Contains(AnonymousRole))
                {
                    var roles = form.RolesAny.ToList();
                    roles.Add(AnonymousRole);
                    form.RolesAny = roles.ToArray();
                }
                returnQuery &= +q
                    .Nested(n => n.Name(NestedNameForRecordRolesAny)
                        .Boost(ElasticSearchingProviderConfig.SearchingBoostsAnyRoles)
                        .InnerHits(i => i.Explain())
                        .Path(p => p.RequiresRolesList)
                        .Query(nq => +nq
                            .Bool(b => b
                                .Should(
                                    s => s.Terms(t => t.Field(p => p.RequiresRolesList!.First().RoleName.Suffix(Keyword)).Terms(form.RolesAny))
                                ).MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                            )
                        )
                        .IgnoreUnmapped()
                    );
            }
            return returnQuery;
        }

        /// <summary>Roles all query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer RolesAllQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (form.RolesAll?.Any() != true)
            {
                return returnQuery;
            }
            var filterList = form.RolesAll.Where(Contract.CheckValidKey).ToList();
            if (filterList.Count == 0)
            {
                return returnQuery;
            }
            filterList.ForEach(filter =>
            {
                returnQuery &= +q
                    .Nested(n => n
                        .Name($"{NestedNameForRecordRolesAllPrefix}{filter}")
                        .Boost(ElasticSearchingProviderConfig.SearchingBoostsAllRoles)
                        .InnerHits(i => i.Explain())
                        .Path(p => p.RequiresRolesList)
                        .Query(nq => +nq
                            .Bool(b => b.MinimumShouldMatch(MinimumShouldMatch.Percentage(100d)) // 100%
                                .Should(
                                    // resharper disable PossiblyMistakenUseOfParamsMethod
                                    s => s.Terms(t => t.Field(p => p.RequiresRolesList!.First().Suffix(Keyword)).Terms(filter))
                                    // resharper remanufacturer PossiblyMistakenUseOfParamsMethod
                                )
                            )
                        ).IgnoreUnmapped()
                    );
            });
            return returnQuery;
        }

        /// <summary>Searches for the first view model additional assignments for roles.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="_">      The result.</param>
        /// <param name="setting">True to setting.</param>
        protected virtual void SearchViewModelAdditionalAssignmentsForRoles(
            TSearchViewModel model,
            ISearchResponse<TIndexModel> _,
            bool setting)
        {
            if (!setting)
            {
                return;
            }
            model.Form!.RolesAny = null;
        }
    }
}
