using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Components.Extensions;
using Asteroids.Domain.Components.SpaceShip;
using EcsCore;
using UnityEngine;

namespace Asteroids.Domain.Systems
{
    public class RayCollisionSystem : IInitSystem, IUpdateSystem
    {
        private Filter _enemiesLayerColliders;
        private Filter _playerRayColliders;

        public void Init(EcsWorld world)
        {
            _playerRayColliders = new Filter(world)
                .Include<RayCollider>()
                .Include<Position>()
                .Include<Rotation>();

            _enemiesLayerColliders = new Filter(world)
                .Include<EnemiesLayer>()
                .Include<CircleCollider>()
                .Include<Position>();
        }

        public void Update()
        {
            _playerRayColliders.ForEach(entity =>
            {
                Vector2 position = entity.Get<Position>().Value;
                Vector2 direction = entity.Get<Rotation>().GetDirection();
                float lenght = entity.Get<RayCollider>().Lenght;
                Vector2 end = position + direction * lenght; 

                _enemiesLayerColliders.ForEach(other =>
                {
                    Vector2 otherPosition = other.Get<Position>().Value;
                    float otherRadius = other.Get<CircleCollider>().Radius;

                    float sumOfDistances = Vector2.Distance(otherPosition, position)
                                           + Vector2.Distance(otherPosition, end);

                    float minDistance = Mathf.Sqrt(otherRadius * otherRadius + lenght * lenght / 4) * 2;

                    if (sumOfDistances <= minDistance)
                        other.Add<Destroy>();
                });
            });
        }
    }
}