// <copyright file="PricePoint.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the price point class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IPricePoint : ITypableBase
    {
        #region Associated Objects
        /// <summary>Gets or sets the store accounts.</summary>
        /// <value>The store accounts.</value>
        ICollection<StoreAccount>? StoreAccounts { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Pricing", "PricePoint")]
    public class PricePoint : TypableBase, IPricePoint
    {
        private ICollection<StoreAccount>? storeAccounts;

        public PricePoint()
        {
            storeAccounts = new HashSet<StoreAccount>();
        }

        #region Associated Objects
        /// <inheritdoc/>
        [DontMapOutEver, DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<StoreAccount>? StoreAccounts { get => storeAccounts; set => storeAccounts = value; }
        #endregion
    }
}
