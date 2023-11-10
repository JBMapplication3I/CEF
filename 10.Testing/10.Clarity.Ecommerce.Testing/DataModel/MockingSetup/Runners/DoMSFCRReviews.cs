// <copyright file="DoMockingSetupForContextRunnerReviews.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner reviews class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerReviewsAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Reviews
            if (DoAll || DoReviews || DoReviewTable)
            {
                var index = 0;
                RawReviews = new()
                {
                    await CreateADummyReviewAsync(id: ++index, key: "REVIEW-1-Product",      name: "I LOVE THIS THING", desc: "desc", approved: true, comment: "This is my awesome comment.",                         sortOrder: 1, value: 4, productID:      1152).ConfigureAwait(false),
                    await CreateADummyReviewAsync(id: ++index, key: "REVIEW-2-Product",      name: "Bad Language",      desc: "desc",                 comment: "This review says bad things with colorful language.", sortOrder: 1, value: 5, productID:      1152).ConfigureAwait(false),
                    await CreateADummyReviewAsync(id: ++index, key: "REVIEW-1-Category",     name: "I LOVE THIS THING", desc: "desc", approved: true, comment: "This is my awesome comment.",                         sortOrder: 1, value: 4, categoryID:     0001).ConfigureAwait(false),
                    await CreateADummyReviewAsync(id: ++index, key: "REVIEW-2-Category",     name: "Bad Language",      desc: "desc",                 comment: "This review says bad things with colorful language.", sortOrder: 1, value: 5, categoryID:     0001).ConfigureAwait(false),
                    await CreateADummyReviewAsync(id: ++index, key: "REVIEW-1-Manufacturer", name: "I LOVE THIS THING", desc: "desc", approved: true, comment: "This is my awesome comment.",                         sortOrder: 1, value: 4, manufacturerID: 0001).ConfigureAwait(false),
                    await CreateADummyReviewAsync(id: ++index, key: "REVIEW-2-Manufacturer", name: "Bad Language",      desc: "desc",                 comment: "This review says bad things with colorful language.", sortOrder: 1, value: 5, manufacturerID: 0001).ConfigureAwait(false),
                    await CreateADummyReviewAsync(id: ++index, key: "REVIEW-1-Store",        name: "I LOVE THIS THING", desc: "desc", approved: true, comment: "This is my awesome comment.",                         sortOrder: 1, value: 4, storeID:        0001).ConfigureAwait(false),
                    await CreateADummyReviewAsync(id: ++index, key: "REVIEW-2-Store",        name: "Bad Language",      desc: "desc",                 comment: "This review says bad things with colorful language.", sortOrder: 1, value: 5, storeID:        0001).ConfigureAwait(false),
                    await CreateADummyReviewAsync(id: ++index, key: "REVIEW-1-Vendor",       name: "I LOVE THIS THING", desc: "desc", approved: true, comment: "This is my awesome comment.",                         sortOrder: 1, value: 4, vendorID:       0001).ConfigureAwait(false),
                    await CreateADummyReviewAsync(id: ++index, key: "REVIEW-2-Vendor",       name: "Bad Language",      desc: "desc",                 comment: "This review says bad things with colorful language.", sortOrder: 1, value: 5, vendorID:       0001).ConfigureAwait(false),
                };
                await InitializeMockSetReviewsAsync(mockContext, RawReviews).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Review Types
            if (DoAll || DoReviews || DoReviewTypeTable)
            {
                var index = 0;
                RawReviewTypes = new()
                {
                    await CreateADummyReviewTypeAsync(id: ++index, key: "Overall", name: "Overall", desc: "desc", sortOrder: 1, displayName: "Overall").ConfigureAwait(false),
                    await CreateADummyReviewTypeAsync(id: ++index, key: "Category", name: "Category", desc: "desc", sortOrder: 2, displayName: "Category").ConfigureAwait(false),
                    await CreateADummyReviewTypeAsync(id: ++index, key: "Manufacturer", name: "Manufacturer", desc: "desc", sortOrder: 3, displayName: "Manufacturer").ConfigureAwait(false),
                    await CreateADummyReviewTypeAsync(id: ++index, key: "Product", name: "Product", desc: "desc", sortOrder: 4, displayName: "Product").ConfigureAwait(false),
                    await CreateADummyReviewTypeAsync(id: ++index, key: "Store", name: "Store", desc: "desc", sortOrder: 5, displayName: "Store").ConfigureAwait(false),
                    await CreateADummyReviewTypeAsync(id: ++index, key: "User", name: "User", desc: "desc", sortOrder: 6, displayName: "User").ConfigureAwait(false),
                    await CreateADummyReviewTypeAsync(id: ++index, key: "Vendor", name: "Vendor", desc: "desc", sortOrder: 7, displayName: "Vendor").ConfigureAwait(false),
                };
                await InitializeMockSetReviewTypesAsync(mockContext, RawReviewTypes).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
