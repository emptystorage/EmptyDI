using EmptyDI.Code.DIContainer;
using EmptyDI.Code.Locator;
using EmptyDI.Code.Tools;

namespace EmptyDI.Code.Implementation
{
    internal ref struct ImplementationInfoConstructor<T>
    {
        internal ImplementationInfo Create(in T implementation)
        {
            var containerBank = InternalLocator.GetObject<ContainerBank>();
            var transitBank = InternalLocator.GetObject<TransitImplementationBank>();
            var paramsInfo = new ImplementationConstructorParamsInfo(ImplementationTools.GetConstructor(typeof(T)), typeof(T), containerBank.FindImplementation);

            return new ImplementationInfo(implementation, typeof(T), paramsInfo, transitBank);
        }
    }
}
