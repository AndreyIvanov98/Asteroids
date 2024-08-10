using System;
using Asteroids.Domain.Common;

namespace Asteroids.Domain
{
    [Serializable]
    public class AsteroidSettings
    {
        public Tag Tag = Tag.Asteroid;
        public int SmallerAsteroidsCount = 3;
        public float SpawnTime = 2f;
        public float Velocity = 7f;
        public float ColliderRadius = 2f;
        public int ScoreForDestroy = 10;
    }
}