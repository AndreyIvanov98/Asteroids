using Asteroids.Domain.Components.Common;
using Asteroids.Presentation.UI.Scripts.MonoBehaviours;
using EcsCore;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids.Presentation.UI.Scripts.Systems
{
    public class CreateRestartMenu : IInitSystem
    {
        private EcsWorld _world;

        public void Init(EcsWorld world)
        {
            _world = world;
            new Filter(world)
                .Include<Ship>()
                .ForEach(entity => entity.Disposed += ShowRestartWindow);
        }

        private void ShowRestartWindow(Entity entity) =>
            new Filter(_world)
                .Include<Score>()
                .ForFirst(entity =>
                {
                    int score = entity.Get<Score>().Value;
                    var windowResource = Resources.Load<RestartWindow>("RestartWindow");
                    var window = Object.Instantiate(windowResource);

                    window.Setup(score.ToString(), () => SceneManager.LoadScene(0));
                });
    }
}