// <copyright file="DoMockingSetupForContextRunnerMediaAsync.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner media class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerMediaAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Stored Files
            if (DoAll || DoMedia || DoStoredFileTable)
            {
                RawStoredFiles = new()
                {
                    await CreateADummyStoredFileAsync(id: 1, key: "STORED-FILE-1", name: "Stored File 1", desc: "desc", fileFormat: ".docx", fileName: "stored-file.docx").ConfigureAwait(false),
                };
                await InitializeMockSetStoredFilesAsync(mockContext, RawStoredFiles).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
