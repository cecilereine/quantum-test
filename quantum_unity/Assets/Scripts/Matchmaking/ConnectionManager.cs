using Photon.Realtime;
using QuantumSoccerTest.Common;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UI = UnityEngine.UI;

namespace QuantumSoccerTest
{
    public class ConnectionManager : SingletonMonobehavior<ConnectionManager>, IConnectionCallbacks
    {
        [SerializeField] private PhotonServerSettings photonAppSettings;
        [SerializeField] private UnityEvent OnConnectedToServer;
        // TODO: move UI stuff from connection manager
        [SerializeField] private TMP_InputField nicknameInputField;
        [SerializeField] private TextMeshProUGUI connectionStatusTxt;
        [SerializeField] private UI.Button connectBtn;

        public static QuantumLoadBalancingClient Client { get; private set; }

        public void Initialize()
        {
            Client = new QuantumLoadBalancingClient()
            {
                NickName = nicknameInputField.text,
                UserId = Guid.NewGuid().ToString(),
            };

            Client.AddCallbackTarget(this);
        }

        public void ConnectToServer()
        {
            connectBtn.interactable = false;
            connectionStatusTxt.text = "Connecting...";
            if (!Client.IsConnected && !Client.ConnectUsingSettings(photonAppSettings.AppSettings))
            {
                Debug.Log("Failed to connect to master server");
                connectBtn.interactable = true;
                Client.Disconnect();
                return;
            }
        }

        #region Connection Callbacks
        public void OnConnected()
        {
        }

        public void OnConnectedToMaster()
        {
            connectionStatusTxt.text = "Connected to server!";
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
