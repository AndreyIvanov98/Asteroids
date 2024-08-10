using System;
using Asteroids.Domain.Components.Common;
using EcsCore;
using UnityEngine;

namespace Asteroids.Domain.Systems
{
    public class CollisionSystem : IInitSystem, IUpdateSystem
    {
        private Filter _playerLayerColliders;
        private Filter _enemiesLayerColliders;

        public void Init(EcsWorld world)
        {
            _playerLayerColliders = new Filter(world)
                .Include<PlayerLayer>()
                .Include<CircleCollider>()
                .Include<Position>();

            _enemiesLayerColliders = new Filter(world)
                .Include<EnemiesLayer>()
                .Include<CircleCollider>()
                .Include<Position>();
        }

        public void Update() =>
            _playerLayerColliders.ForEach(playerLayerEntity =>
            {
                Position position = playerLayerEntity.Get<Position>();
                float radius = playerLayerEntity.Get<CircleCollider>().Radius;

                _enemiesLayerColliders.ForEach(enemiesLayerEntity =>
                {
                    Position enemyPosition = enemiesLayerEntity.Get<Position>();
                    float enemyRadius = enemiesLayerEntity.Get<CircleCollider>().Radius;

                    var distanceSqr = Vector2.SqrMagnitude(position.Value - enemyPosition.Value);
                    var radiusesSqr = Math.Pow(radius + enemyRadius, 2f);

                    if (distanceSqr < radiusesSqr)
                    {
                        if (!playerLayerEntity.Has<Destroy>())
                            playerLayerEntity.Add<Destroy>();

                        if (!enemiesLayerEntity.Has<Destroy>())
                            enemiesLayerEntity.Add<Destroy>();
                    }
                });
            });
    }
}