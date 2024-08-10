using Asteroids.Domain.Components.Common;
using Asteroids.Presentation.UI.Scripts.Components;
using Asteroids.Presentation.UI.Scripts.MonoBehaviours;
using EcsCore;
using UnityEngine;

namespace Asteroids.Presentation.UI.Scripts.Systems
{
    public class UpdateCoordinatesPanelSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<Position>()
                .Include<CoordinatesObserver>();

        public void Update() =>
            _filter.ForEach(entity =>
            {
                ValuePanel panel = entity.Get<CoordinatesObserver>().Panel;
                Vector2 position = entity.Get<Position>().Value;

                panel.SetValue($"{position.x:F2} ; {position.y:F2}");
            });
    }
}