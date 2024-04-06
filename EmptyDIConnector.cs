using System;
using UnityEngine;

using EmptyDI.Code.BindBuilder;
using EmptyDI.Code.Locator;
using EmptyDI.Code.DIContainer;
using EmptyDI.Code.Context;
using EmptyDI.Pool;

namespace EmptyDI
{
    public static class EmptyDIConnector
    {
        public static ProjectContext ProjectContext { get; private set; }

        internal static event Action<Type> OnBindObject;

        /// <summary>
        /// �������� ������ � ���������, ��� �����������
        /// </summary>
        /// <typeparam name="T">��� �������</typeparam>
        /// <param name="installer"></param>
        /// <param name="containerTag">������������� ����������</param>
        /// <returns>������ ��������� ����������� <see cref="BaseBindBuilder{T}"/></returns>
        public static SingleBindBuilder<T> Bind<T>(this IInstaller installer, string containerTag = "common")
            where T : class
        {
            return Bind<T>(installer, null, containerTag);
        }
        /// <summary>
        /// �������� ������ � ���������, ��� �����������
        /// </summary>
        /// <typeparam name="T">��� �������</typeparam>
        /// <param name="installer"></param>
        /// <param name="implementation">���������� �������</param>
        /// <param name="containerTag">������������� ����������</param>
        /// <returns>������ ��������� ����������� <see cref="BaseBindBuilder{T}"/></returns>
        public static SingleBindBuilder<T> Bind<T>(this IInstaller installer, T implementation, string containerTag = "common")
            where T : class
        {
            ParameterValidation<T>();
            OnBindObject?.Invoke(typeof(T));

            var builder = new SingleBindBuilder<T>(installer, containerTag, implementation);
            installer.AddBindBuilder(builder);
            return builder;
        }
        /// <summary>
        /// �������� ������ � ��������� ��� � ������, ��� �����������
        /// </summary>
        /// <typeparam name="P">��� ����</typeparam>
        /// <typeparam name="T">��� �������</typeparam>
        /// <param name="installer"></param>
        /// <param name="implementation">���������� �������</param>
        /// <returns></returns>
        public static SingleBindBuilder<T> Bind<P, T>(this IInstaller installer, T implementation = null)
            where P : DIPool<T>, new()
            where T : class
        {
            ParameterValidation<P>();
            ParameterValidation<T>();
            OnBindObject?.Invoke(typeof(P));
            OnBindObject?.Invoke(typeof(T));

            var builder = new PoolBindBuilder<P, T>(installer, implementation);
            installer.AddBindBuilder(builder);
            return builder.ObjectBuilder;
        }

        /// <summary>
        /// �������� ������
        /// </summary>
        /// <typeparam name="T">��� �������</typeparam>
        /// <returns>���������� �������</returns>
        public static T Get<T>()
            where T : class
        {
            var info = InternalLocator.GetObject<ContainerBank>().FindImplementation<T>();

            if (info == null)
                throw new Exception($"�� ������� ���������� ������� - {typeof(T).Name}");

            return info.Implementation<T>();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void EmptyDIInitialize()
        {
            var projectContextPrefab = Resources.Load<ProjectContext>(nameof(ProjectContext));

            if(projectContextPrefab == null)
            {
#if UNITY_EDITOR
                Code.Tools.EditorTools.CreateProjectContext();
                projectContextPrefab = Resources.Load<ProjectContext>(nameof(ProjectContext));

#else
                MonoBehaviour.DontDestroyOnLoad(new GameObject().AddComponent<ProjectContext>().gameObject);
#endif
            }

            MonoBehaviour.DontDestroyOnLoad(MonoBehaviour.Instantiate(projectContextPrefab).gameObject);
        }

        private static void ParameterValidation<T>()
            where T : class
        {
            if (typeof(T).IsInterface || typeof(T).IsAbstract)
            {
                throw new Exception($"������ �������� ���������� ������� - {typeof(T).Name}, ������� �������� ����������� ��� ����������� �������.");
            }
        }
    }
}

