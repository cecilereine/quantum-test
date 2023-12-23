using TMPro;
using UnityEngine;

namespace QuantumSoccerTest.UI
{
    public class ScoreboardEntry : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtEntry;

        public void SetEntry(string nickname, int score)
        {
            txtEntry.text = $"{nickname}: {score.ToString()}";
        }
    }
}
