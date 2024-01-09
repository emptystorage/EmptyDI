using System;
using UnityEngine;

using Object = UnityEngine.Object;

namespace EmptyDI.Code.Implementation
{
    public struct ImplementationFactory
    {
        public object Create<T>(Type type, ImplementationConstructorParamsInfo constructParameter, object implementation)
            where T : class
        {
            if (constructParameter.IsMonoObject)
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

        public object Clone<T>(Type type, ImplementationConstructorParamsInfo constructParameter, object implementation)
            where T : class
        {
            if (constructParameter.IsMonoObject)
            {
                var @object = (implementation == null)
                                    ? new GameObject().AddComponent(type)
                                    : MonoBehaviour.Instantiate(implementation as Object);

                constructParameter.Constructor.Invoke(@object, constructParameter.Params);

                return @object;
            }
            else
            {
                var @object = implementation as IClonableDIObject<T>;
                return @object.Clone(implementation as T);
            }
        }
    }
}
