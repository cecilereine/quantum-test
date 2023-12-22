using Photon.Deterministic;

namespace Quantum.SoccerTest.Systems
{
    public unsafe class PlayerSetupSystem : SystemSignalsOnly, ISignalOnPlayerDataSet
    {
        public void OnPlayerDataSet(Frame f, PlayerRef playerRef)
        {
            var data = f.GetPlayerData(playerRef);
            var prototype = f.FindAsset<EntityPrototype>(data.CharacterPrototype.Id.Value);
            var entity = f.Create(prototype);

            var playerLink = new PlayerLink()
            {
                Player = playerRef,
            };
            f.Add(entity, playerLink);
            SpawnPlayerAtRandomPos(f, entity);
        }

        private void SpawnPlayerAtRandomPos(Frame f, EntityRef entity)
        {
            // spawn player at a random pos at the back of the room
            var maxX = 4;
            var minX = -4;
            var minZ = 1;
            var maxZ = 4;
            var y = 2;

            var rndX = f.RNG->NextInclusive(minX, maxX);
            var rndZ = f.RNG->NextInclusive(minZ, maxZ);

            if (f.Unsafe.TryGetPointer<Transform3D>(entity, out var playerTransform))
            {
                playerTransform->Position.X = rndX;
                playerTransform->Position = new FPVector3(rndX, y, rndZ);
            }
        }
    }
}
