// <copyright file="Metadata.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the metadata class</summary>
namespace Clarity.Ecommerce.NugetProcessing
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class Metadata
    {
        [XmlElement]
        public string id { get; set; } = "$id$";

        [XmlElement]
        public string version { get; set; } = "$version$";

        [XmlElement]
        public string title { get; set; } = "$title$";

        [XmlElement]
        public string authors { get; set; } = "$author$";

        [XmlElement]
        public string owners { get; set; } = "$author$";

        [XmlElement]
        public string licenseUrl { get; set; } = "https://clarity-ventures.visualstudio.com/CEF-Product/";

        [XmlElement]
        public string projectUrl { get; set; } = "https://clarity-ventures.visualstudio.com/CEF-Product/";

        [XmlElement]
        public bool requireLicenseAcceptance { get; set; }

        [XmlElement]
        public string description { get; set; } = "No description";

        [XmlElement]
        public string releaseNotes { get; set; } = "No Notes";

        [XmlElement]
        public string copyright { get; set; } = "$copyright$";

        [XmlElement]
        public string tags { get; set; } = "internal clarity ecommerce";

        [XmlArray, XmlArrayItem("dependency")]
        public List<Dependency> dependencies { get; set; } = new List<Dependency>();

        [XmlArray, XmlArrayItem("frameworkAssembly")]
        public List<FrameworkAssembly> frameworkAssemblies { get; set; } = new List<FrameworkAssembly>();
    }
}
