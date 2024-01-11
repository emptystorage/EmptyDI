using System;

using EmptyDI.Code.Implementation;
using EmptyDI.Code.Locator;
using EmptyDI.Code.DIContainer;

namespace EmptyDI.Code.BindBuilder
{
    public partial class SingleBindBuilder<T> : IBindBuilder
        where T : class
    {
        private readonly ImplementationInfo Info;
        private readonly string ContainerTag;

        private bool _isCreateNow;
        private bool _isTransitLock;

        internal SingleBindBuilder(string containerTag, ImplementationInfo info, bool isTransitLock = false)
        {
            Info = info;
            ContainerTag = containerTag;
            _isTransitLock = isTransitLock;
        }

        internal SingleBindBuilder(string containerTag, T @object = null)
        {
            var containerBank = InternalLocator.GetObject<ContainerBank>();
            var transitImplementationbank = InternalLocator.GetObject<TransitImplementationBank>();

            Info = new ImplementationInfo(@object, typeof(T), transitImplementationbank, containerBank.FindImplementation);
            ContainerTag = containerTag;
        }
        /// <summary>
        /// Создать объект незамедлительно
        /// </summary>
        /// <returns></returns>
        public SingleBindBuilder<T> IsNowCreate()
        {
            _isCreateNow = true;
            return this;
        }
        /// <summary>
        /// Объект единичный в приложение
        /// </summary>
        public void AsSingle()
        {
            Info.BindingType = BindingType.Single;
        }
        /// <summary>
        /// Объект множественный в приложение 
        /// </summary>
        public void AsTransit()
        {
            Info.BindingType = BindingType.Transit;

            if (Info.IsImplementented
                    && !typeof(T).IsSubclassOf(typeof(UnityEngine.MonoBehaviour)) 
                        && typeof(T).GetInterface(typeof(IClonableDIObject<T>).Name) == null)
                                throw new System.Exception($"Нельзя добавлять зависимость для типа - {typeof(T).Name} с использовать предустановленного объект реализации без интерфейса IClonableDIObject");
        }

        void IBindBuilder.Build()
        {
            if (_isTransitLock)
            {
                Info.BindingType = BindingType.Transit;
            }

            var container = InternalLocator.GetObject<ContainerBank>().Get(ContainerTag);
            container.AddImplementationInfo(typeof(T), Info);

            if (_isCreateNow)
            {
                var @object = Info.Implementation<T>();
            }                
        }

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
