using System;

namespace EmptyDI
{
    /// <summary>
    /// Атрибут для задачи абстракции в параметрах конструктора объекта
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Parameter, Inherited = true)]
    public sealed class AbstractionTypeOfAttribute : Attribute
    {
        public readonly Type AbstractionType;

        public AbstractionTypeOfAttribute(Type abstractionType)
        {
            AbstractionType = abstractionType;
        }
    }
}
