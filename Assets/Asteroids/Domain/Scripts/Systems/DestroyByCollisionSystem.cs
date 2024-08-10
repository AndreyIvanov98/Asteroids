using Asteroids.Domain.Components.Common;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class DestroyByCollisionSystem : IInitSystem, IUpdateSystem
    {
        private EcsWorld _world;
        private Filter _filter;

        public void Init(EcsWorld world)
        {
            _world = world;
            _filter = new Filter(world)
                .Include<Destroy>();
        }

        public void Update() => 
            _filter.ForEach(entity => _world.DestroyEntity(entity));
    }
}