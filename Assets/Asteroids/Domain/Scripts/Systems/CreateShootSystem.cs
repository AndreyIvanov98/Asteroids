using Asteroids.Domain.Common;
using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Components.SpaceShip;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class CreateShootSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<CreateBulletIntent>();

        public void Update()
        {
            _filter.ForEach(entity =>
            {
                CreateShoot(entity);

                entity.Remove<CreateBulletIntent>();
                entity.Remove<SpawnPosition>();
            });
        }

        private void CreateShoot(Entity entity)
        {
            entity
                .Add<Position>()
                .Add<Rotation>()
                .Add<ViewTag>()
                .Add<Velocity>()
                .Add<DestroyTimer>()
                .Add<CircleCollider>();

            var spawnPosition = entity.Get<SpawnPosition>();
            
            if (spawnPosition.FromPlayer)
                entity.Add<PlayerLayer>();
            else
                entity.Add<EnemiesLayer>();

            Position position = entity.Get<Position>();
            position.Value = spawnPosition.Point;

            entity.Get<Rotation>().Angle = spawnPosition.DirectionAngle;
            entity.Get<Velocity>().Amount = 30f;
            entity.Get<CircleCollider>().Radius = .5f;
            entity.Get<DestroyTimer>().Left = 5f;
            entity.Get<ViewTag>().Tag = Tag.Bullet;
        }
    }
}