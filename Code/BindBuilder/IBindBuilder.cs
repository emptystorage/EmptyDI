using System;

namespace EmptyDI.Code.BindBuilder
{
    public interface IBindBuilder : IDisposable
    {
        void Build();
    }
}
