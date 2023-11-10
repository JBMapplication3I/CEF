using Funq;
using ServiceStack.Configuration;

namespace ServiceStack.Testing
{
    public class BasicResolver : IResolver
    {
        private readonly Container container;

        public BasicResolver() : this(new()) { }

        public BasicResolver(Container container)
        {
            this.container = container;
        }

        public T TryResolve<T>()
        {
            return container.TryResolve<T>();
        }
    }
}