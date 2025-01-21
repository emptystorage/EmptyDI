using System;
using EmptyDI.Pool;
using EmptyDI.Code.Locator;
using EmptyDI.Code.DIContainer;
using EmptyDI.Code.Implementation;
using EmptyDI.Code.Tools;
using Unity.Plastic.Newtonsoft.Json.Linq;

namespace EmptyDI.Code.BindBuilder
{
    public struct PoolBindBuilder<P, T> : IBindBuilder
        where P : DIPool<T>, new()
        where T : class
    {
        private readonly IBindBuilder PoolBuilder;

        internal PoolBindBuilder(IInstaller executedInstaller, T implementation)
        {
            var containerBank = InternalLocator.GetObject<ContainerBank>();
            var transitBank = InternalLocator.GetObject<TransitImplementationBank>();
            var pool = new P();

            //OBJECT
            {
                var paramsInfo = new ImplementationConstructorParamsInfo(ImplementationTools.GetConstructor(typeof(T)), typeof(T), containerBank.FindImplementation);
                var objectInfo = new ImplementationInfo(implementation, typeof(T), paramsInfo, transitBank);
                objectInfo.BindingType = BindingType.Transit;
                pool.Info = objectInfo;
            }

            //POOL
            {
                var paramsInfo = new ImplementationConstructorParamsInfo(ImplementationTools.GetConstructor(typeof(P)), typeof(P), containerBank.FindImplementation);
                PoolBuilder = new SingleBindBuilder<P>(executedInstaller, "pools", new ImplementationInfo(pool, typeof(P), paramsInfo, transitBank));
            }
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

        internal PoolBindBuilder(IInstaller executedInstaller, T[] implementations, Func<T, K> getKeyCallback)
        {

            if(getKeyCallback == null)
                throw new ArgumentNullException($"Не указан метод-аргумент по получению ключа типа - {typeof(K).Name} для объектов типа-{typeof(T).Name} в пуле типа - {typeof(P).Name}");


            var containerBank = InternalLocator.GetObject<ContainerBank>();
            var transitBank = InternalLocator.GetObject<TransitImplementationBank>();
            var pool = new P();

            //OBJECT
            {
                var paramsInfo = new ImplementationConstructorParamsInfo(ImplementationTools.GetConstructor(typeof(T)), typeof(T), containerBank.FindImplementation);

                for (int i = 0; i < implementations.Length; i++)
                {
                    var objectInfo = new ImplementationInfo(implementations[i], typeof(T), paramsInfo, transitBank);
                    objectInfo.BindingType = BindingType.Transit;
                    pool.AddImplamintationInfo(getKeyCallback.Invoke(implementations[i]), objectInfo);
                }
            }

            //POOL
            {
                var paramsInfo = new ImplementationConstructorParamsInfo(ImplementationTools.GetConstructor(typeof(P)), typeof(P), containerBank.FindImplementation);
                PoolBuilder = new SingleBindBuilder<P>(executedInstaller, "pools", new ImplementationInfo(pool, typeof(P), paramsInfo, transitBank));
            }
        }

        public Type Type => typeof(P);


        void IBindBuilder.Build()
        {
            PoolBuilder.Build();
        }
    }
}
