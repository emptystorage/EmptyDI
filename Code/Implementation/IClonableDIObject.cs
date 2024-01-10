using System;

namespace EmptyDI
{
    public interface IClonableDIObject<T> : IDisposable
    {
        T Clone(T clonableObject);
    }
}
