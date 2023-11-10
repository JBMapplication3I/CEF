// <copyright file="AvaTaxPath.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ava tax path class</summary>
namespace Avalara.AvaTax.RestClient
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>Build a URL including variables in paths and query strings.</summary>
    /// <remarks>Since this feature is not consistently available throughout C# / DOTNET versions, this is a cross-
    /// version compatibility feature.</remarks>
    public class AvaTaxPath
    {
        /// <summary>Full pathname of the file.</summary>
        private readonly StringBuilder _path = new();

        /// <summary>The query.</summary>
        private readonly Dictionary<string, string> _query = new();

        /// <summary>Initializes a new instance of the <see cref="AvaTaxPath"/> class.</summary>
        /// <param name="uri">URI of the document.</param>
        public AvaTaxPath(string uri)
        {
            _path.Append(uri);
        }

        /// <summary>Apply a variable in the path.</summary>
        /// <param name="name"> The name.</param>
        /// <param name="value">The value.</param>
        public void ApplyField(string name, object value)
        {
            _path.Replace("{" + name + "}", System.Uri.EscapeDataString(value.ToString()!));
        }

        /// <summary>Apply a variable in the path.</summary>
        /// <param name="name"> The name.</param>
        /// <param name="value">The value.</param>
        public void AddQuery(string name, object? value)
        {
            if (value != null)
            {
                _query[name] = value.ToString()!;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            var output = new StringBuilder(_path.ToString());
            if (_query.Count > 0)
            {
                output.Append('?');
                foreach (var kvp in _query)
                {
                    output.AppendFormat(
                        "{0}={1}&",
                        System.Uri.EscapeDataString(kvp.Key),
                        System.Uri.EscapeDataString(kvp.Value));
                }
                output.Length -= 1;
            }
            return output.ToString();
        }
    }
}
