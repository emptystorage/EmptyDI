using System.Collections.Generic;
using UnityEngine;

using EmptyDI.Code.BindBuilder;

namespace EmptyDI
{
    public abstract class MonoInstaller : MonoBehaviour, IInstaller
    {
        private Dictionary<int, IBindBuilder> _builderTable = new();

        public abstract void Install();

        void IInstaller.AddBindBuilder(IBindBuilder builder)
        {
            _builderTable[builder.ID] = builder;
        }

        void IInstaller.CompleteBind()
        {
            foreach (var item in _builderTable)
            {
                item.Value.Build();
            }

            _builderTable.Clear();
        }

        private void Reset()
        {
            name = GetType().Name;
        }
    }
}