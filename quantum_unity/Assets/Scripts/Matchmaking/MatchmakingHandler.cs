using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
            Debug.Log("room created");
        }

        public void OnCreateRoomFailed(short returnCode, string message)
        {
        }

        public void OnFriendListUpdate(List<FriendInfo> friendList)
        {
        }

        public void OnJoinedRoom()
        {
            Debug.Log("room joined");
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
            var roomParams = new EnterRoomParams
            { 
                RoomOptions = new RoomOptions
                {
                    MaxPlayers = 4,
                }
            };
            
            if (!ConnectionManager.Client.OpJoinRandomOrCreateRoom(null, roomParams))
            {
                Debug.Log("failed to join room");
            }
        }

        private void Start()
        {
            if (!isInitializedAtStart)
            {
                return;
            }
            Initialize();
        }
    }
}
