using System.Collections.Generic;
using EmptyDI.Code.Implementation;

namespace EmptyDI.Code.BindBuilder
{
    public partial struct SingleBindBuilder<T> : IBindBuilder
        where T : class
    {
        /// <summary>
        /// Задать параметры в конструкторе объекта
        /// </summary>
        /// <typeparam name="T1">Тип параметра</typeparam>
        /// <param name="value">Значение параметра</param>
        /// <returns></returns>
        public SingleBindBuilder<T> WhereParameters<T1>(T1 value)
        {
            var parameters = new Queue<ConstructorParameterInfo>();
            parameters.Enqueue(new ConstructorParameterInfo(typeof(T1), value));

            Info.ParamsInfo.ChangeConstructorParametersValue(parameters);
            ExecutedInstaller.AddBindBuilder(this);
            return this;
        }
        /// <summary>
        /// Задать параметры в конструкторе объекта
        /// </summary>
        /// <typeparam name="T1">Тип параметра 1</typeparam>
        /// <typeparam name="T2">Тип параметра 2</typeparam>
        /// <param name="value1">Значение параметра 1</param>
        /// <param name="value2">Значение параметра 2</param>
        /// <returns></returns>
        public SingleBindBuilder<T> WhereParameters<T1, T2>(T1 value1, T2 value2)
        {
            var parameters = new Queue<ConstructorParameterInfo>();
            parameters.Enqueue(new ConstructorParameterInfo(typeof(T1), value1));
            parameters.Enqueue(new ConstructorParameterInfo(typeof(T2), value2));

            Info.ParamsInfo.ChangeConstructorParametersValue(parameters);
            ExecutedInstaller.AddBindBuilder(this);
            return this;
        }
        /// <summary>
        /// Задать параметры в конструкторе объекта
        /// </summary>
        /// <typeparam name="T1">Тип параметра 1</typeparam>
        /// <typeparam name="T2">Тип параметра 2</typeparam>
        /// <typeparam name="T3">Тип параметра 3</typeparam>
        /// <param name="value1">Значение параметра 1</param>
        /// <param name="value2">Значение параметра 2</param>
        /// <param name="value3">Значение параметра 3</param>
        /// <returns></returns>
        public SingleBindBuilder<T> WhereParameters<T1, T2, T3>(T1 value1, T2 value2, T3 value3)
        {
            var parameters = new Queue<ConstructorParameterInfo>();
            parameters.Enqueue(new ConstructorParameterInfo(typeof(T1), value1));
            parameters.Enqueue(new ConstructorParameterInfo(typeof(T2), value2));
            parameters.Enqueue(new ConstructorParameterInfo(typeof(T3), value3));

            Info.ParamsInfo.ChangeConstructorParametersValue(parameters);
            ExecutedInstaller.AddBindBuilder(this);
            return this;
        }
        /// <summary>
        /// Задать параметры в конструкторе объекта
        /// </summary>
        /// <typeparam name="T1">Тип параметра 1</typeparam>
        /// <typeparam name="T2">Тип параметра 2</typeparam>
        /// <typeparam name="T3">Тип параметра 3</typeparam>
        /// <typeparam name="T4">Тип параметра 4</typeparam>
        /// <param name="value1">Значение параметра 1</param>
        /// <param name="value2">Значение параметра 2</param>
        /// <param name="value3">Значение параметра 3</param>
        /// <param name="value4">Значение параметра 4</param>
        /// <returns></returns>
        public SingleBindBuilder<T> WhereParameters<T1, T2, T3, T4>(T1 value1, T2 value2, T3 value3, T4 value4)
        {
            var parameters = new Queue<ConstructorParameterInfo>();
            parameters.Enqueue(new ConstructorParameterInfo(typeof(T1), value1));
            parameters.Enqueue(new ConstructorParameterInfo(typeof(T2), value2));
            parameters.Enqueue(new ConstructorParameterInfo(typeof(T3), value3));
            parameters.Enqueue(new ConstructorParameterInfo(typeof(T4), value4));

            Info.ParamsInfo.ChangeConstructorParametersValue(parameters);
            ExecutedInstaller.AddBindBuilder(this);
            return this;
        }
        /// <summary>
        /// Задать параметры в конструкторе объекта
        /// </summary>
        /// <typeparam name="T1">Тип параметра 1</typeparam>
        /// <typeparam name="T2">Тип параметра 2</typeparam>
        /// <typeparam name="T3">Тип параметра 3</typeparam>
        /// <typeparam name="T4">Тип параметра 4</typeparam>
        /// <typeparam name="T5">Тип параметра 5</typeparam>
        /// <param name="value1">Значение параметра 1</param>
        /// <param name="value2">Значение параметра 2</param>
        /// <param name="value3">Значение параметра 3</param>
        /// <param name="value4">Значение параметра 4</param>
        /// <param name="value5">Значение параметра 5</param>
        /// <returns></returns>
        public SingleBindBuilder<T> WhereParameters<T1, T2, T3, T4, T5>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
        {
            var parameters = new Queue<ConstructorParameterInfo>();
            parameters.Enqueue(new ConstructorParameterInfo(typeof(T1), value1));
            parameters.Enqueue(new ConstructorParameterInfo(typeof(T2), value2));
            parameters.Enqueue(new ConstructorParameterInfo(typeof(T3), value3));
            parameters.Enqueue(new ConstructorParameterInfo(typeof(T4), value4));
            parameters.Enqueue(new ConstructorParameterInfo(typeof(T5), value5));

            Info.ParamsInfo.ChangeConstructorParametersValue(parameters);
            ExecutedInstaller.AddBindBuilder(this);
            return this;
        }
    }
}
