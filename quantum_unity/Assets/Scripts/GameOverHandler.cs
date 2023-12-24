using Photon.Realtime;
using Quantum;
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
            //TODO: add ui 
            ConnectionManager.Client.Disconnect();
            QuantumRunner.ShutdownAll();
        }

        private IEnumerator LoadMainSceneProgress()
        {
            while (!sceneLoadingOp.isDone)
            { 
                yield return null;
            }
            Debug.Log("main scene has loaded.");
            UnloadGameScene();
        }

        private IEnumerator UnloadGameSceneProgress()
        {
            Debug.Log("unloading gamescene");
            while (!sceneLoadingOp.isDone)
            {
                yield return null;
            }
            Debug.Log("establish connection");
        }

        private void UnloadGameScene()
        {
            ConnectionManager.Instance.ConnectToServer(true);

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
