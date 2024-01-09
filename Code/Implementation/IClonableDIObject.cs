namespace EmptyDI
{
    public interface IClonableDIObject<T>
    {
        T Clone(T clonableObject);
    }
}
