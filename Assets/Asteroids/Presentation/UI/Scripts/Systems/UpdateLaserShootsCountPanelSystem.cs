using Asteroids.Domain.Components.SpaceShip;
using Asteroids.Presentation.UI.Scripts.Components;
using Asteroids.Presentation.UI.Scripts.MonoBehaviours;
using EcsCore;

namespace Asteroids.Presentation.UI.Scripts.Systems
{
    public class UpdateLaserShootsCountPanelSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<LaserShootsCount>()
                .Include<LaserShootsCountObserver>();

        public void Update() =>
            _filter.ForEach(entity =>
            {
                ValuePanel panel = entity.Get<LaserShootsCountObserver>().Panel;
                LaserShootsCount laserShootsCount = entity.Get<LaserShootsCount>();

                panel.SetValue($"{laserShootsCount.Left}/{laserShootsCount.Max}");
            });
    }
}