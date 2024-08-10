using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Components.Extensions;
using Asteroids.Domain.Components.SpaceShip;
using Asteroids.Domain.Services;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class LaserIntentSystem : IInitSystem, IUpdateSystem
    {
        private readonly IInputService _input;
        private readonly ShipSettings _settings;

        private EcsWorld _world;
        private Filter _filter;

        public LaserIntentSystem(IInputService input, ISettingsService settings)
        {
            _input = input;
            _settings = settings.Ship;
        }

        public void Init(EcsWorld world)
        {
            _world = world;
            _filter = new Filter(world)
                .Include<CanLaserShootByPlayer>()
                .Include<LaserShootsCount>()
                .Include<Position>()
                .Include<Rotation>()
                .Exclude<LaserCooldown>();
        }

        public void Update()
        {
            if (!_input.CanShootLaser)
                return;

            _filter.ForEach(entity =>
            {
                var shootsCount = entity.Get<LaserShootsCount>().Left;

                if (shootsCount <= 0)
                    return;

                CreateIntent(entity);
                AddCooldown(entity);
            });
        }

        private void CreateIntent(Entity entity)
        {
            entity.CreateSpawnPosition(_world, _settings.ShotOffset, true, out var intentEntity);
            intentEntity.Add<CreateLaserIntent>();
            intentEntity.Add<Parent>();
            Parent parent = intentEntity.Get<Parent>();
            parent.Entity = entity;
            parent.Distance = _settings.ShotOffset;
        }

        private void AddCooldown(Entity entity)
        {
            entity.Add<LaserCooldown>();
            entity.Get<LaserCooldown>().Time = _settings.LaserCooldown;
        }
    }
}