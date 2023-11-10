// <copyright file="DistrictImage.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the district image class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IDistrictImage : IImageBase<District, DistrictImageType>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Geography", "DistrictImage")]
    public class DistrictImage : ImageBase, IDistrictImage
    {
        #region IImageBase
        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public override int? MasterID { get; set; }

        /// <inheritdoc/>
        [DontMapInWithRelateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual District? Master { get; set; }
        #endregion

        #region IHaveATypeBase Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public override int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual DistrictImageType? Type { get; set; }
        #endregion
        #endregion
    }
}
