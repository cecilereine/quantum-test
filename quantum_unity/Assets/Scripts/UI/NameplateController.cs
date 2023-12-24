using TMPro;
using UnityEngine;

namespace QuantumSoccerTest.UI
{
    public class NameplateController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtNickname;
        private Camera mainCam;

        public void SetNameplate(string nickname)
        {
            mainCam = Camera.main;
            txtNickname.text = nickname;
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
