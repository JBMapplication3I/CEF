// <copyright file="AddressCollection.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the address collection class</summary>
namespace Microsoft.Owin.BuilderProperties
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>Wraps the host.Addresses list.</summary>
    public struct AddressCollection : IEnumerable<Address>, IEnumerable
    {
        /// <summary>Gets the number of elements in the collection.</summary>
        /// <value>The number of elements in the collection.</value>
        public int Count => List.Count;

        /// <summary>Gets the item with the specified index from the collection.</summary>
        /// <param name="index">The index.</param>
        /// <returns>The item with the specified index.</returns>
        public Address this[int index]
        {
            get => new(List[index]);
            set => List[index] = value.Dictionary;
        }

        /// <summary>Gets the underlying address list.</summary>
        /// <value>The underlying address list.</value>
        public IList<IDictionary<string, object>> List { get; }

        /// <summary>Initializes a new instance of the
        /// <see cref="AddressCollection" /> class.</summary>
        /// <param name="list">The address list to set to the collection.</param>
        public AddressCollection(IList<IDictionary<string, object>> list)
        {
            List = list;
        }

        /// <summary>Adds the specified address to the collection.</summary>
        /// <param name="address">The address to add to the collection.</param>
        public void Add(Address address)
        {
            List.Add(address.Dictionary);
        }

        /// <summary>Creates a new empty instance of <see cref="AddressCollection" />.</summary>
        /// <returns>A new empty instance of <see cref="AddressCollection" />.</returns>
        public static AddressCollection Create()
        {
            return new AddressCollection(new List<IDictionary<string, object>>());
        }

        /// <summary>Determines whether the current collection is equal to the specified collection.</summary>
        /// <param name="other">The other collection to compare to the current collection.</param>
        /// <returns>true if current collection is equal to the specified collection; otherwise, false.</returns>
        public bool Equals(AddressCollection other)
        {
            return Equals(List, other.List);
        }

        /// <summary>Determines whether the current collection is equal to the specified object.</summary>
        /// <param name="obj">The object to compare to the current collection.</param>
        /// <returns>true if current collection is equal to the specified object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is AddressCollection))
            {
                return false;
            }
            return Equals((AddressCollection)obj);
        }

        /// <summary>Gets the enumerator that iterates through the collection.</summary>
        /// <returns>The enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<Address> GetEnumerator()
        {
            foreach (var list in List)
            {
                yield return new Address(list);
            }
        }

        /// <summary>Gets the hash code for this instance.</summary>
        /// <returns>The hash code for this instance.</returns>
        public override int GetHashCode()
        {
            if (List == null)
            {
                return 0;
            }
            return List.GetHashCode();
        }

        /// <summary>Determines whether the first collection is equal to the second collection.</summary>
        /// <param name="left"> The first collection to compare.</param>
        /// <param name="right">The second collection to compare.</param>
        /// <returns>true if both collections are equal; otherwise, false.</returns>
        public static bool operator ==(AddressCollection left, AddressCollection right)
        {
            return left.Equals(right);
        }

        /// <summary>Determines whether the first collection is not equal to the second collection.</summary>
        /// <param name="left"> The first collection to compare.</param>
        /// <param name="right">The second collection to compare.</param>
        /// <returns>true if both collections are not equal; otherwise, false.</returns>
        public static bool operator !=(AddressCollection left, AddressCollection right)
        {
            return !left.Equals(right);
        }

        /// <summary>Gets the enumerator that iterates through the collection.</summary>
        /// <returns>The enumerator that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Address>)this).GetEnumerator();
        }
    }
}
