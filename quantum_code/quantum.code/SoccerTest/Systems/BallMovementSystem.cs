namespace Quantum.SoccerTest.Systems
{
    public unsafe class BallMovementSystem : SystemMainThread, ISignalOnBallHit
    {
        public void OnBallHit(Frame f, EntityRef ball, EntityRef player)
        {
            var ballPhysicsBody = f.Unsafe.GetPointer<PhysicsBody3D>(ball);
            var ballProp = f.Unsafe.GetPointer<SoccerBall>(ball);
            var playerLink = f.Unsafe.GetPointer<PlayerLink>(player);

            ballProp->lastPlayerToHit = playerLink->Player;
     
            var playerKcc = f.Unsafe.GetPointer<CharacterController3D>(player);

            ballPhysicsBody->AddForce(playerKcc->Velocity * ballProp->ballPushPower);
        }

        public override void Update(Frame f)
        {
        }
    }
}
