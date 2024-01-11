using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using EmptyDI.Pool;
using EmptyDI.Code.Locator;
using EmptyDI.Code.DIContainer;
using EmptyDI.Code.Implementation;

namespace EmptyDI.Code.BindBuilder
{
    public sealed class PoolBindBuilder<P, T> : IBindBuilder
        where P : DIPool<T>, new()
        where T : class
    {
        private readonly IBindBuilder PoolBuilder;

        internal PoolBindBuilder(T implementation)
        {
            var containerBank = InternalLocator.GetObject<ContainerBank>();
            var transitBank = InternalLocator.GetObject<TransitImplementationBank>();
            var objectInfo = new ImplementationInfo(implementation, typeof(T), transitBank, containerBank.FindImplementation);
            var pool = new P();
            pool.Info = objectInfo;

            ObjectBuilder = new SingleBindBuilder<T>("poolable_object", objectInfo, true);
            PoolBuilder = new SingleBindBuilder<P>("pools", new ImplementationInfo(pool, typeof(P), transitBank, containerBank.FindImplementation));
        }

        public SingleBindBuilder<T> ObjectBuilder { get; }

        void IBindBuilder.Build()
        {
            using (var builder = ObjectBuilder as IBindBuilder)
            {
                builder.Build();
            }

            PoolBuilder.Build();
            PoolBuilder.Dispose();
        }

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
