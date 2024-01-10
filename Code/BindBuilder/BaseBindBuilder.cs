using EmptyDI.Code.Implementation;
using EmptyDI.Code.Locator;
using EmptyDI.Code.DIContainer;

namespace EmptyDI.Code.BindBuilder
{
    public partial class BaseBindBuilder<T> : IBindBuilder
        where T : class
    {
        private readonly ImplementationInfo Info;
        private readonly string ContainerTag;

        private bool _isCreateNow;
        private bool _isImplemented;

        public BaseBindBuilder(string containerTag, T @object = null)
        {
            var containerBank = InternalLocator.GetObject<ContainerBank>();
            var transitImplementationbank = InternalLocator.GetObject<TransitImplementationBank>();

            Info = new ImplementationInfo(@object, typeof(T), transitImplementationbank, containerBank.FindImplementation);

            ContainerTag = containerTag;
            _isImplemented = @object != null;
        }
        /// <summary>
        /// Создать объект незамедлительно
        /// </summary>
        /// <returns></returns>
        public BaseBindBuilder<T> IsNowCreate()
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

            if (_isImplemented 
                    && !typeof(T).IsSubclassOf(typeof(UnityEngine.MonoBehaviour)) 
                        && typeof(T).GetInterface(typeof(IClonableDIObject<T>).Name) == null)
                                throw new System.Exception($"Нельзя добавлять зависимость для типа - {typeof(T).Name} с использовать предустановленного объект реализации без интерфейса IClonableDIObject");
        }

        void IBindBuilder.Build()
        {
            var container = InternalLocator.GetObject<ContainerBank>().Get(ContainerTag);
            container.AddImplementationInfo(typeof(T), Info);

            if (_isCreateNow)
            {
                var @object = Info.Implementation<T>();
            }                
        }
    }
}
