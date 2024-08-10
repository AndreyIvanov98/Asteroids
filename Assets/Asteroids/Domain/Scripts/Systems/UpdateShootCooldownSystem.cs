using Asteroids.Domain.Components.SpaceShip;
using Asteroids.Domain.Services;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class UpdateShootCooldownSystem : IInitSystem, IUpdateSystem
    {
        private readonly ITimeService _time;
        private Filter _filter;

        public UpdateShootCooldownSystem(ITimeService time) => 
            _time = time;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<ShootCooldown>();

        public void Update()
        {
            float deltaTime = _time.DeltaTime;

            _filter.ForEach(entity =>
            {
                ShootCooldown cooldown = entity.Get<ShootCooldown>();
                cooldown.Time -= deltaTime;

                if (cooldown.Time <= 0f)
                    entity.Remove<ShootCooldown>();
            });
        }
    }
}