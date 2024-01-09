﻿using System.Collections;
using UnityEngine;

namespace EmptyDI.Code.Context
{
    public abstract class BaseContext : MonoBehaviour
    {
        [Header("Mono Installers")]
        [SerializeField] private MonoInstaller[] _monoInstallers;

        [Header("Scriptable Obejct Installers")]
        [SerializeField] private ScriptableObjectInstaller[] _scriptableObjectInstallers;

        [SerializeField] private ExecuteBindType _executeBindType = ExecuteBindType.Start;


        private void Awake()
        {
            ContextCreated();

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

        private void ExecuteBind()
        {
            ExecuteInstallers(_monoInstallers);
            ExecuteInstallers(_scriptableObjectInstallers);
        }

        protected virtual void ContextCreated() { }

        private void ExecuteInstallers(IInstaller[] installers)
        {
            for (int i = 0; i < installers.Length; i++)
            {
                installers[i].Install();
            }

            for (int i = 0; i < installers.Length; i++)
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
