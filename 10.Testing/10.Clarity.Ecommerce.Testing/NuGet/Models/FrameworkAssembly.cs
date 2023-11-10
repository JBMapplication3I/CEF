// <copyright file="FrameworkAssembly.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the framework assembly class</summary>
namespace Clarity.Ecommerce.NugetProcessing
{
    using System.Xml.Serialization;

    [XmlRoot("frameworkAssembly")]
    public class FrameworkAssembly
    {
        [XmlAttribute]
        public string assemblyName { get; set; } = null!;

        [XmlAttribute]
        public string targetFramework { get; set; } = "net462";
    }
}
