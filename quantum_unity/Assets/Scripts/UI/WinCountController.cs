using Quantum;
using TMPro;
using UnityEngine;

namespace QuantumSoccerTest.UI
{
    public class WinCountController : MonoBehaviour
    {
        [SerializeField] private string winCountKey = "winCount";
        [SerializeField] private TextMeshProUGUI winCountTxt;

        private int winCount = 0;

        private void UpdateWinCountUI()
        {
            winCountTxt.text = "WINS: " + GetWinCount().ToString();
        }

        private int GetWinCount()
        { 
            return winCount = PlayerPrefs.GetInt(winCountKey); ;
        }

        private void SetWinCount(int winCount)
        {
            PlayerPrefs.SetInt(winCountKey, winCount);
        }

        private void OnGameOver(EventOnGameOver callback)
        {
            SetWinCount((int)callback.winCount);
            UpdateWinCountUI();
        }

        private void Awake()
        {
            GetWinCount();
        }

        private void Start()
        {
            UpdateWinCountUI();
        }

        private void OnEnable()
        {
            QuantumEvent.Subscribe<EventOnGameOver>(this, OnGameOver);
        }

        private void OnDisable()
        {
            QuantumEvent.UnsubscribeListener<EventOnGameOver>(this);
        }
    }
}
