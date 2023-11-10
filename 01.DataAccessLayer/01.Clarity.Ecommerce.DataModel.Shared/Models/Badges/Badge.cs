// <copyright file="Badge.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the badge class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IBadge
        : INameableBase,
            IHaveATypeBase<BadgeType>,
            IHaveImagesBase<Badge, BadgeImage, BadgeImageType>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Badges", "Badge")]
    public class Badge : NameableBase, IBadge
    {
        private ICollection<BadgeImage>? images;

        public Badge()
        {
            // HaveImagesBase
            images = new HashSet<BadgeImage>();
        }

        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual BadgeType? Type { get; set; }
        #endregion

        #region HaveImagesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<BadgeImage>? Images { get => images; set => images = value; }
        #endregion
    }
}
