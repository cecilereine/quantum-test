using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;


namespace QuantumSoccerTest
{
    public class MatchmakingHandler : MonoBehaviour, IMatchmakingCallbacks
    {
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
            // TODO: max 4 players
            ConnectionManager.Client.OpJoinRandomOrCreateRoom(null, null);
        }
    }
}
