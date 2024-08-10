using Asteroids.Domain.Components.Common;
using Asteroids.Presentation.Services;
using Asteroids.Presentation.UI.Scripts.Components;
using Asteroids.Presentation.UI.Scripts.MonoBehaviours;
using EcsCore;
using UnityEngine;

namespace Asteroids.Presentation.UI.Scripts.Systems
{
    public class CreateCoordinatePanelSystem : IInitSystem, IUpdateSystem
    {
        private readonly Transform _canvas;
        private readonly IResourcesService _resources;
        private Filter _filter;

        public CreateCoordinatePanelSystem(Transform canvas, IResourcesService resources)
        {
            _canvas = canvas;
            _resources = resources;
        }

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<Ship>()
                .Include<Position>()
                .Exclude<CoordinatesObserver>();

        public void Update() =>
            _filter.ForEach(entity =>
            {
                var panelResource = _resources.Load<ValuePanel>("CoordinatesPanel");
                var panel = Object.Instantiate(panelResource, _canvas);

                entity.Add<CoordinatesObserver>();
                entity.Get<CoordinatesObserver>().Panel = panel;
            });
    }
}