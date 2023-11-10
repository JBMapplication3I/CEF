// <copyright file="DoMockingSetupForContextRunnerBadges.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner badges class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerBadgesAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Badges
            if (DoAll || DoBadges || DoBadgeTable)
            {
                var index = 0;
                RawBadges = new()
                {
                    await CreateADummyBadgeAsync(id: ++index, key: "brand-" + index, name: "Badge One", desc: "description").ConfigureAwait(false),
                };
                await InitializeMockSetBadgesAsync(mockContext, RawBadges).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Badge Images
            if (DoAll || DoBadges || DoBadgeImageTable)
            {
                var index = 0;
                RawBadgeImages = new()
                {
                    await CreateADummyBadgeImageAsync(id: ++index, key: "Image-" + index, name: "An Image " + index, desc: "desc", displayName: "An Image " + index, isPrimary: true, originalFileFormat: "jpg", originalFileName: "image.jpg", thumbnailFileFormat: "jpg", thumbnailFileName: "image.jpg").ConfigureAwait(false),
                };
                await InitializeMockSetBadgeImagesAsync(mockContext, RawBadgeImages).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Badge Image Types
            if (DoAll || DoBadges || DoBadgeImageTypeTable)
            {
                var index = 0;
                RawBadgeImageTypes = new()
                {
                    await CreateADummyBadgeImageTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetBadgeImageTypesAsync(mockContext, RawBadgeImageTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Badge Types
            if (DoAll || DoBadges || DoBadgeTypeTable)
            {
                var index = 0;
                RawBadgeTypes = new()
                {
                    await CreateADummyBadgeTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetBadgeTypesAsync(mockContext, RawBadgeTypes).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
