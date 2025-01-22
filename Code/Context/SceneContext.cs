using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using EmptyDI.Code.Locator;
using EmptyDI.Code.DIContainer;

namespace EmptyDI.Code.Context
{
    public sealed class SceneContext : BaseContext
    {
        public Stack<Type> _sceneBindedTypes = new();

        protected override void ContextStarted()
        {
            EmptyDIConnector.BindedObject += OnBindedObject;
        }

        protected override void ContextCompleted()
        {
            EmptyDIConnector.BindedObject -= OnBindedObject;
            SceneManager.sceneUnloaded += SceneIsUnloaded;
        }

        private void OnBindedObject(Type type)
        {
            _sceneBindedTypes.Push(type);
        }

        private void SceneIsUnloaded(Scene scene)
        {
            var bank = InternalLocator.GetObject<ContainerBank>();

            while(_sceneBindedTypes.Count > 0)
            {
                bank.RemoveImplementation(_sceneBindedTypes.Pop());
            }

            SceneManager.sceneUnloaded -= SceneIsUnloaded;
        }
    }
}
