using EmptyDI.Code.BindBuilder;

namespace EmptyDI
{
    public interface IInstaller
    {        
        void Install();
        void AddBindBuilder(IBindBuilder builder);
        void CompleteBind();
    }
}
