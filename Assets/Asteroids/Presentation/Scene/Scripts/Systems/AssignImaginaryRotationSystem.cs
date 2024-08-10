using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Services;
using Asteroids.Presentation.Scene.Components;
using EcsCore;
using UnityEngine;

namespace Asteroids.Presentation.Scene.Systems
{
    public class AssignImaginaryRotationSystem : IInitSystem, IUpdateSystem
    {
        private readonly IRandomService _random;
        private Filter _filter;

        public AssignImaginaryRotationSystem(IRandomService random) => 
            _random = random;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<View>()
                .Include<ViewTag>()
                .Exclude<ImaginaryRotation>();

        public void Update()
        {
            _filter.ForEach(entity =>
            {
                var tag = entity.Get<ViewTag>().Tag;

                if (PresentationSettings.CanImaginaryRotate.Contains(tag))
                    AssignComponent(entity);
            });
        }

        private void AssignComponent(Entity entity)
        {
            entity.Add<ImaginaryRotation>();

            var rotation = entity.Get<ImaginaryRotation>();
            rotation.Speed = _random.Direction;
            rotation.Graphics = entity.Get<View>()
                .GameObject
                .GetComponentInChildren<SpriteRenderer>()
                .transform;
        }
    }
}