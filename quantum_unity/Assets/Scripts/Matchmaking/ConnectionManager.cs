using Photon.Realtime;
using QuantumSoccerTest.Common;
using QuantumSoccerTest.UI;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace QuantumSoccerTest
{
    public class ConnectionManager : SingletonMonobehavior<ConnectionManager>, IConnectionCallbacks
    {
        [SerializeField] private UnityEvent OnConnectedToServer;
        // TODO: move UI stuff from connection manager
        [SerializeField] private TMP_InputField nicknameInputField;
        [SerializeField] private TextMeshProUGUI connectionStatusTxt;
        [SerializeField] private Button connectBtn;

        private bool connectedFromGameScene;

        public static QuantumLoadBalancingClient Client { get; private set; }

        public void Initialize()
        {
            Client = new QuantumLoadBalancingClient()
            {
                UserId = Guid.NewGuid().ToString(),
            };

            Client.AddCallbackTarget(this);
        }

        public void ConnectToServer(bool fromMainScene = false)
        {
            connectedFromGameScene = fromMainScene;
            Client.NickName = nicknameInputField.text;

            connectBtn.interactable = false;
            connectionStatusTxt.text = "Connecting...";
            var appSettings = PhotonServerSettings.Instance.AppSettings;

            if (!Client.IsConnected && !Client.ConnectUsingSettings(appSettings))
            {
                connectionStatusTxt.text = "Failed to connect to master server";
                connectBtn.interactable = true;
                Client.Disconnect();
                return;
            }

            if (BasicPopupController.Instance != null)
            {
                BasicPopupController.Instance.HidePopup();
            }

            Debug.Log($"{Client.AppId}, {Client.CloudRegion}");
        }

        #region Connection Callbacks
        public void OnConnected()
        {
        }

        public void OnConnectedToMaster()
        {
            connectionStatusTxt.text = "Connected to server!";
            if (connectedFromGameScene)
            {
                return;
            }
            OnConnectedToServer?.Invoke();
        }

        public void OnCustomAuthenticationFailed(string debugMessage)
        {
        }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
        }

        public void OnDisconnected(DisconnectCause cause)
        {
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                BasicPopupController.Instance.ShowPopup("connecting to server...");
                ConnectToServer();
            }
        }

        public void OnRegionListReceived(RegionHandler regionHandler)
        {
        }
        #endregion

        private void Start()
        {
            Initialize();
        }

        private void Update() 
        {
            Client?.Service();
        }
    }
}
