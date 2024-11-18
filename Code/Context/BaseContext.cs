using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EmptyDI.Code.Context
{
    public abstract class BaseContext : MonoBehaviour
    {
        [Header("Mono Installers")]
        [SerializeField] private List<MonoInstaller> _monoInstallers;

        [Header("Scriptable Obejct Installers")]
        [SerializeField] private List<ScriptableObjectInstaller> _scriptableObjectInstallers;

        [SerializeField] private ExecuteBindType _executeBindType = ExecuteBindType.Start;

        private void Reset()
        {
            _monoInstallers = new List<MonoInstaller>();
            _scriptableObjectInstallers = new List<ScriptableObjectInstaller>();
        }

        private void Awake()
        {
            if (_executeBindType != ExecuteBindType.Awake) return;

            ExecuteBind();
        }

        private IEnumerator Start()
        {
            if(_executeBindType == ExecuteBindType.Start)
            {
                ExecuteBind();
            }

            yield return null;

            if(_executeBindType == ExecuteBindType.AfterFrame)
            {
                ExecuteBind();
            }
        }

        internal void AddInstaller(in MonoInstaller installer)
        {
            if (_monoInstallers.Contains(installer)) return;

            _monoInstallers.Add(installer);
        }

        internal void RemoveInstaller(in MonoInstaller installer)
        {
            _monoInstallers.Remove(installer);
        }

        private void ExecuteBind()
        {
            ContextStarted();
            ExecuteInstallers(_monoInstallers);
            ExecuteInstallers(_scriptableObjectInstallers);
            ContextCompleted();
        }

        protected virtual void ContextStarted() { }
        protected virtual void ContextCompleted() { }

        private void ExecuteInstallers(IReadOnlyList<IInstaller> installers)
        {
            for (int i = 0; i < installers.Count; i++)
            {
                installers[i].Install();
            }

            for (int i = 0; i < installers.Count; i++)
            {
                installers[i].CompleteBind();
            }
        }

        private enum ExecuteBindType
        {
            Awake,
            Start,
            AfterFrame
        }
    }
}
