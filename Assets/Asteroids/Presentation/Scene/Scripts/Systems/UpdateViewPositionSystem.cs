using Asteroids.Domain.Components.Common;
using Asteroids.Presentation.Scene.Components;
using EcsCore;
using UnityEngine;

namespace Asteroids.Presentation.Scene.Systems
{
    public class UpdateViewPositionSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<View>()
                .Include<Position>();

        public void Update()
        {
            _filter.ForEach(entity =>
            {
                Vector2 position = entity.Get<Position>().Value;
                var view = entity.Get<View>().GameObject.transform;

                view.position = position;
            });
        }
    }
}