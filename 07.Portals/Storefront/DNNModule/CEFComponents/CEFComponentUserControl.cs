// <copyright file="CEFComponentUserControl.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cef component user control class</summary>
namespace Clarity.Ecommerce.DNN.Extensions.CEFComponents
{
    using System.Web.UI;
    using DotNetNuke.Entities.Modules;

    public class CEFComponentUserControl : UserControl
    {
        // ReSharper disable once StyleCop.SA1306, StyleCop.SA1401
        protected ModuleInfo ModuleConfiguration;

        // ReSharper disable once StyleCop.SA1306, StyleCop.SA1401
        protected CEFComponent Component;

        // ReSharper disable once InconsistentNaming, StyleCop.SA1306, StyleCop.SA1310, StyleCop.SA1401
        protected string CEF_UI_Base;

        public void Initialize(ModuleInfo moduleConfiguration, CEFComponent component, string cefUIBase)
        {
            ModuleConfiguration = moduleConfiguration;
            Component = component;
            CEF_UI_Base = cefUIBase;
        }
    }
}
