using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QuantumSoccerTest
{
    public class MainMenuLoader : MonoBehaviour
    {
        [SerializeField] private string mainMenuSceneName = "MainMenu";

        private AsyncOperation sceneLoadingOp;
        private bool hasBootstrapUnloaded = false;

        public void LoadMenu()
        {
            sceneLoadingOp = SceneManager.LoadSceneAsync(mainMenuSceneName, LoadSceneMode.Additive);
            //sceneLoadingOp.Add(SceneManager.LoadSceneAsync(mainMenuSceneName, LoadSceneMode.Additive));
            StartCoroutine(LoadMainSceneProgress());
        }

        private IEnumerator LoadMainSceneProgress()
        {
            while (!sceneLoadingOp.isDone)
            {
                yield return null;
            }

            if (hasBootstrapUnloaded)
            {
                yield break;
            }

            hasBootstrapUnloaded = true;
            SceneManager.UnloadSceneAsync("Bootstrap");
        }
    }
}
