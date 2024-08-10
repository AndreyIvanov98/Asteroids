using Asteroids.Domain.Services;
using UnityEngine;

namespace Asteroids.Services
{
    public class RandomService : IRandomService
    {
        public bool IsTrue => Random.value > .5f;

        public float Range(float min, float max) => 
            Random.Range(min, max);

        public float RandomSign(float value) => 
            IsTrue ? value : -value;

        public float Direction => 
            Range(0f, 360f);

        public Vector2 RandomPosition(float width, float height)
        {
            Vector2 position;

            if (IsTrue)
            {
                position.x = Range(-width, width);
                position.y = RandomSign(height);
            }
            else
            {
                position.x = RandomSign(width);
                position.y = Range(-height, height);
            }

            return position;
        }
    }
}