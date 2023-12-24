using Photon.Deterministic;

namespace Quantum.SoccerTest.Systems
{
    public unsafe class BallMovementSystem : SystemMainThread, ISignalOnBallHit, ISignalOnBallReset, ISignalOnBallBlocked
    {
        public void OnBallBlocked(Frame f, EntityRef ball, EntityRef player)
        {
            var playerLink = f.Unsafe.GetPointer<PlayerLink>(player);
            if (!playerLink->isBlockEnabled)
            {
                return;
            }
            var ballPhysicsBody = f.Unsafe.GetPointer<PhysicsBody3D>(ball);
            var ballProp = f.Unsafe.GetPointer<SoccerBall>(ball);
            var ballTrans = f.Unsafe.GetPointer<Transform3D>(ball);
            var playerTrans = f.Unsafe.GetPointer<Transform3D>(player);
            var distance = FPVector3.Distance(ballTrans->Position, playerTrans->Position);
            if(distance <= 3)
            {
                ballPhysicsBody->AddForce(-FPVector3.Forward * 500);
                playerLink->isBlockEnabled = false;
            }
        }

        public void OnBallHit(Frame f, EntityRef ball, EntityRef player)
        {
            var ballPhysicsBody = f.Unsafe.GetPointer<PhysicsBody3D>(ball);
            var ballProp = f.Unsafe.GetPointer<SoccerBall>(ball);
            var playerLink = f.Unsafe.GetPointer<PlayerLink>(player);

            ballProp->lastPlayerToHit = player;
     
            var playerKcc = f.Unsafe.GetPointer<CharacterController3D>(player);

            ballPhysicsBody->AddForce(playerKcc->Velocity * ballProp->ballPushPower);
        }

        public void OnBallReset(Frame f, EntityRef ball)
        {
            var ballTrans = f.Unsafe.GetPointer<Transform3D>(ball);
            var ballProp = f.Unsafe.GetPointer<SoccerBall>(ball);
            var ballPhysicsBody = f.Unsafe.GetPointer<PhysicsBody3D>(ball);

            ballTrans->Position = ballProp->spawnPosition;
            ballPhysicsBody->Velocity = FPVector3.Zero;
        }

        public override void Update(Frame f)
        {
        }
    }
}
