using Asteroids.Domain.Components.Common;
using Asteroids.Presentation.Scene.Components;
using EcsCore;
using UnityEngine;

namespace Asteroids.Presentation.Scene.Systems
{
    public class UpdateViewRotationSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<View>()
                .Include<RotateView>()
                .Include<Rotation>();

        public void Update()
        {
            _filter.ForEach(entity =>
            {
                float angle = entity.Get<Rotation>().Angle;
                var view = entity.Get<View>().GameObject.transform;

                view.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            });
        }
    }
}