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
            EmptyDIConnector.OnBindObject += ObjectBinded;
        }

        protected override void ContextCompleted()
        {
            EmptyDIConnector.OnBindObject -= ObjectBinded;
            SceneManager.sceneUnloaded += SceneIsUnloaded;
        }

        private void ObjectBinded(Type type)
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
