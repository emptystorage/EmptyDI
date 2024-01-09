using UnityEngine;
using UnityEngine.SceneManagement;

namespace EmptyDI.Code.Context
{
    public sealed class ProjectContext : BaseContext
    {
        private int _sceneIndex;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _sceneIndex = (_sceneIndex == 0) ? 1 : 0;

                SceneManager.LoadScene(_sceneIndex);
            }
        }
    }
}
