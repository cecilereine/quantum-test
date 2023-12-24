using Quantum;
using System.Collections.Generic;
using UnityEngine;

namespace QuantumSoccerTest.UI
{
    public class ScoreboardController : MonoBehaviour
    {
        [SerializeField] private GameObject entryPrefab;
        [SerializeField] private RectTransform entriesContainer;

        private Dictionary<string, ScoreboardEntry> entries = new();

        public void AddEntry(string nickname)
        {
            var entryObj = Instantiate(entryPrefab, entriesContainer);
            var entry = entryObj.GetComponent<ScoreboardEntry>();
            entry.SetEntry(nickname, 0);
            entries.Add(nickname, entry);
        }

        public void UpdateEntry(string nickname, int score)
        {
            if(entries.TryGetValue(nickname, out var entry)) 
            {
                entry.SetEntry(nickname, score);
            }
        }

        private void OnScoreUpdated(EventOnScoreUpdated callback)
        {
            Debug.Log("event updated");
            var f = QuantumRunner.Default.Game.Frames.Verified;
            var playerData = f.GetPlayerData(callback.playerRef);
            var nickname = playerData.NickName;
            UpdateEntry(nickname, (int)callback.score);
        }

        private void OnEnable()
        {
            QuantumEvent.Subscribe<EventOnScoreUpdated>(this, OnScoreUpdated);
        }

        private void OnDisable()
        {
            QuantumEvent.UnsubscribeListener<EventOnScoreUpdated>(this);
        }
    }
}
