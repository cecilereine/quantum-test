using TMPro;
using UnityEngine;

namespace QuantumSoccerTest.UI
{
    public class NameplateController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtNickname;
        private Camera mainCam;

        public void SetNameplate()
        {
            mainCam = Camera.main;
            txtNickname.text = ConnectionManager.Client.NickName;
        }
        private void LateUpdate()
        {
            if (mainCam != null)
            {
                transform.rotation = mainCam.transform.rotation;
            }
        }
    }
}
