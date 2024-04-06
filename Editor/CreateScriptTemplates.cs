using System.IO;
using UnityEngine;
using UnityEditor;

namespace EmptyDI
{
    public static class CreateScriptTemplatesEmptyDI
    {
        [MenuItem("Assets/Create/EmptyDI/New Mono Installer", priority = 9)]
        public static void CreateNewPresentorClass()
        {
            string path = AssetDatabase.GetAssetPath(Resources.Load<TextAsset>("EmptyDI_MonoInstaller.cs"));

            if (string.IsNullOrEmpty(path))
            {
                Debug.LogError("Не найден шаблон для создания cs файла для EmptyDI Installer");
                return;
            }

            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(path, "NewMonoInstaller.cs");
        }
    }

}
