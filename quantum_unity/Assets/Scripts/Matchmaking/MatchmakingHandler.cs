using Photon.Realtime;
using QuantumSoccerTest.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace QuantumSoccerTest
{
    public class MatchmakingHandler : MonoBehaviour, IMatchmakingCallbacks
    {
        [SerializeField] private bool isInitializedAtStart = false;
        [SerializeField] private UnityEvent OnRoomJoined;

        public void Initialize()
        {
            ConnectionManager.Client.AddCallbackTarget(this);
            JoinOrCreateRoom();
        }

        #region Matchmaking Callbacks
        public void OnCreatedRoom()
        {
        }

        public void OnCreateRoomFailed(short returnCode, string message)
        {
        }

        public void OnFriendListUpdate(List<FriendInfo> friendList)
        {
        }

        public void OnJoinedRoom()
        {
            BasicPopupController.Instance.HidePopup();
            OnRoomJoined?.Invoke();
        }

        public void OnJoinRandomFailed(short returnCode, string message)
        {
        }

        public void OnJoinRoomFailed(short returnCode, string message)
        {
        }

        public void OnLeftRoom()
        {
        }
        #endregion

        private void JoinOrCreateRoom()
        {
            BasicPopupController.Instance.ShowPopup("Joining room...");
            var roomParams = new EnterRoomParams
            { 
                RoomOptions = new RoomOptions
                {
                    MaxPlayers = 4,
                }
            };
            
            if (!ConnectionManager.Client.OpJoinRandomOrCreateRoom(null, roomParams))
            {
                BasicPopupController.Instance.ShowPopup("Failed to join room");
                ConnectionManager.Instance.ConnectToServer();
            }
        }

        private void Start()
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenu"));
            if (!isInitializedAtStart)
            {
                return;
            }
            Initialize();
        }
    }
}
