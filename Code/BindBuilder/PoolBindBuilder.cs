using System;
using EmptyDI.Pool;
using EmptyDI.Code.Implementation;

namespace EmptyDI.Code.BindBuilder
{
    public struct PoolBindBuilder<P, T> : IBindBuilder
        where P : DIPool<T>, new()
        where T : class
    {
        private readonly IBindBuilder PoolBuilder;

        internal PoolBindBuilder(IInstaller executedInstaller, T implementation)
        {
            var pool = new P();
            pool.Info = new ImplementationInfoConstructor<T>().Create(implementation);
            pool.Info.BindingType = BindingType.Transit;

            PoolBuilder = new SingleBindBuilder<P>(executedInstaller, "pools", new ImplementationInfoConstructor<P>().Create(pool));
        }

        public Type Type => typeof(P);

        void IBindBuilder.Build()
        {
            PoolBuilder.Build();
        }
    }

    public struct PoolBindBuilder<P, K, T> : IBindBuilder
        where P : DIPool<K, T>, new()
        where T : class
    {
        private readonly IBindBuilder PoolBuilder;

        internal PoolBindBuilder(IInstaller executedInstaller)
        {
            var pool = new P();

            PoolBuilder = new SingleBindBuilder<P>(executedInstaller, "pools", new ImplementationInfoConstructor<P>().Create(pool)); ;
        }

        internal PoolBindBuilder(IInstaller executedInstaller, T[] implementations, Func<T, K> getKeyCallback)
        {

            if(getKeyCallback == null)
                throw new ArgumentNullException($"Не указан метод-аргумент по получению ключа типа - {typeof(K).Name} для объектов типа-{typeof(T).Name} в пуле типа - {typeof(P).Name}");

            var pool = new P();

            PoolBuilder = new SingleBindBuilder<P>(executedInstaller, "pools", new ImplementationInfoConstructor<P>().Create(pool));;

            for (int i = 0; i < implementations.Length; i++)
            {
                pool.Bind(getKeyCallback.Invoke(implementations[i]), implementations[i]);
            }
        }

        public Type Type => typeof(P);


        void IBindBuilder.Build()
        {
            PoolBuilder.Build();
        }
    }
}
