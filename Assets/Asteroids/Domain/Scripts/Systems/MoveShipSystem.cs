using System;
using Asteroids.Domain.Components;
using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Components.Extensions;
using Asteroids.Domain.Services;
using EcsCore;
using UnityEngine;

namespace Asteroids.Domain.Systems
{
    public class MoveShipSystem : IInitSystem, IUpdateSystem
    {
        private readonly ITimeService _timeService;
        private Filter _filter;

        public MoveShipSystem(ITimeService timeService) => 
            _timeService = timeService;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<Velocity>()
                .Include<Position>();

        public void Update()
        {
            float deltaTime = _timeService.DeltaTime;

            _filter.ForEach(entity =>
            {
                Position position = entity.Get<Position>();
                float velocity = entity.Get<Velocity>().Amount;
                Rotation rotation = entity.Get<Rotation>();
                Vector2 direction = rotation.GetDirection();

                position.Value += direction * velocity * deltaTime;
            });
        }
    }
}