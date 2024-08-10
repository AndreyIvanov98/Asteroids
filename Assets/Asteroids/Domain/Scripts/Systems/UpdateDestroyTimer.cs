using Asteroids.Domain.Components.SpaceShip;
using Asteroids.Domain.Services;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class UpdateDestroyTimer : IInitSystem, IUpdateSystem
    {
        private readonly ITimeService _time;
        private EcsWorld _world;
        private Filter _filter;

        public UpdateDestroyTimer(ITimeService time) => 
            _time = time;

        public void Init(EcsWorld world)
        {
            _world = world;
            _filter = new Filter(world)
                .Include<DestroyTimer>();
        }

        public void Update()
        {
            float deltaTime = _time.DeltaTime;

            _filter.ForEach(entity =>
            {
                DestroyTimer timer = entity.Get<DestroyTimer>();
                timer.Left -= deltaTime;

                if (timer.Left <= 0f)
                    _world.DestroyEntity(entity);
            });
        }
    }
}