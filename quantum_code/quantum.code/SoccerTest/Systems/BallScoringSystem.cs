using static Quantum.SoccerTest.MovementSystem;

namespace Quantum.SoccerTest.Systems
{
    public unsafe class BallScoringSystem : SystemSignalsOnly, ISignalOnTriggerEnter3D
    {
        // checks if  goal is hit by something
        public void OnTriggerEnter3D(Frame f, TriggerInfo3D info)
        {
            if (!f.TryGet<Goal>(info.Entity, out var goal))
            {
                return;
            }
            if (f.TryGet<SoccerBall>(info.Other, out var ball))
            {
                var playerLink = f.Unsafe.GetPointer<PlayerLink>(ball.lastPlayerToHit);
                playerLink->score += 1;

                f.Signals.OnBallReset(info.Other);
                f.Signals.OnRoundReset(ball.lastPlayerToHit);
                f.Events.OnScoreUpdated(playerLink->Player, playerLink->score);

                var data = f.GetPlayerData(playerLink->Player);

                if (playerLink->score >= 3)
                {
                    data.WinCount++;
                    f.Events.OnGameOver(playerLink->Player, data.WinCount);
                    DestroyPlayers(f);
                }
            }
        }

        private void DestroyPlayers(Frame f)
        {
            var playerFilter = f.Unsafe.FilterStruct<Filter>();
            var playerStruct = default(Filter);

            while (playerFilter.Next(&playerStruct))
            {
                f.Destroy(playerStruct.entityRef);
            }
        }
    }
}
