using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Components.Extensions;
using EcsCore;
using UnityEngine;

namespace Asteroids.Domain.Systems
{
    public class UpdatePositionByParentSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<Position>()
                .Include<Parent>();

        public void Update() =>
            _filter.ForEach(entity =>
            {
                Position position = entity.Get<Position>();
                Parent parent = entity.Get<Parent>();
                Vector2 parentPosition = parent.Entity.Get<Position>().Value;
                Vector2 direction = parent.Entity.Get<Rotation>().GetDirection();

                position.Value = parentPosition + direction * parent.Distance;
            });
    }
}