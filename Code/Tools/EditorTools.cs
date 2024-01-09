using UnityEngine;
using UnityEditor;

using EmptyDI.Code.Context;

namespace EmptyDI.Code.Tools
{
    public sealed class EditorTools
    {
#if UNITY_EDITOR
        [MenuItem("GameObject/EmptyDI/SceneContext", false, 0)]
        public static void CreateSceneContext()
        {
           new GameObject("EmptyDI_SceneContext").AddComponent<SceneContext>();
        }

        [MenuItem("EmptyDI/ProjectContext")]
        public static void CreateProjectContext()
        {
            var projectContext = Resources.Load<ProjectContext>(nameof(ProjectContext));

            if (projectContext == default)
            {
                if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                {
                    AssetDatabase.CreateFolder("Assets", "Resources");
                    AssetDatabase.Refresh();
                }

                projectContext = new GameObject().AddComponent<ProjectContext>();
                PrefabUtility.SaveAsPrefabAsset(projectContext.gameObject, $"Assets/Resources/{nameof(ProjectContext)}.prefab");
                Object.DestroyImmediate(projectContext.gameObject);
            }                      

            Selection.activeObject = projectContext;
        }
#endif
    }
}
