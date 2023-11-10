// <copyright file="Package.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the package class</summary>
namespace Clarity.Ecommerce.NugetProcessing
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("package", Namespace = "http://schemas.microsoft.com/packaging/2010/07/nuspec.xsd")]
    public class Package
    {
        [XmlElement]
        public Metadata metadata { get; set; } = new Metadata();

        [XmlArray, XmlArrayItem("file")]
        public List<File> files { get; set; } = new List<File>();
    }
}
