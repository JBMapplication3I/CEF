// <copyright file="IDataSerializer`1.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IDataSerializer`1 interface</summary>
namespace Microsoft.Owin.Security.DataHandler.Serializer
{
    /// <summary>Interface for data serializer.</summary>
    /// <typeparam name="TModel">Type of the model.</typeparam>
    public interface IDataSerializer<TModel>
    {
        /// <summary>Deserialize this IDataSerializer{TModel} to the given stream.</summary>
        /// <param name="data">The data.</param>
        /// <returns>A TModel.</returns>
        TModel Deserialize(byte[] data);

        /// <summary>Serialize this IDataSerializer{TModel} to the given stream.</summary>
        /// <param name="model">The model.</param>
        /// <returns>A byte[].</returns>
        byte[] Serialize(TModel model);
    }
}
