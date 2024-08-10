using Asteroids.Domain.Components.SpaceShip;
using Asteroids.Domain.Services;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class UpdateLaserCooldownSystem : IInitSystem, IUpdateSystem
    {
        private readonly ITimeService _time;
        private Filter _filter;

        public UpdateLaserCooldownSystem(ITimeService time) => 
            _time = time;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<LaserCooldown>();

        public void Update()
        {
            float deltaTime = _time.DeltaTime;

            _filter.ForEach(entity =>
            {
                LaserCooldown cooldown = entity.Get<LaserCooldown>();
                cooldown.Time -= deltaTime;

                if (cooldown.Time <= 0f)
                    entity.Remove<LaserCooldown>();
            });
        }
    }
}