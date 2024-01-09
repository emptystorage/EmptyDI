using System;
using EmptyDI.Code.Tools;

namespace EmptyDI.Code.Implementation
{
    public enum BindingType
    {
        Single,
        Transit
    }

    public sealed class ImplementationInfo : IDisposable
    {
        private readonly Type ImplementationType;

        private object _implementation;
        private bool _isDisposable;

        public ImplementationInfo(object implementation, Type implementationType, Func<Type, ImplementationInfo> onGetImplementationCallback)
        {
            ImplementationType = implementationType;
            ParamsInfo = new ImplementationConstructorParamsInfo(
                                        ImplementationTools.GetConstructor(implementationType), 
                                        implementationType, 
                                        onGetImplementationCallback);

            _implementation = implementation;
            _isDisposable = implementationType.GetInterface(nameof(IDisposable)) != null;
        }

        public ImplementationConstructorParamsInfo ParamsInfo { get; }

        public BindingType BindingType { get; set; }

        public T Implementation<T>()
            where T : class
        {
            if (BindingType == BindingType.Single)
            {
                if (_implementation == null)
                {
                    _implementation = new ImplementationFactory().Create<T>(ImplementationType, ParamsInfo, _implementation);
                }

                return _implementation as T;
            }
            else
            {

                return new ImplementationFactory().Clone<T>(ImplementationType, ParamsInfo, _implementation) as T;
            }
        }

        public void Dispose()
        {
            if (_isDisposable & _implementation != null)
            {
                var disposableImplementation = _implementation as IDisposable;
                disposableImplementation.Dispose();
            }

            _implementation = null;
            ParamsInfo.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
