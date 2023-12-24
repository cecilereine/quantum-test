using Quantum;
using UnityEngine;
using QuantumSoccerTest.UI;

namespace QuantumSoccerTest
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private EntityView entityView;
        [SerializeField] private NameplateController nameplateController;

        public void Initialize()
        {
            var f = QuantumRunner.Default.Game.Frames.Verified;
            var playerRef = f.Get<PlayerLink>(entityView.EntityRef).Player;
            var playerData = f.GetPlayerData(playerRef);

            // set nickname in-game
            nameplateController.SetNameplate(playerData.NickName);

            var scoreboardController = GameObject.FindObjectOfType<ScoreboardController>();
            if (scoreboardController != null)
            {
                scoreboardController.AddEntry(playerData.NickName);
            }
        }
    }
}
