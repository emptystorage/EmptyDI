using System;
using System.Collections.Generic;

using EmptyDI.Code.Locator;

namespace EmptyDI.Code.Implementation
{
    internal sealed class TransitImplementationBank : ILocatorObject
    {
        private Dictionary<Type, Stack<object>> _bank = new();

        public void Add(Type type, object implementation)
        {
            if(!_bank.TryGetValue(type, out var stack))
            {
                stack = new Stack<object>();
                _bank.Add(type, stack);
            }

            stack.Push(implementation);
        }

        public void Remove(Type type)
        {
            if (_bank.TryGetValue(type, out var stack))
            {
                stack.Clear();
            }

            _bank.Remove(type);
        }

        public void ForEach(Type type, Action<object> callback)
        {
            if(_bank.TryGetValue(type, out var stack))
            {
                while(stack.Count > 0)
                {
                    callback?.Invoke(stack.Pop());
                }
            }
        }

        public IReadOnlyCollection<object> GetList(Type type)
        {
            return _bank.ContainsKey(type)
                            ? _bank[type]
                            : new Stack<object>();
        }
    }
}
