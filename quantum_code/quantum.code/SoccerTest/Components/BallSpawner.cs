namespace Quantum
{
    public partial struct BallSpawner
    {
        public unsafe void Spawn(Frame f)
        {
            var ballEntity = f.Create(ballPrefab);
            var ballTransform = f.Unsafe.GetPointer<Transform3D>(ballEntity);
            var ball = f.Unsafe.GetPointer<SoccerBall>(ballEntity);

            var rndX = f.RNG->NextInclusive(-10, 10);

            ballTransform->Position = new Photon.Deterministic.FPVector3(rndX, 2, 0);
            ball->spawnPosition = ballTransform->Position;
        }
    }
}
