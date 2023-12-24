using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QuantumSoccerTest.UI
{
    public class BasicPopupController : MonoBehaviour
    {
        // lazy referencing
        public static BasicPopupController Instance { get; private set; }

        [SerializeField] private GameObject popup;
        [SerializeField] private TextMeshProUGUI popupText;
        [SerializeField] private Button popupBtn;
        [SerializeField] private TextMeshProUGUI popupBtnText;

        public Action OnSubmit { get; set; }

        public void ShowPopup(string message, string buttonText = "")
        {
            if (string.IsNullOrWhiteSpace(buttonText))
            {
                popupBtn.gameObject.SetActive(false);
            }
            else
            {
                popupBtn.gameObject.SetActive(true);
                popupBtnText.text = buttonText;
            }

            popupText.text = message;
            popup.SetActive(true);
        }

        public void HidePopup() 
        {
            popup.SetActive(false);
        }

        private void OnSubmitClicked()
        {
            OnSubmit?.Invoke();
        }
        
        private void Awake()
        {
            Instance = this;
            HidePopup();
        }

        private void OnEnable()
        {
            popupBtn.onClick.AddListener(OnSubmitClicked);
        }

        private void OnDisable()
        {
            popupBtn.onClick.RemoveListener(OnSubmitClicked);
        }
    }
}
