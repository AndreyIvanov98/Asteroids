using Asteroids.Domain.Components.Common;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class CreateScoreSystem : IInitSystem
    {
        public void Init(EcsWorld world) => 
            world
                .NewEntity()
                .Add<Score>();
    }
}