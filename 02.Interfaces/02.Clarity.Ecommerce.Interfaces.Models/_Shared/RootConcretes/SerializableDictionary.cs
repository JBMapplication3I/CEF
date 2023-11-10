// <copyright file="SerializableDictionary.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the serializable dictionary class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>Dictionary of serializable.</summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <seealso cref="Dictionary{String, T}"/>
    [Serializable]
    public class SerializableDictionary<T> : Dictionary<string, T>
    {
        /// <summary>Initializes a new instance of the <see cref="SerializableDictionary{T}"/> class.</summary>
        public SerializableDictionary()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SerializableDictionary{T}"/> class.</summary>
        /// <param name="comparer">The comparer.</param>
        public SerializableDictionary(IEqualityComparer<string> comparer)
            : base(comparer)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SerializableDictionary{T}"/> class.</summary>
        /// <param name="dictionary">The dictionary.</param>
        public SerializableDictionary(IDictionary<string, T> dictionary)
            : base(dictionary)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SerializableDictionary{T}"/> class.</summary>
        /// <param name="capacity">The capacity.</param>
        public SerializableDictionary(int capacity)
            : base(capacity)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SerializableDictionary{T}"/> class.</summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="comparer">  The comparer.</param>
        public SerializableDictionary(IDictionary<string, T> dictionary, IEqualityComparer<string> comparer)
            : base(dictionary, comparer)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SerializableDictionary{T}"/> class.</summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="comparer">The comparer.</param>
        public SerializableDictionary(int capacity, IEqualityComparer<string> comparer)
            : base(capacity, comparer)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SerializableDictionary{T}"/> class.</summary>
        /// <param name="info">   The information.</param>
        /// <param name="context">The context.</param>
        protected SerializableDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
