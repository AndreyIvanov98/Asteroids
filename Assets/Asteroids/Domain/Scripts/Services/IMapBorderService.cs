using UnityEngine;

namespace Asteroids.Domain.Services
{
    public interface IMapBorderService
    {
        public float Width { get; }
        public float Height { get; }
        public Vector2 RandomSpawnPoint();
    }
}