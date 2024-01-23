using System.IO;
using UnityEditor;

namespace EmptyDI
{
    public static class CreateScriptTemplatesEmptyDI
    {
        [MenuItem("Assets/Create/EmptyDI/New Mono Installer", priority = 9)]
        public static void CreateNewPresentorClass()
        {
            string path = "Assets/EmptyDI/Editor/EmptyDI_MonoInstaller.cs.txt";

            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(path, "NewMonoInstaller.cs");
        }
    }

}
