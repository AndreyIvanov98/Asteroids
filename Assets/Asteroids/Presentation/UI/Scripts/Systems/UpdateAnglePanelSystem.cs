using Asteroids.Domain.Components.Common;
using Asteroids.Presentation.UI.Scripts.Components;
using Asteroids.Presentation.UI.Scripts.MonoBehaviours;
using EcsCore;

namespace Asteroids.Presentation.UI.Scripts.Systems
{
    public class UpdateAnglePanelSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<Rotation>()
                .Include<AngleObserver>();

        public void Update() =>
            _filter.ForEach(entity =>
            {
                ValuePanel panel = entity.Get<AngleObserver>().Panel;
                float angle = entity.Get<Rotation>().Angle;

                panel.SetValue($"{angle:F2}°");
            });
    }
}