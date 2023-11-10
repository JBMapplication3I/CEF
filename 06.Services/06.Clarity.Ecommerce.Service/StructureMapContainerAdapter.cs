// <copyright file="StructureMapContainerAdapter.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the structure map container adapter class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using ServiceStack.Configuration;

    /// <summary>Class StructureMapContainerAdapter.</summary>
    /// <seealso cref="IContainerAdapter"/>
    public class StructureMapContainerAdapter : IContainerAdapter
    {
        public T TryResolve<T>()
        {
            return RegistryLoader.ContainerInstance.TryGetInstance<T>();
        }

        public T Resolve<T>()
        {
            return RegistryLoader.ContainerInstance.TryGetInstance<T>();
        }
    }
}
