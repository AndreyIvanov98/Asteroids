using Asteroids.Domain.Components.Common;
using Asteroids.Presentation.Services;
using Asteroids.Presentation.UI.Scripts.Components;
using Asteroids.Presentation.UI.Scripts.MonoBehaviours;
using EcsCore;
using UnityEngine;

namespace Asteroids.Presentation.UI.Scripts.Systems
{
    public class CreateScorePanelSystem : IInitSystem, IUpdateSystem
    {
        private readonly Transform _canvas;
        private readonly IResourcesService _resources;
        private Filter _filter;

        public CreateScorePanelSystem(Transform canvas, IResourcesService resources)
        {
            _canvas = canvas;
            _resources = resources;
        }

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<Score>()
                .Exclude<ScoreObserver>();

        public void Update() =>
            _filter.ForEach(entity =>
            {
                var panelResource = _resources.Load<ValuePanel>("ScorePanel");
                var panel = Object.Instantiate(panelResource, _canvas);

                entity.Add<ScoreObserver>();
                entity.Get<ScoreObserver>().Panel = panel;
            });
    }
}