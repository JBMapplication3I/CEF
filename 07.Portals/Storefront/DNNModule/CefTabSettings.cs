// <copyright file="CEFTabSettings.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF tab settings class</summary>
namespace Clarity.Ecommerce.DNN.Extensions
{
    /// <summary>A CEF tab settings.</summary>
    public class CEFTabSettings
    {
        /// <summary>Gets or sets the identifier of the tab.</summary>
        /// <value>The identifier of the tab.</value>
        public int TabId { get; set; }

        /// <summary>Gets or sets the identifier of the parent.</summary>
        /// <value>The identifier of the parent.</value>
        public int ParentId { get; set; }

        /// <summary>Gets or sets the name of the tab.</summary>
        /// <value>The name of the tab.</value>
        public string TabName { get; set; }

        /// <summary>Gets or sets the title.</summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>Gets or sets the key words.</summary>
        /// <value>The key words.</value>
        public string KeyWords { get; set; }

        /// <summary>Gets or sets a value indicating whether this CEFTabSettings is visible.</summary>
        /// <value>True if this CEFTabSettings is visible, false if not.</value>
        public bool IsVisible { get; set; }

        /// <summary>Gets or sets a value indicating whether this CEFTabSettings is deleted.</summary>
        /// <value>True if this CEFTabSettings is deleted, false if not.</value>
        public bool IsDeleted { get; set; }

        /// <summary>Gets or sets a value indicating whether the link is disabled.</summary>
        /// <value>True if disable link, false if not.</value>
        public bool DisableLink { get; set; }

        /// <summary>Gets or sets URL of the document.</summary>
        /// <value>The URL.</value>
        public string Url { get; set; }

        /// <summary>Gets or sets the skin source.</summary>
        /// <value>The skin source.</value>
        public string SkinSrc { get; set; }

        /// <summary>Gets or sets the container source.</summary>
        /// <value>The container source.</value>
        public string ContainerSrc { get; set; }

        /// <summary>Gets or sets a value indicating whether this CEFTabSettings is super tab.</summary>
        /// <value>True if this CEFTabSettings is super tab, false if not.</value>
        public bool IsSuperTab { get; set; }

        /// <summary>Gets or sets the page header text.</summary>
        /// <value>The page header text.</value>
        public string PageHeaderText { get; set; }
    }
}
