using Asteroids.Domain.Services;
using Asteroids.Domain.Systems;
using Asteroids.Domain.Systems.Ufo;
using EcsCore;

namespace Asteroids.Bootstrap.Bootstrappers
{
    public class ModelBootstrapper : IBootstrapper
    {
        public void Setup(SystemsArray systems, ServiceLocator.Services services)
        {
            var input = services.Get<IInputService>();
            var time = services.Get<ITimeService>();
            var mapBorders = services.Get<IMapBorderService>();
            var randomService = services.Get<IRandomService>();
            var settings = services.Get<ISettingsService>();

            systems
                .Add(new CreateScoreSystem())
                .Add(new CollisionSystem())
                .Add(new RayCollisionSystem())
                .Add(new AddScoreSystem())

                .Add(new CreateShipSystem(settings))
                .Add(new AccelerationSystem(input, time))
                .Add(new RotationSystem(input, time))
                .Add(new MoveShipSystem(time))
                .Add(new FrictionSystem(input, time))
                .Add(new TeleportThroughBorderSystem(mapBorders))

                .Add(new ShootIntentSystem(input, settings))
                .Add(new CreateShootSystem())
                .Add(new UpdateShootCooldownSystem(time))
                .Add(new UpdateDestroyTimer(time))

                .Add(new LaserIntentSystem(input, settings))
                .Add(new UpdateLaserShootsCountSystem())
                .Add(new CreateLaserSystem())
                .Add(new UpdateLaserCooldownSystem(time))
                .Add(new UpdateLaserRestoreShootSystem(time))
                .Add(new UpdatePositionByParentSystem())
                .Add(new UpdateRotationByParentSystem())

                .Add(new CreateAsteroidIntentSystem(time, mapBorders, randomService, settings))
                .Add(new CreateAsteroidSystem(randomService, settings))
                .Add(new CreateSmallAsteroidsSystem(randomService, settings))

                .Add(new CreateUfoIntentSystem(time, mapBorders, randomService, settings))
                .Add(new CreateUfoSystem(settings))
                .Add(new UfoToShipDirectionAssignSystem())
                .Add(new UfoShootSystem(settings))

                .Add(new DestroyByCollisionSystem());
        }
    }
}
