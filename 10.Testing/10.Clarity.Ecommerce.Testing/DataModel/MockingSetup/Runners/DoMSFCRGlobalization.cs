// <copyright file="DoMockingSetupForContextRunnerGlobalizationAsync.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner globalization class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerGlobalizationAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Languages
            if (DoAll || DoGlobalization || DoLanguageTable)
            {
                var index = 0;
                RawLanguages = new()
                {
                    await CreateADummyLanguageAsync(id: ++index, key: "en_US", locale: "en_US", unicodeName: "English (US)").ConfigureAwait(false),
                    await CreateADummyLanguageAsync(id: ++index, key: "es_SP", locale: "es_SP", unicodeName: "Spanish (Spain)").ConfigureAwait(false),
                };
                await InitializeMockSetLanguagesAsync(mockContext, RawLanguages).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Language Images
            if (DoAll || DoGlobalization || DoLanguageImageTable)
            {
                var index = 0;
                RawLanguageImages = new()
                {
                    await CreateADummyLanguageImageAsync(id: ++index, key: "Image-" + index, name: "An Image " + index, desc: "desc", displayName: "An Image " + index, isPrimary: true, originalFileFormat: "jpg", originalFileName: "image.jpg", thumbnailFileFormat: "jpg", thumbnailFileName: "image.jpg").ConfigureAwait(false),
                };
                await InitializeMockSetLanguageImagesAsync(mockContext, RawLanguageImages).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Language Image Types
            if (DoAll || DoGlobalization || DoLanguageImageTypeTable)
            {
                var index = 0;
                RawLanguageImageTypes = new()
                {
                    await CreateADummyLanguageImageTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetLanguageImageTypesAsync(mockContext, RawLanguageImageTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: UiKeys
            if (DoAll || DoGlobalization || DoUiKeyTable)
            {
                var index = 0;
                RawUiKeys = new()
                {
                    await CreateADummyUiKeyAsync(id: ++index, key: "admin.label-1", type: "admin").ConfigureAwait(false),
                    await CreateADummyUiKeyAsync(id: ++index, key: "admin.label-2", type: "admin").ConfigureAwait(false),
                    await CreateADummyUiKeyAsync(id: ++index, key: "store.label-1", type: "store").ConfigureAwait(false),
                };
                await InitializeMockSetUiKeysAsync(mockContext, RawUiKeys).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: UiTranslations
            if (DoAll || DoGlobalization || DoUiTranslationTable)
            {
                var index = 0;
                RawUiTranslations = new()
                {
                    await CreateADummyUiTranslationAsync(id: ++index, key: "admin.label-1.en-us", locale: "en-US", value: "admin label one").ConfigureAwait(false),
                    await CreateADummyUiTranslationAsync(id: ++index, key: "admin.label-1.ru-ru", locale: "ru-RU", value: "пользоваться").ConfigureAwait(false),
                    await CreateADummyUiTranslationAsync(id: ++index, key: "admin.label-2.en-us", locale: "en-US", uiKeyID: 2, value: "admin label two").ConfigureAwait(false),
                    await CreateADummyUiTranslationAsync(id: ++index, key: "admin.label-2.ru-ru", locale: "ru-RU", uiKeyID: 2, value: "можетепереводить").ConfigureAwait(false),
                    await CreateADummyUiTranslationAsync(id: ++index, key: "store.label-1.en-us", locale: "en-US", uiKeyID: 3, value: "store label one").ConfigureAwait(false),
                    await CreateADummyUiTranslationAsync(id: ++index, key: "store.label-1.en-ru", locale: "ru-RU", uiKeyID: 3, value: "ненравятся").ConfigureAwait(false),
                };
                await InitializeMockSetUiTranslationsAsync(mockContext, RawUiTranslations).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
