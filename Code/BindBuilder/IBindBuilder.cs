using System;

namespace EmptyDI.Code.BindBuilder
{
    public interface IBindBuilder 
    {
        Type Type { get; }
        void Build();
    }
}
