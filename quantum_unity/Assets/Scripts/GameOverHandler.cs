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
            //sceneLoadingOp.Add(SceneManager.LoadSceneAsync(mainMenuSceneName, LoadSceneMode.Additive));
            StartCoroutine(LoadMainSceneProgress());
        }

        private void OnGameOver(EventOnGameOver callback)
        {
            //TODO: add ui 
            ConnectionManager.Client.Disconnect();
            QuantumRunner.ShutdownAll(true);
            LoadMenu();
        }

        private IEnumerator LoadMainSceneProgress()
        {
            while (!sceneLoadingOp.isDone)
            { 
                yield return null;
            }
            Debug.Log("main scene has loaded.");
            UnloadGameSceneProgress();

        }

        private void UnloadGameSceneProgress()
        {
            SceneManager.UnloadSceneAsync(gameplaySceneName);
        }

        private void OnEnable()
        {
            QuantumEvent.Subscribe<EventOnGameOver>(this, OnGameOver);
        }

        private void OnDisable()
        {
            QuantumEvent.UnsubscribeListener<EventOnGameOver>(this);

        }

        public void OnConnected()
        {
        }

        public void OnConnectedToMaster()
        {
        }

        public void OnDisconnected(DisconnectCause cause)
        {
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
    }
}
