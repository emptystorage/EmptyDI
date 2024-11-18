using System.Collections.Generic;
using UnityEngine;

using EmptyDI.Code.BindBuilder;
using System;
using EmptyDI.Code.Context;

namespace EmptyDI
{
    [ExecuteInEditMode]
    public abstract class MonoInstaller : MonoBehaviour, IInstaller
    {
        private Dictionary<Type, IBindBuilder> _builders = new();

        public event Action InstallCompleted;

        public abstract void Install();

        void IInstaller.AddBindBuilder(IBindBuilder builder)
        {
            _builders[builder.Type] = builder;
        }

        void IInstaller.CompleteBind()
        {
            foreach (var item in _builders.Values)
            {
                item.Build();
            }

            _builders.Clear();
            InstallCompleted?.Invoke();
        }

        private void Awake()
        {
            name = GetType().Name;

            var context = GetComponentInParent<BaseContext>();

            if(context != null)
            {
                context.AddInstaller(this);
            }
        }

        private void OnDestroy()
        {
            var context = GetComponentInParent<BaseContext>();

            if(context != null)
            {
                context.RemoveInstaller(this);
            }
        }
    }
}