using UnityEngine;
using UnityEngine.EventSystems;

namespace QuantumSoccerTest.Common
{
    [RequireComponent(typeof(EventSystem))]
    public class EventSystemTracker : MonoBehaviour
    {

        private void Start()
        {
            var eventSystem = GetComponent<EventSystem>();
            if (EventSystem.current != eventSystem)
            {
                Destroy(gameObject);
            }
        }
    }
}
