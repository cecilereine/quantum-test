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
    }
}
