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

    public abstract class DIPool<K, T> : IDisposable
        where T : class
    {
        private readonly Dictionary<K, Stack<T>> TableObjectStack = new();
        private readonly Dictionary<K, ImplementationInfo> TableInfo = new();

        public DIPool() { }

        public T Spawn(K @key)
        {
            if (!TableInfo.TryGetValue(@key, out var info))
            {
                throw new Exception($"Не найдена информация о реализции типа - {typeof(T).Name} под ключом - {@key}");
            }
            else
            {
                if (TableObjectStack.TryGetValue(key, out var stack))
                {
                    var @object = stack.Count > 0
                                        ? stack.Pop()
                                        : info.Implementation<T>();
                    OnSpawn(@object);
                    return @object;
                }
                else
                {
                    var @object = info.Implementation<T>();
                    OnSpawn(@object);

                    return @object;
                }
            }
        }

        public void Despawn(K @key, T @object)
        {
            if (!TableObjectStack.TryGetValue(@key, out var stack))
            {
                stack = new();
                TableObjectStack.Add(@key, stack);
            }

            stack.Push(@object);
            OnDespawn(@object);
        }

        internal void AddImplamintationInfo(K key, in ImplementationInfo info)
        {
            TableInfo[key] = info;
        }

        protected virtual void OnSpawn(T @object) { }
        protected virtual void OnDespawn(T @object) { }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
