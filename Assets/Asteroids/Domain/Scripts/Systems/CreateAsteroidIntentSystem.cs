using Asteroids.Domain.Components.Asteroids;
using Asteroids.Domain.Services;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class CreateAsteroidIntentSystem : IInitSystem, IUpdateSystem
    {
        private readonly ITimeService _time;
        private readonly IMapBorderService _map;
        private readonly IRandomService _random;
        private readonly AsteroidSettings _settings;
        private float _currentTime;
        private EcsWorld _world;

        public CreateAsteroidIntentSystem(
            ITimeService time,
            IMapBorderService map,
            IRandomService random,
            ISettingsService settings)
        {
            _time = time;
            _map = map;
            _random = random;
            _settings = settings.Asteroid;
        }

        public void Init(EcsWorld world)
        {
            _world = world;
            _currentTime = _settings.SpawnTime;
        }

        public void Update()
        {
            _currentTime -= _time.DeltaTime;

            if (_currentTime <= 0f)
            {
                CreateSpawnIntent();
                _currentTime = _settings.SpawnTime;
            }
        }

        private void CreateSpawnIntent()
        {
            var entity = _world.NewEntity().Add<CreateAsteroidIntent>();
            var intent = entity.Get<CreateAsteroidIntent>();

            intent.Point = _random.RandomPosition(_map.Width, _map.Height);
        }
    }
}