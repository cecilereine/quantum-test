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
                isBlockEnabled = true,
                Player = playerRef,
            };
            f.Add(entity, playerLink);
            SpawnPlayerAtRandomPos(f, entity);

            // spawn ball
            var playerCount = f.ComponentCount<PlayerLink>();
            Log.Debug("no of players: " + playerCount);
            if (playerCount > 0)
            {
                SpawnBall(f);
            }
        }

        private void SpawnBall(Frame f)
        {
            if (f.TryGetSingleton<BallSpawner>(out var ballSpawner) && !ballSpawner.hasSpawned)
            {
                ballSpawner.Spawn(f);
                ballSpawner.hasSpawned = true;
            }
        }

        private void SpawnPlayerAtRandomPos(Frame f, EntityRef entity)
        {
            // TODO: modify seed
            // spawn player at a random pos at the back of the room
            var maxX = 10;
            var minX = -10;
            var minZ = -5;
            var maxZ = -2;
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
