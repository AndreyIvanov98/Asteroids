using System;
using Asteroids.Domain.Common;

namespace Asteroids.Domain
{
    [Serializable]
    public class UfoSettings
    {
        public Tag Tag = Tag.Ufo;
        public float Velocity = 7f;
        public float ColliderRadius = 2f;
        public int ScoreForDestroy = 30;
        public float SpawnTime = 5f;
        public float ShootCooldown = 3f;
        public float ShotOffset = 2f;
    }
}