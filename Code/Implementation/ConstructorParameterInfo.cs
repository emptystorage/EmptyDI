using System;

namespace EmptyDI.Code.Implementation
{
    public sealed class ConstructorParameterInfo : IDisposable
    {
        public readonly Type ParameterType;

        public object paramsValue;

        public ConstructorParameterInfo(Type parameterType)
        {
            ParameterType = parameterType;
        }

        public ConstructorParameterInfo(Type parameterType, object value)
        {
            ParameterType = parameterType;
            paramsValue = value;
        }

        public void Dispose()
        {
            paramsValue = default;

            GC.SuppressFinalize(this);
        }
    }
}
