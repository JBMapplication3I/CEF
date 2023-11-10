// <copyright file="RegistrationConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base search model class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using System.Collections.Generic;
    using JetBrains.Annotations;

    [PublicAPI]
    public partial class RegistrationConfig
    {
        public Dictionary<string, RegistrationStepConfig>? sections { get; set; }
    }
}
