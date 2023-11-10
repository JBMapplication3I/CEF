// <copyright file="IService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IService interface</summary>
namespace Clarity.Ecommerce.SignalRCore
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IService
    {
        Task<TModel?> GetAsync<TModel>(string endpoint, Dictionary<string, object> urlParameters);

        Task<TModel?> PostAsync<TModel>(string endpoint, TModel model);

        ////Task<TModel?> GetAsync<TModel>(string endpoint);

        ////Task<List<TModel?>> PostAsync<TModel>(string endpoint, BaseSearchModel search);
    }
}
