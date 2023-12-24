using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QuantumSoccerTest
{
    public class MainMenuLoader : MonoBehaviour
    {
        [SerializeField] private string mainMenuSceneName = "MainMenu";
        [SerializeField] private string persistentSceneName = "PersistentScene";

        private AsyncOperation sceneLoadingOp;
        private bool hasBootstrapUnloaded = false;

        public void LoadMenu()
        {
            if(!hasBootstrapUnloaded) 
            {
                SceneManager.LoadSceneAsync(persistentSceneName, LoadSceneMode.Additive);
            }
            sceneLoadingOp = SceneManager.LoadSceneAsync(mainMenuSceneName, LoadSceneMode.Additive);
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
