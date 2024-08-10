using Asteroids.Presentation.Scene.Components;
using EcsCore;
using UnityEngine;

namespace Asteroids.Presentation.Scene.Systems
{
    public class DestroyViewSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<View>()
                .Exclude<DisposeViewObserver>();

        public void Update() =>
            _filter.ForEach(entity =>
            {
                entity.Add<DisposeViewObserver>();
                entity.Disposed += DestroyView;
            });

        private void DestroyView(Entity entity)
        {
            GameObject view = entity.Get<View>().GameObject;
            Object.Destroy(view);
        }
    }
}