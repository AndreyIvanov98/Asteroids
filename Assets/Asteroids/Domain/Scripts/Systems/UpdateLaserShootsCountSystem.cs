using Asteroids.Domain.Components.SpaceShip;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class UpdateLaserShootsCountSystem : IInitSystem, IUpdateSystem
    {
        private Filter _intents;
        private Filter _laserShootCounts;

        public void Init(EcsWorld world)
        {
            _intents = new Filter(world)
                .Include<CreateLaserIntent>();

            _laserShootCounts = new Filter(world)
                .Include<LaserShootsCount>();
        }

        public void Update()
        {
            _intents.ForEach(intent =>
            {
               _laserShootCounts.ForEach(ship =>
               {
                   var shootCount = ship.Get<LaserShootsCount>();
                   shootCount.Left--;

                   if (!ship.Has<RestoreLaserShootTimer>())
                   {
                       ship.Add<RestoreLaserShootTimer>();
                       ship.Get<RestoreLaserShootTimer>().Left = 2f;
                   }
                   else
                   {
                       ship.Get<RestoreLaserShootTimer>().Left = 2f;
                   }
               });
            });
        }
    }
}