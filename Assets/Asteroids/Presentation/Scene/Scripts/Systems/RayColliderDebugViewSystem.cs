using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Components.Extensions;
using Asteroids.Domain.Components.SpaceShip;
using EcsCore;
using UnityEngine;

namespace Asteroids.Presentation.Scene.Systems
{
    public class RayColliderDebugViewSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<Position>()
                .Include<Rotation>()
                .Include<RayCollider>();

        public void Update() =>
            _filter.ForEach(entity =>
            {
                Position position = entity.Get<Position>();
                Rotation rotation = entity.Get<Rotation>();
                RayCollider collider = entity.Get<RayCollider>();

                Vector2 direction = rotation.GetDirection();
                Vector2 start = position.Value;
                Vector2 end = start + direction * collider.Lenght;

                Debug.DrawLine(start, end, Color.green);
            });
    }
}