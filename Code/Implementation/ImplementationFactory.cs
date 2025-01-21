using System;
using UnityEngine;

using Object = UnityEngine.Object;

namespace EmptyDI.Code.Implementation
{
    internal ref struct ImplementationFactory
    {
        internal object Create<T>(Type type, ImplementationConstructorParamsInfo constructParameter, object implementation, bool isMonoObject)
            where T : class
        {
            if (isMonoObject)
            {
                var @object = (implementation == null)
                                    ? new GameObject().AddComponent(type)
                                    : MonoBehaviour.Instantiate(implementation as Object);

                constructParameter.Constructor.Invoke(@object, constructParameter.Params);
                return @object;
            }
            else
            {
                return Activator.CreateInstance(type, constructParameter.Params);
            }
        }

        internal object Clone<T>(Type type, ImplementationConstructorParamsInfo constructParameter, object implementation, bool isMonoObject)
            where T : class
        {
            if (isMonoObject)
            {
                var @object = (implementation == null)
                                    ? new GameObject().AddComponent(type)
                                    : MonoBehaviour.Instantiate(implementation as Object);

                constructParameter.Constructor.Invoke(@object, constructParameter.Params);

                return @object;
            }
            else
            {
                var @object = (implementation == null)
                                    ? Create<T>(type, constructParameter, implementation, isMonoObject)
                                    : ((IClonableDIObject<T>)implementation).Clone(implementation as T);
                return @object;
            }
        }
    }
}
