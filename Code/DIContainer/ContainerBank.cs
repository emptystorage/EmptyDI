using System;
using System.Collections.Generic;

using EmptyDI.Code.Locator;
using EmptyDI.Code.Implementation;

namespace EmptyDI.Code.DIContainer
{
    internal sealed class ContainerBank : ILocatorObject
    {
        private Dictionary<string, Container> _containerCollection = new();

        internal Container Get(string tag)
        {
            if(!_containerCollection.TryGetValue(tag, out var container))
            {
                container = new();
                _containerCollection.Add(tag, container);
            }

            return container;
        }

        internal ImplementationInfo FindImplementation<T>() => FindImplementation(typeof(T));

        internal ImplementationInfo FindImplementation(Type implementationType)
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

        internal void RemoveImplementation(Type implementationType)
        {
            foreach(var item in _containerCollection)
            {
                item.Value.RemoveImplementationInfo(implementationType);
            }
        }

        internal void Remove(string tag)
        {
            if(_containerCollection.TryGetValue(tag, out var container))
            {
                container.Dispose();
                _containerCollection.Remove(tag);
            }
        }
    }
}
