namespace Quantum.SoccerTest
{
    public unsafe class MovementSystem : SystemMainThreadFilter<MovementSystem.Filter>, IKCCCallbacks3D
    {
        public struct Filter
        {
            public EntityRef entityRef;
            public CharacterController3D* characterController;
            public Transform3D* transform;
            public PlayerLink* playerLink;
        }

        public override void Update(Frame f, ref Filter filter)
        {
            var input = *f.GetPlayerInput(filter.playerLink->Player);
            filter.characterController->Move(f, filter.entityRef, input.Direction.XOY);
        }

        public void OnCollisionEnter3D(Frame f, CollisionInfo3D collisionInfo)
        {
            if(f.TryGet<SoccerBall>(collisionInfo.Other, out var ball)) 
            {
                f.Destroy(collisionInfo.Other);
            }
        }
    }
}
