using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Components.Extensions;
using Asteroids.Domain.Components.SpaceShip;
using Asteroids.Presentation.Scene.Components;
using EcsCore;
using UnityEngine;

namespace Asteroids.Presentation.Scene.Systems
{
    public class UpdateLaserViewSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<RayCollider>()
                .Include<Position>()
                .Include<Rotation>()
                .Include<LaserView>();

        public void Update() =>
            _filter.ForEach(entity =>
            {
                Vector2 position = entity.Get<Position>().Value;
                Vector2 direction = entity.Get<Rotation>().GetDirection();
                float lenght = entity.Get<RayCollider>().Lenght;

                LineRenderer viewInstance = entity.Get<View>().GameObject.GetComponent<LineRenderer>();
                viewInstance.positionCount = 2;
                viewInstance.SetPosition(0, position);
                viewInstance.SetPosition(1, position + direction * lenght);
            });
    }
}