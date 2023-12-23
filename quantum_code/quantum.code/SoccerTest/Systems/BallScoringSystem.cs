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
                f.Events.OnScoreUpdated(playerLink->Player, playerLink->score);
            }
        }
    }
}
