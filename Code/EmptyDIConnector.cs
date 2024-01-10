using System;
using UnityEngine;

using EmptyDI.Code.BindBuilder;
using EmptyDI.Code.Locator;
using EmptyDI.Code.DIContainer;
using EmptyDI.Code.Context;

namespace EmptyDI
{
    public static class EmptyDIConnector
    {
        public static ProjectContext ProjectContext { get; private set; }

        internal static event Action<Type> OnBindObject;

        /// <summary>
        /// Добавить объект в контейнер, как зависимость
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="installer"></param>
        /// <param name="containerTag">Идентификатор контейнера</param>
        /// <returns>Объект настройки зависимость <see cref="BaseBindBuilder{T}"/></returns>
        public static BaseBindBuilder<T> Bind<T>(this IInstaller installer, string containerTag = "common")
            where T : class
        {
            if (typeof(T).IsInterface || typeof(T).IsAbstract)
            {
                throw new Exception($"Нельзя добавить реализацию объекта - {typeof(T).Name}, который является интерфейсом или абстрактным классом.");
            }

            OnBindObject?.Invoke(typeof(T));

            var builder = new BaseBindBuilder<T>(containerTag);
            installer.AddBindBuilder(builder);
            return builder;
        }
        /// <summary>
        /// Добавить объект в контейнер, как зависимость
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="installer"></param>
        /// <param name="implementation">Реализация объекта </param>
        /// <param name="containerTag">Идентификатор контейнера</param>
        /// <returns>Объект настройки зависимость <see cref="BaseBindBuilder{T}"/></returns>
        public static BaseBindBuilder<T> Bind<T>(this IInstaller installer, T implementation, string containerTag = "common")
            where T : class
        {
            if (typeof(T).IsInterface || typeof(T).IsAbstract)
            {
                throw new Exception($"Нельзя добавить реализацию объекта - {typeof(T).Name}, который является интерфейсом или абстрактным классом.");
            }

            OnBindObject?.Invoke(typeof(T));

            var builder = new BaseBindBuilder<T>(containerTag, implementation);
            installer.AddBindBuilder(builder);
            return builder;
        }
        /// <summary>
        /// Получить объект
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <returns>Реализация объекта</returns>
        public static T Get<T>()
            where T : class
        {
            var info = InternalLocator.GetObject<ContainerBank>().FindImplementation<T>();

            if (info == null)
                throw new Exception($"Не найдена реализация объекта - {typeof(T).Name}");

            return info.Implementation<T>();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void EmptyDIInitialize()
        {
            var projectContextPrefab = Resources.Load<ProjectContext>(nameof(ProjectContext));

            MonoBehaviour.DontDestroyOnLoad(MonoBehaviour.Instantiate(projectContextPrefab).gameObject);
        }
    }
}

