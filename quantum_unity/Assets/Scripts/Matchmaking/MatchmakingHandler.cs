using ExitGames.Client.Photon;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Events;

namespace QuantumSoccerTest
{
    public class MatchmakingHandler : MonoBehaviour, IMatchmakingCallbacks
    {
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
            ConnectionManager.Client.OpJoinRandomOrCreateRoom(null, roomParams);
        }
    }
}
