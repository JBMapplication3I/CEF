// <copyright file="FranchiseService.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the franchise service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI,
        Authenticate,
        Route("/Franchises/CurrentFranchise", "GET",
            Summary = "Returns the current franchise ID for the specified user identifier.")]
    public partial class GetCurrentFranchiseID : IReturn<int?>
    {
        /// <summary>Gets or sets the UserID.</summary>
        /// <value>The UserID.</value>
        [ApiMember(Name = nameof(UserID), DataType = "int?", ParameterType = "query", IsRequired = true,
            Description = "User ID")]
        public int UserID { get; set; }
    }

    [PublicAPI,
        Authenticate,
        Route("/Franchises/Franchise/Current/Administration", "GET",
            Summary = "Use to get the franchise that the current user has administrative rights to (limited to franchise admins)")]
    public partial class GetCurrentFranchiseAdministration : IReturn<CEFActionResponse<FranchiseModel>>
    {
    }

    [PublicAPI]
    public partial class FranchiseService
    {
        public async Task<object?> Get(GetCurrentFranchiseID request)
        {
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            return await context.FranchiseUsers
                .AsNoTracking()
                .FilterByActive(true)
                .Where(x => x.SlaveID == request.UserID)
                .Select(x => x.MasterID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        // Franchise Administration
        public async Task<object?> Get(GetCurrentFranchiseAdministration _)
        {
            // NOTE: Never cached, for admins only
            try
            {
                var result = (FranchiseModel?)await Workflows.Franchises.GetAsync(
                        await CurrentFranchiseForFranchiseAdminIDOrThrow401Async().ConfigureAwait(false),
                        contextProfileName: null)
                    .ConfigureAwait(false);
                return result.WrapInPassingCEFARIfNotNull();
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        name: "GetCurrentFranchiseAdministration Error",
                        message: ex.Message,
                        ex: ex,
                        contextProfileName: null)
                    .ConfigureAwait(false);
                return CEFAR.FailingCEFAR<FranchiseModel>(
                    "Unable to locate current franchise the user would be administrator of");
            }
        }
    }
}
