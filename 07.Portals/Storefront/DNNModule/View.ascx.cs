// <copyright file="View.ascx.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the view.ascx class</summary>
namespace Clarity.Ecommerce.DNN.Extensions.CoreModule
{
    using System;
    using DotNetNuke.Entities.Modules;

    public partial class View : PortalModuleBase // , IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ////try
            ////{
            ////}
            ////catch (Exception exc) //Module failed to load
            ////{
            ////    Exceptions.ProcessModuleLoadException(this, exc);
            ////}
        }

        ////public ModuleActionCollection ModuleActions
        ////{
        ////    get
        ////    {
        ////        var actions = new ModuleActionCollection
        ////            {
        ////                {
        ////                    GetNextActionID(), Localization.GetString("EditModule", LocalResourceFile), "", "", "",
        ////                    EditUrl(), false, SecurityAccessLevel.Edit, true, false
        ////                }
        ////            };
        ////        return actions;
        ////    }
        ////}
    }
}
