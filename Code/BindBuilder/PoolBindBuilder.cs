using EmptyDI.Pool;
using EmptyDI.Code.Locator;
using EmptyDI.Code.DIContainer;
using EmptyDI.Code.Implementation;

namespace EmptyDI.Code.BindBuilder
{
    public struct PoolBindBuilder<P, T> : IBindBuilder
        where P : DIPool<T>, new()
        where T : class
    {
        private readonly IInstaller ExecutedInstaller;
        private readonly IBindBuilder PoolBuilder;

        internal PoolBindBuilder(IInstaller executedInstaller, T implementation)
        {
            var containerBank = InternalLocator.GetObject<ContainerBank>();
            var transitBank = InternalLocator.GetObject<TransitImplementationBank>();
            var objectInfo = new ImplementationInfo(implementation, typeof(T), transitBank, containerBank.FindImplementation);
            var pool = new P();
            pool.Info = objectInfo;

            ID = typeof(P).GetHashCode();
            ExecutedInstaller = executedInstaller;
            ObjectBuilder = new SingleBindBuilder<T>(executedInstaller, "poolable_object", objectInfo, true);
            PoolBuilder = new SingleBindBuilder<P>(executedInstaller, "pools", new ImplementationInfo(pool, typeof(P), transitBank, containerBank.FindImplementation));
        }

        public int ID { get; }
        public SingleBindBuilder<T> ObjectBuilder { get; }

        void IBindBuilder.Build()
        {
            PoolBuilder.Build();
        }
    }
}
