// <copyright file="StringWriterWithEncoding.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the string writer with encoding class</summary>
namespace Clarity.Ecommerce.NugetProcessing
{
    using System.IO;
    using System.Text;

    public sealed class StringWriterWithEncoding : StringWriter
    {
        public override Encoding Encoding { get; }

        public StringWriterWithEncoding(Encoding encoding)
        {
            Encoding = encoding;
        }
    }
}
