using Photon.Realtime;
using Quantum;
using QuantumSoccerTest.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QuantumSoccerTest
{
    public class GameOverHandler : MonoBehaviour, IConnectionCallbacks
    {
        // TODO: create scene loading manager
        [SerializeField] private string gameplaySceneName = "QuantumGame";
        [SerializeField] private string mainMenuSceneName = "MainMenu";

        // private List<AsyncOperation> sceneLoadingOp = new();
        private AsyncOperation sceneLoadingOp;

        public void LoadMenu()
        {
            sceneLoadingOp = SceneManager.LoadSceneAsync(mainMenuSceneName, LoadSceneMode.Additive);
            StartCoroutine(LoadMainSceneProgress());
        }

        private void OnGameOver(EventOnGameOver callback)
        {
            BasicPopupController.Instance.OnSubmit += () =>
            {
                QuantumRunner.ShutdownAll();
                ConnectionManager.Client.Disconnect();
            };
            BasicPopupController.Instance.ShowPopup("GAME OVER", "MENU");
        }

        private IEnumerator LoadMainSceneProgress()
        {
            while (!sceneLoadingOp.isDone)
            { 
                yield return null;
            }
            UnloadGameScene();
        }

        private IEnumerator UnloadGameSceneProgress()
        {
            while (!sceneLoadingOp.isDone)
            {
                yield return null;
            }
        }

        private void UnloadGameScene()
        {
            ConnectionManager.Instance.ConnectToServer(true);
            BasicPopupController.Instance.HidePopup();

            sceneLoadingOp = SceneManager.UnloadSceneAsync(gameplaySceneName);
            StartCoroutine(UnloadGameSceneProgress());
        }

        public void OnConnected()
        {
        }

        public void OnConnectedToMaster()
        {
        }

        public void OnDisconnected(DisconnectCause cause)
        {
            LoadMenu();
        }

        public void OnRegionListReceived(RegionHandler regionHandler)
        {
        }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
        }

        public void OnCustomAuthenticationFailed(string debugMessage)
        {
        }

        private void OnEnable()
        {
            QuantumEvent.Subscribe<EventOnGameOver>(this, OnGameOver);
            ConnectionManager.Client.AddCallbackTarget(this);
        }

        private void OnDisable()
        {
            QuantumEvent.UnsubscribeListener<EventOnGameOver>(this);
            ConnectionManager.Client.RemoveCallbackTarget(this);
        }
    }
}
