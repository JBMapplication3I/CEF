// <copyright file="File.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the file class</summary>
namespace Clarity.Ecommerce.NugetProcessing
{
    using System.Xml.Serialization;

    [XmlRoot("file")]
    public class File
    {
        [XmlAttribute]
        public string src { get; set; } = null!;

        [XmlAttribute]
        public string target { get; set; } = "lib\\net462";
    }
}
