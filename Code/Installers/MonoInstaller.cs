using System.Collections.Generic;
using UnityEngine;

using EmptyDI.Code.BindBuilder;

namespace EmptyDI
{
    public abstract class MonoInstaller : MonoBehaviour, IInstaller
    {
        private Queue<IBindBuilder> _builderQueue = new();

        public abstract void Install();

        void IInstaller.AddBindBuilder(IBindBuilder builder)
        {
            _builderQueue.Enqueue(builder);
        }

        void IInstaller.CompleteBind()
        {
            while (_builderQueue.Count > 0)
            {
                using(var builder = _builderQueue.Dequeue())
                {
                    builder.Build();
                }
            }
        }

        private void Reset()
        {
            name = GetType().Name;
        }
    }
}