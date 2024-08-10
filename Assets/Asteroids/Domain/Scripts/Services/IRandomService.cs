using UnityEngine;

namespace Asteroids.Domain.Services
{
    public interface IRandomService
    {
        bool IsTrue { get; }
        float Range(float min, float max);
        float RandomSign(float value);
        float Direction { get; }
        Vector2 RandomPosition(float width, float height);
    }
}