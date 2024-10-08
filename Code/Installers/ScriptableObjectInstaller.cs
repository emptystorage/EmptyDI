﻿using System;
using System.Collections.Generic;
using UnityEngine;

using EmptyDI.Code.BindBuilder;

namespace EmptyDI
{
    public abstract class ScriptableObjectInstaller : ScriptableObject, IInstaller
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

        private void Reset()
        {
            name = GetType().Name;
        }
    }
}