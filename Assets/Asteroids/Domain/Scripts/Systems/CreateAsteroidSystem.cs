using Asteroids.Domain.Components.Asteroids;
using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Services;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class CreateAsteroidSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;
        private EcsWorld _world;

        private readonly IRandomService _random;
        private readonly AsteroidSettings _settings;

        public CreateAsteroidSystem(IRandomService random, ISettingsService settings)
        {
            _random = random;
            _settings = settings.Asteroid;
        }

        public void Init(EcsWorld world)
        {
            _world = world;
            _filter = new Filter(world)
                .Include<CreateAsteroidIntent>();
        }

        public void Update() =>
            _filter.ForEach(entity =>
            {
                var intent = entity.Get<CreateAsteroidIntent>();

                CreateAsteroid(intent);

                entity.Remove<CreateAsteroidIntent>();
            });

        private void CreateAsteroid(CreateAsteroidIntent intent)
        {
            Entity asteroid = _world.NewEntity()
                .Add<ViewTag>()
                .Add<Position>()
                .Add<Rotation>()
                .Add<Velocity>()
                .Add<CircleCollider>()
                .Add<CanBeTeleported>()
                .Add<EnemiesLayer>()
                .Add<ScoreForDestroy>()
                .Add<BreaksDownIntoSmallAsteroids>();

            asteroid.Get<ViewTag>().Tag = _settings.Tag;
            var position = asteroid.Get<Position>();
            position.Value = intent.Point;
            asteroid.Get<Rotation>().Angle = _random.Direction;
            asteroid.Get<Velocity>().Amount = _settings.Velocity;
            asteroid.Get<CircleCollider>().Radius = _settings.ColliderRadius;
            asteroid.Get<ScoreForDestroy>().Amount = _settings.ScoreForDestroy;
        }
    }
}