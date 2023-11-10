// <copyright file="CEFComponentParameterExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF component parameter extensions class</summary>
namespace Clarity.Ecommerce.DNN.Extensions
{
    public static class CEFComponentParameterExtensions
    {
        public static CEFComponentParameter Clone(this CEFComponentParameter parameter)
        {
            return new CEFComponentParameter
            {
                Name = parameter.Name,
                FriendlyName = parameter.FriendlyName,
                Description = parameter.Description,
                Group = parameter.Group,
                Type = parameter.Type,
                Value = parameter.Value,
                Order = parameter.Order
            };
        }
    }
}
