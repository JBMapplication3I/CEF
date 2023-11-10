// <copyright file="AttributeType.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the attribute type class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IAttributeType : ITypableBase
    {
        ICollection<GeneralAttribute>? GeneralAttributes { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Attributes", "AttributeType")]
    public class AttributeType : TypableBase, IAttributeType
    {
        private ICollection<GeneralAttribute>? generalAttributes;

        public AttributeType()
        {
            generalAttributes = new HashSet<GeneralAttribute>();
        }

        #region Associated Objects
        /// <inheritdoc/>
        [DontMapOutEver, DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<GeneralAttribute>? GeneralAttributes { get => generalAttributes; set => generalAttributes = value; }
        #endregion
    }
}
