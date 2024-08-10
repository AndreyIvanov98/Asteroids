using Asteroids.Domain.Components.Common;
using EcsCore;
using UnityEngine;

namespace Asteroids.Presentation.Scene.Systems
{
    public class SphereColliderDebugViewSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<Position>()
                .Include<CircleCollider>();

        public void Update() =>
            _filter.ForEach(entity =>
            {
                Vector2 position = entity.Get<Position>().Value;
                float radius = entity.Get<CircleCollider>().Radius;

                var right = position + Vector2.right * radius;
                var left = position + Vector2.left * radius;
                var up = position + Vector2.up * radius;
                var down = position + Vector2.down * radius;

                Debug.DrawLine(position, right, Color.green);
                Debug.DrawLine(position, left, Color.green);
                Debug.DrawLine(position, up, Color.green);
                Debug.DrawLine(position, down, Color.green);
            });
    }
}