using System;
using EmptyDI.Code.Tools;

using Object = UnityEngine.Object;

namespace EmptyDI.Code.Implementation
{
    public enum BindingType
    {
        Single,
        Transit
    }

    internal sealed class ImplementationInfo : IDisposable
    {
        private readonly Type ImplementationType;
        private readonly TransitImplementationBank Bank;

        private object _implementation;
        private bool _isDisposable;
        private bool _isMonoObject;

        internal ImplementationInfo(
            object implementation, 
            Type implementationType, 
            TransitImplementationBank bank,
            Func<Type, ImplementationInfo> onGetImplementationCallback)
        {
            ImplementationType = implementationType;
            Bank = bank;
            ParamsInfo = new ImplementationConstructorParamsInfo(
                                        ImplementationTools.GetConstructor(implementationType), 
                                        implementationType, 
                                        onGetImplementationCallback);

            _implementation = implementation;
            _isDisposable = implementationType.GetInterface(nameof(IDisposable)) != null;
            _isMonoObject = implementationType.IsSubclassOf(typeof(UnityEngine.MonoBehaviour));
        }

        internal ImplementationConstructorParamsInfo ParamsInfo { get; }
        internal BindingType BindingType { get; set; }

        internal T Implementation<T>()
            where T : class
        {
            if (BindingType == BindingType.Single)
            {
                if (_implementation == null)
                {
                    _implementation = new ImplementationFactory().Create<T>(ImplementationType, ParamsInfo, _implementation, _isMonoObject);
                }

                return _implementation as T;
            }
            else
            {
                var @object = new ImplementationFactory().Clone<T>(ImplementationType, ParamsInfo, _implementation, _isMonoObject);
                Bank.Add(ImplementationType, @object);

                return @object as T;
            }
        }

        public void Dispose()
        {
            if (_isDisposable)
            {
                if(_implementation != null) 
                {
                    var disposableImplementation = _implementation as IDisposable;
                    disposableImplementation.Dispose();
                }                

                if(BindingType == BindingType.Transit)
                {
                    Bank.ForEach(
                        ImplementationType,
                        x =>
                        {
                            var disposedImpl = x as IDisposable;
                            disposedImpl.Dispose();
                        });

                    Bank.Remove(ImplementationType);
                }
            }
            else
            {
                if(BindingType == BindingType.Transit)
                {
                    Bank.Remove(ImplementationType);
                }
            }

            if (_isMonoObject)
            {
                if(BindingType == BindingType.Transit)
                {
                    Bank.ForEach(
                        ImplementationType,
                        x =>
                        {
                            Object.Destroy(x as Object);
                        });

                    Bank.Remove(ImplementationType);
                }
            }

            _implementation = null;
            ParamsInfo.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
