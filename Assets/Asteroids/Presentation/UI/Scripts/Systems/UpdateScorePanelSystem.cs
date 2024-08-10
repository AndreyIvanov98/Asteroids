using Asteroids.Domain.Components.Common;
using Asteroids.Presentation.UI.Scripts.Components;
using Asteroids.Presentation.UI.Scripts.MonoBehaviours;
using EcsCore;

namespace Asteroids.Presentation.UI.Scripts.Systems
{
    public class UpdateScorePanelSystem : IInitSystem, IUpdateSystem
    {
        private Filter _filter;

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<Score>()
                .Include<ScoreObserver>();

        public void Update() =>
            _filter.ForEach(entity =>
            {
                ValuePanel panel = entity.Get<ScoreObserver>().Panel;
                int score = entity.Get<Score>().Value;

                panel.SetValue(score.ToString());
            });
    }
}