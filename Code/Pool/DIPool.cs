using System;
using System.Collections.Generic;

using EmptyDI.Code.Implementation;

namespace EmptyDI.Pool
{
    public abstract class DIPool<T> : IDisposable
        where T : class
    {
        private readonly Stack<T> ObjectStack = new();

        public DIPool() { }

        internal ImplementationInfo Info { get; set; }

        public T Spawn()
        {
            var @object = ObjectStack.Count > 0
                                    ? ObjectStack.Pop()
                                    : Info.Implementation<T>();

            OnSpawn(@object);
            return @object;
        }

        public void Despawn(T @object)
        {
            OnDespawn(@object);
            ObjectStack.Push(@object);
        }

        protected virtual void OnSpawn(T @object) { }
        protected virtual void OnDespawn(T @object) { }

        public void Dispose()
        {
            ObjectStack.Clear();

            GC.SuppressFinalize(this);
        }
    }
}
