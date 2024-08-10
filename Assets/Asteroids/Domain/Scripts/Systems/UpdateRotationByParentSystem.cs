using Asteroids.Domain.Components.Common;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class UpdateRotationByParentSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<Rotation>()
                .Include<Parent>();

        public void Update() =>
            _filter.ForEach(entity =>
            {
                Rotation rotation = entity.Get<Rotation>();
                Entity parent = entity.Get<Parent>().Entity;
                Rotation parentRotation = parent.Get<Rotation>();

                rotation.Angle = parentRotation.Angle;
            });
    }
}