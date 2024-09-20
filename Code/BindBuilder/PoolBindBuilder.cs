using EmptyDI.Pool;
using EmptyDI.Code.Locator;
using EmptyDI.Code.DIContainer;
using EmptyDI.Code.Implementation;
using System;

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
            var objectInfo = new ImplementationInfo(implementation, typeof(T), transitBank, containerBank.FindImplementation);
            var pool = new P();
            pool.Info = objectInfo;

            ObjectBuilder = new SingleBindBuilder<T>(executedInstaller, "pool_object", objectInfo, true);
            PoolBuilder = new SingleBindBuilder<P>(executedInstaller, "pools", new ImplementationInfo(pool, typeof(P), transitBank, containerBank.FindImplementation));
        }

        public SingleBindBuilder<T> ObjectBuilder { get; }

        public Type Type => typeof(P);

        void IBindBuilder.Build()
        {
            PoolBuilder.Build();
        }
    }
}
