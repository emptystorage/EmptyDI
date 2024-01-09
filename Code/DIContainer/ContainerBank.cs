using System;
using System.Collections.Generic;

using EmptyDI.Code.Locator;
using EmptyDI.Code.Implementation;

namespace EmptyDI.Code.DIContainer
{
    public sealed class ContainerBank : ILocatorObject
    {
        private Dictionary<string, Container> _containerCollection = new();

        public Container Get(string tag)
        {
            if(!_containerCollection.TryGetValue(tag, out var container))
            {
                container = new();
                _containerCollection.Add(tag, container);
            }

            return container;
        }

        public ImplementationInfo FindImplementation<T>() => FindImplementation(typeof(T));

        public ImplementationInfo FindImplementation(Type implementationType)
        {
            foreach (var item in _containerCollection)
            {
                if (item.Value.TryGetImplementationInfo(implementationType, out var info))
                {
                    return info;
                }
            }

            return null;
        }

        public void Remove(string tag)
        {
            if(_containerCollection.TryGetValue(tag, out var container))
            {
                container.Dispose();
                _containerCollection.Remove(tag);
            }
        }
    }
}
