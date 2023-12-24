using Quantum.Demo;
using Quantum;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QuantumSoccerTest
{
    public class GameLoader : MonoBehaviour
    {
        [SerializeField] private string gameplaySceneName = "QuantumGame";
        [SerializeField] private string mainMenuSceneName = "MainMenu";

        [SerializeField] private RuntimeConfig runtimeConfig;
        private AsyncOperation sceneLoadingOp;

        public void LoadGameScene()
        {
            sceneLoadingOp = SceneManager.LoadSceneAsync(gameplaySceneName, LoadSceneMode.Additive);
            StartCoroutine(UpdateSceneLoadingProgress());
        }

        public void UnloadMainMenu()
        {
            SceneManager.UnloadSceneAsync(mainMenuSceneName);
        }

        private void StartMatch()
        {
            Debug.Log("Start match");
            var param = new QuantumRunner.StartParameters
            {
                RuntimeConfig = runtimeConfig,
                DeterministicConfig = DeterministicSessionConfigAsset.Instance.Config,
                ReplayProvider = null,
                GameMode = Photon.Deterministic.DeterministicGameMode.Multiplayer,
                PlayerCount = 4, // TODO: don't hardcode
                LocalPlayerCount = 1,
                RecordingFlags = RecordingFlags.None,
                NetworkClient = ConnectionManager.Client,
            };

            var clientId = ClientIdProvider.CreateClientId(ClientIdProvider.Type.PhotonUserId, ConnectionManager.Client);
            QuantumRunner.StartGame(clientId, param);
        }

        private IEnumerator UpdateSceneLoadingProgress()
        {
            while (!sceneLoadingOp.isDone)
            {
                yield return null;
            }

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(gameplaySceneName));

            StartMatch();
            UnloadMainMenu();
        }
    }
}
