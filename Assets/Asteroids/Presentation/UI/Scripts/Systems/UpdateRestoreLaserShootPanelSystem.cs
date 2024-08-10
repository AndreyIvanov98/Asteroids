using Asteroids.Domain.Components.SpaceShip;
using Asteroids.Presentation.UI.Scripts.Components;
using Asteroids.Presentation.UI.Scripts.MonoBehaviours;
using EcsCore;

namespace Asteroids.Presentation.UI.Scripts.Systems
{
    public class UpdateRestoreLaserShootPanelSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<RestoreLaserShootTimer>()
                .Include<RestoreLaserShootObserver>();

        public void Update() =>
            _filter.ForEach(entity =>
            {
                ValuePanel panel = entity.Get<RestoreLaserShootObserver>().Panel;
                RestoreLaserShootTimer timer = entity.Get<RestoreLaserShootTimer>();

                panel.SetValue($"{timer.Left:F2} c.");
            });
    }
}