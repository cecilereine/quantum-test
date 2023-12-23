namespace Quantum
{
    public partial struct BallSpawner
    {
        public unsafe void Spawn(Frame f)
        {
            var ballEntity = f.Create(ballPrefab);
            var ballTransform = f.Unsafe.GetPointer<Transform3D>(ballEntity);

            ballTransform->Position = new Photon.Deterministic.FPVector3(0, 2, 0);
        }
    }
}
