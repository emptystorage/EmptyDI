using System;

namespace EmptyDI
{
    /// <summary>
    /// Атрибут для задачи метода конструктора в MonoBehaviour
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public sealed class InjectAttribute : Attribute
    {
    }
}
