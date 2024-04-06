namespace EmptyDI.Code.BindBuilder
{
    public interface IBindBuilder 
    {
        int ID { get; }
        void Build();
    }
}
