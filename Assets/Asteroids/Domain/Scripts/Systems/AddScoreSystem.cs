using Asteroids.Domain.Components.Common;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class AddScoreSystem : IInitSystem, IUpdateSystem
    {
        private Filter _enemiesToDestroy;
        private Filter _score;

        public void Init(EcsWorld world)
        {
            _score = new Filter(world)
                .Include<Score>();

            _enemiesToDestroy = new Filter(world)
                .Include<EnemiesLayer>()
                .Include<ScoreForDestroy>()
                .Include<Destroy>();
        }

        public void Update()
        {
            _score.ForEach(scoreEntity =>
            {
                Score score = scoreEntity.Get<Score>();

                _enemiesToDestroy.ForEach(entity =>
                {
                    int amount = entity.Get<ScoreForDestroy>().Amount;

                    score.Value += amount;
                });
            });
        }
    }
}