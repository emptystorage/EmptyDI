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
        /// �������� ������ � ���������, ��� �����������
        /// </summary>
        /// <typeparam name="T">��� �������</typeparam>
        /// <param name="installer"></param>
        /// <param name="containerTag">������������� ����������</param>
        /// <returns>������ ��������� ����������� <see cref="BaseBindBuilder{T}"/></returns>
        public static BaseBindBuilder<T> Bind<T>(this IInstaller installer, string containerTag = "common")
            where T : class
        {
            if (typeof(T).IsInterface || typeof(T).IsAbstract)
            {
                throw new Exception($"������ �������� ���������� ������� - {typeof(T).Name}, ������� �������� ����������� ��� ����������� �������.");
            }

            OnBindObject?.Invoke(typeof(T));

            var builder = new BaseBindBuilder<T>(containerTag);
            installer.AddBindBuilder(builder);
            return builder;
        }
        /// <summary>
        /// �������� ������ � ���������, ��� �����������
        /// </summary>
        /// <typeparam name="T">��� �������</typeparam>
        /// <param name="installer"></param>
        /// <param name="implementation">���������� ������� </param>
        /// <param name="containerTag">������������� ����������</param>
        /// <returns>������ ��������� ����������� <see cref="BaseBindBuilder{T}"/></returns>
        public static BaseBindBuilder<T> Bind<T>(this IInstaller installer, T implementation, string containerTag = "common")
            where T : class
        {
            if (typeof(T).IsInterface || typeof(T).IsAbstract)
            {
                throw new Exception($"������ �������� ���������� ������� - {typeof(T).Name}, ������� �������� ����������� ��� ����������� �������.");
            }

            OnBindObject?.Invoke(typeof(T));

            var builder = new BaseBindBuilder<T>(containerTag, implementation);
            installer.AddBindBuilder(builder);
            return builder;
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

            MonoBehaviour.DontDestroyOnLoad(MonoBehaviour.Instantiate(projectContextPrefab).gameObject);
        }
    }
}

