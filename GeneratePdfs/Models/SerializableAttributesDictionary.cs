﻿// <copyright file="SerializableAttributesDictionary.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the serializable attributes dictionary class</summary>
namespace GeneratePdfs.Models
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    /// <summary>Dictionary of serializable attributes.</summary>
    public class SerializableAttributesDictionary : ConcurrentDictionary<string, SerializableAttributeObject>
    {
        /// <summary>Initializes a new instance of the <see cref="SerializableAttributesDictionary"/> class.</summary>
        public SerializableAttributesDictionary()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SerializableAttributesDictionary"/> class.</summary>
        /// <param name="comparer">The comparer.</param>
        public SerializableAttributesDictionary(IEqualityComparer<string> comparer)
            : base(comparer)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SerializableAttributesDictionary"/> class.</summary>
        /// <param name="source">Source for the.</param>
        public SerializableAttributesDictionary(IDictionary<string, SerializableAttributeObject> source)
            : base(source)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SerializableAttributesDictionary"/> class.</summary>
        /// <param name="source">  Source for the dictionary.</param>
        /// <param name="comparer">The comparer.</param>
        public SerializableAttributesDictionary(
            IDictionary<string, SerializableAttributeObject> source,
            IEqualityComparer<string> comparer)
            : base(source, comparer)
        {
        }
    }
}
