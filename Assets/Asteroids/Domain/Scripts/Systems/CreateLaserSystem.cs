using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Components.SpaceShip;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class CreateLaserSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<CreateLaserIntent>();

        public void Update()
        {
            _filter.ForEach(entity =>
            {
                CreateLaser(entity);

                entity.Remove<CreateLaserIntent>();
                entity.Remove<SpawnPosition>();
            });
        }

        private void CreateLaser(Entity entity)
        {
            entity
                .Add<Position>()
                .Add<Rotation>()
                .Add<DestroyTimer>()
                .Add<RayCollider>();

            var spawnPosition = entity.Get<SpawnPosition>();
            
            if (spawnPosition.FromPlayer)
                entity.Add<PlayerLayer>();
            else
                entity.Add<EnemiesLayer>();

            Position position = entity.Get<Position>();
            position.Value = spawnPosition.Point;

            entity.Get<Rotation>().Angle = spawnPosition.DirectionAngle;
            entity.Get<RayCollider>().Lenght = 30f;
            entity.Get<DestroyTimer>().Left = .3f;
        }
    }
}