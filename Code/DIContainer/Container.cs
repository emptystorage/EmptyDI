using System;
using System.Collections.Generic;

using EmptyDI.Code.Implementation;

namespace EmptyDI.Code.DIContainer
{
    public sealed class Container : IDisposable
    {
        private Dictionary<Type, ImplementationInfo> _implementationInfoTable = new();

        public void AddImplementationInfo(Type implementationType, ImplementationInfo implementationInfo)
        {
            if(_implementationInfoTable.ContainsKey(implementationType))
                throw new Exception($"В контейнере есть зависимость - {implementationType.Name}");

            _implementationInfoTable.Add(implementationType, implementationInfo);
        }

        public bool TryGetImplementationInfo(Type implementationType, out ImplementationInfo info)
        {
            _implementationInfoTable.TryGetValue(implementationType, out info);

            return info != null;
        }

        public void Dispose()
        {
            _implementationInfoTable.Clear();

            GC.SuppressFinalize(this);
        }
    }
}