using System.Collections.Generic;
using EmptyDI.Code.Implementation;

namespace EmptyDI.Code.BindBuilder
{
    public partial struct SingleBindBuilder<T> : IBindBuilder
        where T : class
    {
        /// <summary>
        /// Задать реализацию абстракции в параметрах конструктора объекта
        /// </summary>
        /// <typeparam name="I">Тип абстракции</typeparam>
        /// <typeparam name="R1">Тип реализации</typeparam>
        /// <param name="abstaractionValue1">Значение реализации</param>
        /// <returns></returns>
        public SingleBindBuilder<T> WhereAbstaction<I, R1>(R1 abstaractionValue1 = null)
            where I : class
            where R1 : class, I
        {
            var parameters = new Queue<ConstructorParameterInfo>();
            parameters.Enqueue(new ConstructorParameterInfo(typeof(R1), abstaractionValue1));

            Info.ParamsInfo.ChangeConstructorParametersType(parameters);
            ExecutedInstaller.AddBindBuilder(this);
            return this;
        }
        /// <summary>
        /// Задать реализацию абстракции в параметрах конструктора объекта
        /// </summary>
        /// <typeparam name="I">Тип абстракции</typeparam>
        /// <typeparam name="R1">Тип реализации 1</typeparam>
        /// <typeparam name="R2">Тип реализации 2</typeparam>
        /// <param name="abstaractionValue1">Значение реализации 1</param>
        /// <param name="abstaractionValue2">Значение реализации 2</param>
        /// <returns></returns>
        public SingleBindBuilder<T> WhereAbstaction<I, R1, R2>(R1 abstaractionValue1 = null, R2 abstaractionValue2 = null)
            where I : class
            where R1 : class, I
            where R2 : class, I
        {
            var parameters = new Queue<ConstructorParameterInfo>();
            parameters.Enqueue(new ConstructorParameterInfo(typeof(R1), abstaractionValue1));
            parameters.Enqueue(new ConstructorParameterInfo(typeof(R2), abstaractionValue2));

            Info.ParamsInfo.ChangeConstructorParametersType(parameters);
            ExecutedInstaller.AddBindBuilder(this);
            return this;
        }
        /// <summary>
        /// Задать реализацию абстракции в параметрах конструктора объекта
        /// </summary>
        /// <typeparam name="I">Тип абстракции</typeparam>
        /// <typeparam name="R1">Тип реализации 1</typeparam>
        /// <typeparam name="R2">Тип реализации 2</typeparam>
        /// <typeparam name="R3">Тип реализации 3</typeparam>
        /// <param name="abstaractionValue1">Значение реализации 1</param>
        /// <param name="abstaractionValue2">Значение реализации 2</param>
        /// <param name="abstaractionValue3">Значение реализации 3</param>
        /// <returns></returns>
        public SingleBindBuilder<T> WhereAbstaction<I, R1, R2, R3>(R1 abstaractionValue1 = null, R2 abstaractionValue2 = null, R3 abstaractionValue3 = null)
            where I : class
            where R1 : class, I
            where R2 : class, I
            where R3 : class, I
        {
            var parameters = new Queue<ConstructorParameterInfo>();
            parameters.Enqueue(new ConstructorParameterInfo(typeof(R1), abstaractionValue1));
            parameters.Enqueue(new ConstructorParameterInfo(typeof(R2), abstaractionValue2));
            parameters.Enqueue(new ConstructorParameterInfo(typeof(R3), abstaractionValue3));

            Info.ParamsInfo.ChangeConstructorParametersType(parameters);
            ExecutedInstaller.AddBindBuilder(this);
            return this;
        }
        /// <summary>
        /// Задать реализацию абстракции в параметрах конструктора объекта
        /// </summary>
        /// <typeparam name="I">Тип абстракции</typeparam>
        /// <typeparam name="R1">Тип реализации 1</typeparam>
        /// <typeparam name="R2">Тип реализации 2</typeparam>
        /// <typeparam name="R3">Тип реализации 3</typeparam>
        /// <typeparam name="R4">Тип реализации 4</typeparam>
        /// <param name="abstaractionValue1">Значение реализации 1</param>
        /// <param name="abstaractionValue2">Значение реализации 2</param>
        /// <param name="abstaractionValue3">Значение реализации 3</param>
        /// <param name="abstaractionValue4">Значение реализации 4</param>
        /// <returns></returns>
        public SingleBindBuilder<T> WhereAbstaction<I, R1, R2, R3, R4>(R1 abstaractionValue1 = null, R2 abstaractionValue2 = null, R3 abstaractionValue3 = null, R4 abstaractionValue4 = null)
            where I : class
            where R1 : class, I
            where R2 : class, I
            where R3 : class, I
            where R4 : class, I
        {
            var parameters = new Queue<ConstructorParameterInfo>();
            parameters.Enqueue(new ConstructorParameterInfo(typeof(R1), abstaractionValue1));
            parameters.Enqueue(new ConstructorParameterInfo(typeof(R2), abstaractionValue2));
            parameters.Enqueue(new ConstructorParameterInfo(typeof(R3), abstaractionValue3));
            parameters.Enqueue(new ConstructorParameterInfo(typeof(R4), abstaractionValue4));

            Info.ParamsInfo.ChangeConstructorParametersType(parameters);
            ExecutedInstaller.AddBindBuilder(this);
            return this;
        }
        /// <summary>
        /// Задать реализацию абстракции в параметрах конструктора объекта
        /// </summary>
        /// <typeparam name="I">Тип абстракции</typeparam>
        /// <typeparam name="R1">Тип реализации 1</typeparam>
        /// <typeparam name="R2">Тип реализации 2</typeparam>
        /// <typeparam name="R3">Тип реализации 3</typeparam>
        /// <typeparam name="R4">Тип реализации 4</typeparam>
        /// <typeparam name="R5">Тип реализации 5</typeparam>
        /// <param name="abstaractionValue1">Значение реализации 1</param>
        /// <param name="abstaractionValue2">Значение реализации 2</param>
        /// <param name="abstaractionValue3">Значение реализации 3</param>
        /// <param name="abstaractionValue4">Значение реализации 4</param>
        /// <param name="abstaractionValue5">Значение реализации 5</param>
        /// <returns></returns>
        public SingleBindBuilder<T> WhereAbstaction<I, R1, R2, R3, R4, R5>(R1 abstaractionValue1 = null, R2 abstaractionValue2 = null, R3 abstaractionValue3 = null, R4 abstaractionValue4 = null, R5 abstaractionValue5 = null)
            where I : class
            where R1 : class, I
            where R2 : class, I
            where R3 : class, I
            where R4 : class, I
            where R5 : class, I
        {
            var parameters = new Queue<ConstructorParameterInfo>();
            parameters.Enqueue(new ConstructorParameterInfo(typeof(R1), abstaractionValue1));
            parameters.Enqueue(new ConstructorParameterInfo(typeof(R2), abstaractionValue2));
            parameters.Enqueue(new ConstructorParameterInfo(typeof(R3), abstaractionValue3));
            parameters.Enqueue(new ConstructorParameterInfo(typeof(R4), abstaractionValue4));
            parameters.Enqueue(new ConstructorParameterInfo(typeof(R5), abstaractionValue5));

            Info.ParamsInfo.ChangeConstructorParametersType(parameters);
            ExecutedInstaller.AddBindBuilder(this);
            return this;
        }
    }
}
