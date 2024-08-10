using Asteroids.Domain.Components.Common;
using Asteroids.Presentation.UI.Scripts.Components;
using Asteroids.Presentation.UI.Scripts.MonoBehaviours;
using EcsCore;

namespace Asteroids.Presentation.UI.Scripts.Systems
{
    public class UpdateVelocityPanelSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<Velocity>()
                .Include<VelocityObserver>();

        public void Update() =>
            _filter.ForEach(entity =>
            {
                ValuePanel panel = entity.Get<VelocityObserver>().Panel;
                float velocity = entity.Get<Velocity>().Amount;

                panel.SetValue($"{velocity:F2}");
            });
    }
}