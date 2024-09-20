using EmptyDI.Code.BindBuilder;
using System;

namespace EmptyDI
{
    public interface IInstaller
    {
        event Action InstallCompleted;

        void Install();
        void AddBindBuilder(IBindBuilder builder);
        void CompleteBind();
    }
}
