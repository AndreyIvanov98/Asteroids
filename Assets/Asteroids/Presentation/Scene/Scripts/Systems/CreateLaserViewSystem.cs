using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Components.Extensions;
using Asteroids.Domain.Components.SpaceShip;
using Asteroids.Presentation.Scene.Components;
using Asteroids.Presentation.Services;
using EcsCore;
using UnityEngine;

namespace Asteroids.Presentation.Scene.Systems
{
    public class CreateLaserViewSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;
        private LineRenderer _viewResource;

        private readonly ISceneObjectsService _sceneObjects;
        private readonly IResourcesService _resources;

        public CreateLaserViewSystem(ISceneObjectsService sceneObjects, IResourcesService resources)
        {
            _sceneObjects = sceneObjects;
            _resources = resources;
        }

        public void Init(EcsWorld world)
        {
            _viewResource = _resources.Load<LineRenderer>("LaserView");
            _filter = new Filter(world)
                .Include<RayCollider>()
                .Include<Position>()
                .Include<Rotation>()
                .Exclude<LaserView>();
        }

        public void Update() =>
            _filter.ForEach(entity =>
            {
                Vector2 position = entity.Get<Position>().Value;
                Vector2 direction = entity.Get<Rotation>().GetDirection();
                float lenght = entity.Get<RayCollider>().Lenght;

                var viewInstance = Object.Instantiate(_viewResource, _sceneObjects.SpaceObjectsParent);
                viewInstance.positionCount = 2;
                viewInstance.SetPosition(0, position);
                viewInstance.SetPosition(1, position + direction * lenght);

                entity.Add<LaserView>();
                entity.Add<View>();
                entity.Get<View>().GameObject = viewInstance.gameObject;
            });
    }
}