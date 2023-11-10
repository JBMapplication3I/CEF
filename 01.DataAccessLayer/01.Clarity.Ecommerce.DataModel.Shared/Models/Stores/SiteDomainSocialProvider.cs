// <copyright file="SiteDomainSocialProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the social media site domain class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ISiteDomainSocialProvider : IAmARelationshipTable<SiteDomain, SocialProvider>
    {
        /// <summary>Gets or sets the script.</summary>
        /// <value>The script.</value>
        string? Script { get; set; }

        /// <summary>Gets or sets the URL values.</summary>
        /// <value>The URL values.</value>
        string? UrlValues { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Stores", "SiteDomainSocialProvider")]
    public class SiteDomainSocialProvider : Base, ISiteDomainSocialProvider
    {
        #region IAmARelationshipTable<SiteDomain, SocialProvider>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual SiteDomain? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual SocialProvider? Slave { get; set; }
        #endregion

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? Script { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), DefaultValue(null)]
        public string? UrlValues { get; set; }
    }
}
