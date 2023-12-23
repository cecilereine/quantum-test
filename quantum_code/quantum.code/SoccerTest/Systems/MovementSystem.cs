namespace Quantum.SoccerTest
{
    public unsafe class MovementSystem : SystemMainThreadFilter<MovementSystem.Filter>
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
    }
}
