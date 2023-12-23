using Quantum;
using Photon.Deterministic;
using UnityEngine;

namespace QuantumSoccerTest
{
    public class InputPoller : MonoBehaviour
    {
        private void PollInput(CallbackPollInput callback)
        {
            Quantum.Input input = new Quantum.Input();
            var x = UnityEngine.Input.GetAxis("Horizontal");
            var y = UnityEngine.Input.GetAxis("Vertical");

            input.Block = UnityEngine.Input.GetButton("Jump");
            input.Direction = new Vector2(x, y).ToFPVector2();
            callback.SetInput(input, DeterministicInputFlags.Repeatable);
        }

        private void OnEnable()
        {
            QuantumCallback.Subscribe(this, (CallbackPollInput callback) => PollInput(callback));
        }

        private void OnDisable()
        {
            QuantumCallback.UnsubscribeListener(this);
        }
    }
}
