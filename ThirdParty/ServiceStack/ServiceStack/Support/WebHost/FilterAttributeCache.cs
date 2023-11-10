using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ServiceStack.Web;

namespace ServiceStack.Support.WebHost
{
    public static class FilterAttributeCache
    {
        private static Dictionary<Type, IHasRequestFilter[]> requestFilterAttributes
            = new();

        private static Dictionary<Type, IHasResponseFilter[]> responseFilterAttributes
            = new();

        private static IHasRequestFilter[] ShallowCopy(this IHasRequestFilter[] filters)
        {
            var to = new IHasRequestFilter[filters.Length];
            for (var i = 0; i < filters.Length; i++)
            {
                to[i] = filters[i].Copy();
            }
            return to;
        }

        private static IHasResponseFilter[] ShallowCopy(this IHasResponseFilter[] filters)
        {
            var to = new IHasResponseFilter[filters.Length];
            for (var i = 0; i < filters.Length; i++)
            {
                to[i] = filters[i].Copy();
            }
            return to;
        }

        public static IHasRequestFilter[] GetRequestFilterAttributes(Type requestDtoType)
        {
            if (requestFilterAttributes.TryGetValue(requestDtoType, out var attrs))
            {
                return attrs.ShallowCopy();
            }

            var attributes = requestDtoType.AllAttributes().OfType<IHasRequestFilter>().ToList();

            var serviceType = HostContext.Metadata.GetServiceTypeByRequest(requestDtoType);
            if (serviceType != null)
            {
                attributes.AddRange(serviceType.AllAttributes().OfType<IHasRequestFilter>());
            }

            attributes.Sort((x, y) => x.Priority - y.Priority);
            attrs = attributes.ToArray();

            Dictionary<Type, IHasRequestFilter[]> snapshot, newCache;
            do
            {
                snapshot = requestFilterAttributes;
                newCache = new(requestFilterAttributes) { [requestDtoType] = attrs };
            } while (!ReferenceEquals(
                Interlocked.CompareExchange(ref requestFilterAttributes, newCache, snapshot), snapshot));

            return attrs.ShallowCopy();
        }

        public static IHasResponseFilter[] GetResponseFilterAttributes(Type requestDtoType)
        {
            if (responseFilterAttributes.TryGetValue(requestDtoType, out var attrs))
            {
                return attrs.ShallowCopy();
            }

            var attributes = requestDtoType.AllAttributes().OfType<IHasResponseFilter>().ToList();

            var serviceType = HostContext.Metadata.GetServiceTypeByRequest(requestDtoType);
            if (serviceType != null)
            {
                attributes.AddRange(serviceType.AllAttributes().OfType<IHasResponseFilter>());
            }

            attributes.Sort((x, y) => x.Priority - y.Priority);
            attrs = attributes.ToArray();

            Dictionary<Type, IHasResponseFilter[]> snapshot, newCache;
            do
            {
                snapshot = responseFilterAttributes;
                newCache = new(responseFilterAttributes)
                {
                    [requestDtoType] = attrs
                };

            } while (!ReferenceEquals(
                Interlocked.CompareExchange(ref responseFilterAttributes, newCache, snapshot), snapshot));

            return attrs.ShallowCopy();
        }
    }
}
