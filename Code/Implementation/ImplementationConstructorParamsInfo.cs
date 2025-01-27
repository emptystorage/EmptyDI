using System;
using System.Collections.Generic;
using System.Reflection;

namespace EmptyDI.Code.Implementation
{
    internal sealed class ImplementationConstructorParamsInfo : IDisposable
    {
        private List<ConstructorParameterInfo> _params = new();

        internal ImplementationConstructorParamsInfo(MethodBase constructor, Type implementationType, Func<Type, ImplementationInfo> onGetImplementationInfo)
        {
            Constructor = constructor;
            OnGetBindedImplementationInfo = onGetImplementationInfo;

            var @params = constructor.GetParameters();

            for (int i = 0; i < @params.Length; i++)
            {
                if (@params[i].ParameterType.IsInterface)
                {
                    var paramsAttribute = @params[i].GetCustomAttribute<AbstractionTypeOfAttribute>();

                    if (paramsAttribute == null)
                        _params.Add(new ConstructorParameterInfo(@params[i].ParameterType));
                    else
                        _params.Add(new ConstructorParameterInfo(paramsAttribute.AbstractionType));
                }
                else
                {
                    _params.Add(new ConstructorParameterInfo(@params[i].ParameterType));
                }
            }
        }

        internal object[] Params
        {
            get
            {
                var result = new object[_params.Count];

                for (int i = 0; i < _params.Count; i++)
                {
                    if(_params[i].paramsValue == null)
                    {
                        var info = OnGetBindedImplementationInfo(_params[i].ParameterType);

                        if (info == null)
                            throw new NotImplementedException($"Не найден объект с типом - {_params[i].ParameterType.Name}, когда была попытка создать класс - {Constructor.ReflectedType.Name}");

                        var @object = info.Implementation<object>();

                        if(info.BindingType == BindingType.Single)
                        {
                            _params[i].paramsValue = @object;
                        }

                        result[i] = @object;
                    }
                    else
                    {
                        result[i] = _params[i].paramsValue;
                    }
                }

                return result;
            }
        }

        internal MethodBase Constructor { get; }        

        private Func<Type, ImplementationInfo> OnGetBindedImplementationInfo;

        internal void ChangeConstructorParametersValue(Queue<ConstructorParameterInfo> parameters)
        {
            var parameterIndexTable = new Dictionary<Type, int>();
            var paramterIndex = 0;

            while(parameters.Count > 0)
            {
                var info = parameters.Dequeue();

                if (parameterIndexTable.TryGetValue(info.ParameterType, out var indexTypeOf))
                {
                    parameterIndexTable[info.ParameterType] = indexTypeOf + 1;
                }
                else
                {
                    parameterIndexTable[info.ParameterType] = paramterIndex;
                }

                paramterIndex++;

                ChangeParameterValue(info, parameterIndexTable[info.ParameterType]);
            }
        }

        internal void ChangeConstructorParametersType(Queue<ConstructorParameterInfo> parameters)
        {
            while(parameters.Count > 0)
            {
                var parameter = parameters.Dequeue();

                for (int i = 0; i < _params.Count; i++)
                {
                    if (parameter.ParameterType.IsInterface | parameter.ParameterType.IsAbstract)
                        throw new Exception($"{parameter.ParameterType.Name} Нельзя использовать абстрактный класс или интерфейс в параметрах конструктора");

                    if (parameter.ParameterType.IsSubclassOf(_params[i].ParameterType)
                        | parameter.ParameterType.GetInterface(_params[i].ParameterType.Name) != null)
                    {
                        _params[i] = parameter;
                        break;
                    }
                }
            }
        }

        internal void ChangeParameterValue(ConstructorParameterInfo parameterInfo, int paramterIndex)
        {
            var info = _params.FindAll(x => x.ParameterType.Equals(parameterInfo.ParameterType) 
                                            || (x.ParameterType.Equals(typeof(Single)) & parameterInfo.ParameterType.Equals(typeof(Int32)))
                                                || (x.ParameterType.Equals(typeof(Int32)) & parameterInfo.ParameterType.Equals(typeof(Single))));

            if (info == null)
                throw new Exception($"Не найден параметр - {parameterInfo.ParameterType} в реализации - {Constructor.ReflectedType.Name}");

            info[paramterIndex].paramsValue = parameterInfo.paramsValue;
        }

        public void Dispose()
        {
            OnGetBindedImplementationInfo = null;

            _params.ForEach(x => x.Dispose());
            _params = null;

            GC.SuppressFinalize(this);
        }
    }
}
