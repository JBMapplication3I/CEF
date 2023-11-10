// <copyright file="UploadFilesServiceTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the upload files service tests class</summary>
// ReSharper disable UnusedMember.Global
namespace Clarity.Ecommerce.Testing
{
    using System.Configuration;
    using System.IO;

    public class UploadFilesServiceTests : XUnitLogHelper
    {
        public UploadFilesServiceTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            ConfigurationManager.AppSettings["Clarity.Uploads.Files"] = @"c:\temp\cef\files";
            ConfigurationManager.AppSettings["Clarity.Uploads.Images.Product"] = @"c:\temp\cef\files\product\images\";
            ConfigurationManager.AppSettings["Clarity.Uploads.Images.Category"] = @"c:\temp\cef\files\category\images\";
            ConfigurationManager.AppSettings["Clarity.Uploads.Files.Product"] = @"c:\temp\cef\files\product\docs\";
            ConfigurationManager.AppSettings["Clarity.Uploads.Files.Category"] = @"c:\temp\cef\files\category\docs\";
            var tempFilesDirectory = new DirectoryInfo(ConfigurationManager.AppSettings["Clarity.Files"]!);
            if (tempFilesDirectory.Exists)
            {
                tempFilesDirectory.Delete(true);
            }
            tempFilesDirectory.Create();
            var productDirectory = tempFilesDirectory.CreateSubdirectory("product");
            productDirectory.CreateSubdirectory("images");
            productDirectory.CreateSubdirectory("docs");
            var categoryDirectory = tempFilesDirectory.CreateSubdirectory("category");
            categoryDirectory.CreateSubdirectory("images");
            categoryDirectory.CreateSubdirectory("docs");
        }
    }
}
