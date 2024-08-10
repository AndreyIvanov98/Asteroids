using Asteroids.Domain.Common;
using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Components.SpaceShip;
using Asteroids.Domain.Services;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class CreateShipSystem : IInitSystem
    {
        private readonly ShipSettings _settings;

        public CreateShipSystem(ISettingsService settings) => 
            _settings = settings.Ship;

        public void Init(EcsWorld world)
        {
            var ship = CreateShip(world);
            Setup(ship);
        }

        private static Entity CreateShip(EcsWorld world) =>
            world.NewEntity()
                .Add<ViewTag>()
                .Add<Ship>()
                .Add<Position>()
                .Add<Rotation>()
                .Add<RotationSpeed>()
                .Add<MaxVelocity>()
                .Add<Velocity>()
                .Add<AccelerationSpeed>()
                .Add<Friction>()
                .Add<CanRotateByPlayer>()
                .Add<CircleCollider>()
                .Add<CanAccelerateByPlayer>()
                .Add<CanShootByPlayer>()
                .Add<CanLaserShootByPlayer>()
                .Add<CanBeTeleported>()
                .Add<PlayerLayer>()
                .Add<LaserShootsCount>();

        private void Setup(Entity ship)
        {
            ship.Get<ViewTag>().Tag = _settings.Tag;
            ship.Get<MaxVelocity>().Amount = _settings.MaxVelocity;
            ship.Get<RotationSpeed>().Amount = _settings.RotationSpeed;
            ship.Get<AccelerationSpeed>().Amount = _settings.AccelerationSpeed;
            ship.Get<Friction>().Amount = _settings.Friction;
            ship.Get<CircleCollider>().Radius = _settings.ColliderRadius;
            LaserShootsCount shootsCount = ship.Get<LaserShootsCount>();
            shootsCount.Max = _settings.MaxLaserShoots;
            shootsCount.Left = _settings.MaxLaserShoots;
        }
    }
}