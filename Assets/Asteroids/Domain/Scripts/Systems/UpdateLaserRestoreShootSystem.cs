using Asteroids.Domain.Components.SpaceShip;
using Asteroids.Domain.Services;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class UpdateLaserRestoreShootSystem : IInitSystem, IUpdateSystem
    {
        private readonly ITimeService _time;
        private Filter _filter;

        public UpdateLaserRestoreShootSystem(ITimeService time) => 
            _time = time;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<LaserShootsCount>()
                .Include<RestoreLaserShootTimer>();

        public void Update()
        {
            float deltaTime = _time.DeltaTime;
            
            _filter.ForEach(entity =>
            {
                RestoreLaserShootTimer timer = entity.Get<RestoreLaserShootTimer>();
                LaserShootsCount count = entity.Get<LaserShootsCount>();
                timer.Left -= deltaTime;

                if (timer.Left > 0f)
                    return;

                if (count.Left >= count.Max)
                    return;

                count.Left++;
                
                if (count.Left == count.Max)
                    entity.Remove<RestoreLaserShootTimer>();
                else
                    timer.Left = 2f;
            });
        }
    }
}