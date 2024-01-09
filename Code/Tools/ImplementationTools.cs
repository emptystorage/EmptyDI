using System;
using System.Reflection;
using UnityEngine;

using EmptyDI.Code.Implementation;

namespace EmptyDI.Code.Tools
{
    public static class ImplementationTools
    {
        public static MethodBase GetConstructor(Type implementationType)
        {
            MethodBase constructor = null;

            if (implementationType.IsSubclassOf(typeof(MonoBehaviour)))
            {
                if (!TryGetInjectConstructor(implementationType, out constructor))
                    throw new Exception($"Не найден метод с атрибутом Inject в классе - {implementationType.Name}");
            }
            else
            {
                var constructors = implementationType.GetConstructors();

                if (constructors.Length > 1)
                    throw new Exception($"Больше одного конструктора в классе - {implementationType.Name}");

                if(constructors.Length == 0)
                {
                    if (!TryGetInjectConstructor(implementationType, out constructor))
                        throw new Exception($"Не найден метод с атрибутом Inject в классе - {implementationType.Name}");
                }
                else
                {
                    constructor = constructors[default];
                }
            }

            return constructor;
        }

        private static bool TryGetInjectConstructor(Type implementationType, out MethodBase info)
        {
            info = null;
            var methods = implementationType.GetMethods();

            for (int i = 0; i < methods.Length; i++)
            {
                if (methods[i].GetCustomAttribute<InjectAttribute>() != null)
                {
                    info = methods[i];
                    break;
                }
            }

            return info != null;

        }
    }
}
