// <copyright file="Dependency.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the dependency class</summary>
namespace Clarity.Ecommerce.NugetProcessing
{
    using System.Xml.Serialization;

    [XmlRoot("dependency")]
    public class Dependency
    {
        [XmlAttribute]
        public string id { get; set; } = null!;

        [XmlAttribute]
        public string version { get; set; } = null!;
    }
}
