// <copyright file="ProcessedResult.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the processed result class</summary>
// ReSharper disable InconsistentNaming
#pragma warning disable IDE1006, SA1300 // Naming Styles
namespace CodeGenerator
{
    using System;
    using System.Linq;

    /// <summary>Encapsulates the result of a processed.</summary>
    public class ProcessedResult
    {
        /// <summary>Initializes a new instance of the <see cref="ProcessedResult"/> class.</summary>
        /// <param name="t">            The Type to process.</param>
        /// <param name="bt">           The bt.</param>
        /// <param name="btString">     The bt string.</param>
        /// <param name="generatedName">Name of the generated.</param>
        /// <param name="usedIn">       The used in.</param>
#pragma warning disable SA1305 // Field names should not use Hungarian notation
        public ProcessedResult(Type t, Type? bt, string btString, string generatedName, bool[] usedIn)
#pragma warning restore SA1305 // Field names should not use Hungarian notation
        {
            this.t = t;
            this.bt = bt;
            this.btString = btString;
            this.generatedName = generatedName;
            this.hasBase = t.BaseType != null;
            if (this.hasBase)
            {
                var temp = t.BaseType!.Name + string.Empty;
                if (temp.Contains("`") && t.BaseType.GetGenericArguments().Any())
                {
                    var replace = "<" + t.BaseType.GetGenericArguments().Select(x => x.Name).Aggregate((c, n) => c + ", " + n) + ">";
                    temp = temp[..temp.IndexOf('`')];
                    temp += replace;
                }
                /*
                else if (temp.EndsWith("[]"))
                {
                    temp = temp[0..^2];
                    temp += t.BaseType.GetElementType().Name;
                }
                */
                this.generatedBaseName = temp;
            }
            this.usedIn = usedIn;
        }

        /// <summary>Gets the t.</summary>
        /// <value>The t.</value>
        public Type t { get; }

        /// <summary>Gets the bt.</summary>
        /// <value>The bt.</value>
        public Type? bt { get; }

        /// <summary>Gets the bt string.</summary>
        /// <value>The bt string.</value>
        public string btString { get; }

        /// <summary>Gets the name of the generated.</summary>
        /// <value>The name of the generated.</value>
        public string generatedName { get; }

        /// <summary>Gets the name of the generated base.</summary>
        /// <value>The name of the generated base.</value>
        public string? generatedBaseName { get; }

        /// <summary>Gets a value indicating whether this ProcessedResult has a base type.</summary>
        /// <value>True if this ProcessedResult has a base type, false if not.</value>
        public bool hasBase { get; }

        /// <summary>Gets the used in.</summary>
        /// <value>The used in.</value>
        public bool[] usedIn { get; }
    }
}
