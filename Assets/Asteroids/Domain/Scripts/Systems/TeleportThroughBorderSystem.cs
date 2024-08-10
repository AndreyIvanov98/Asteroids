using System;
using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Services;
using EcsCore;
using UnityEngine;

namespace Asteroids.Domain.Systems
{
    public class TeleportThroughBorderSystem : IInitSystem, IUpdateSystem
    {
        private readonly IMapBorderService _mapBorderService;
        private Filter _filter;

        public TeleportThroughBorderSystem(IMapBorderService mapBorderService) => 
            _mapBorderService = mapBorderService;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<Position>()
                .Include<CanBeTeleported>();

        public void Update() =>
            _filter.ForEach(entity =>
            {
                Position position = entity.Get<Position>();

                if (Math.Abs(position.Value.x) > _mapBorderService.Width + 1f)
                    position.Value.x = -Mathf.Sign(position.Value.x) * _mapBorderService.Width;

                if (Math.Abs(position.Value.y) > _mapBorderService.Height + 1f)
                    position.Value.y = -Mathf.Sign(position.Value.y) * _mapBorderService.Height;
            });
    }
}