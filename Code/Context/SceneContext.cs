using UnityEngine;
using UnityEngine.SceneManagement;

namespace EmptyDI.Code.Context
{
    public sealed class SceneContext : BaseContext
    {
        protected override void ContextCreated()
        {
            SceneManager.sceneUnloaded += SceneIsUnloaded;
        }

        private void SceneIsUnloaded(Scene scene)
        {
            Debug.Log($"Scene unloaded! - {scene.name}");
            SceneManager.sceneUnloaded -= SceneIsUnloaded;
        }
    }
}
