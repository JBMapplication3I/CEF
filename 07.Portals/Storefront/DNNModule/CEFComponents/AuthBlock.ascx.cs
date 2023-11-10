// <copyright file="AuthBlock.ascx.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the auth block class</summary>
namespace Clarity.Ecommerce.DNN.Extensions.CEFComponents
{
    using System;

    public partial class AuthBlock : CEFComponentUserControl
    {
        protected string PasswordHashAttributeValue { get; private set; }

        protected string SaltKeyAttributeValue { get; private set; }

        protected string VIKeyAttributeValue { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            PasswordHashAttributeValue = Convert.ToString(Component.GetParameter("PasswordHash")?.Value ?? string.Empty);
            SaltKeyAttributeValue = Convert.ToString(Component.GetParameter("SaltKey")?.Value ?? string.Empty);
            VIKeyAttributeValue = Convert.ToString(Component.GetParameter("VIKey")?.Value ?? string.Empty);
        }
    }
}
