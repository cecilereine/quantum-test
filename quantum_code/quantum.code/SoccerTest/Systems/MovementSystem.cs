using static Quantum.SoccerTest.MovementSystem;

namespace Quantum.SoccerTest
{
    public unsafe class MovementSystem : SystemMainThreadFilter<MovementSystem.Filter>, IKCCCallbacks3D, ISignalOnRoundReset
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

            if (input.Block.WasPressed)
            {
                var ballFilter = f.Filter<SoccerBall>();
                while(ballFilter.NextUnsafe(out var entity, out var ball)) 
                {
                    f.Signals.OnBallBlocked(entity, filter.entityRef);
                }
            }

            filter.characterController->Move(f, filter.entityRef, input.Direction.XOY);
        }

        public void OnCollisionEnter3D(Frame f, CollisionInfo3D collisionInfo)
        {
            if(f.TryGet<SoccerBall>(collisionInfo.Other, out var ball)) 
            {
                f.Signals.OnBallHit(collisionInfo.Other, collisionInfo.Entity);
            }
        }

        public void OnRoundReset(Frame f, EntityRef player)
        {
            var playerFilter = f.Unsafe.FilterStruct<Filter>();
            var playerStruct = default(Filter);

            while (playerFilter.Next(&playerStruct))
            {
                playerStruct.playerLink->isBlockEnabled = true;
                playerStruct.transform->Position = playerStruct.playerLink->spawnPosition;
            }
        }
    }
}
