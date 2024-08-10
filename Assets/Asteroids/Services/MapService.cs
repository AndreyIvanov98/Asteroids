using Asteroids.Domain.Services;
using Asteroids.Presentation.Services;
using UnityEngine;

namespace Asteroids.Services
{
    public class MapService : IMapBorderService
    {
        private readonly IRandomService _random;

        public MapService(ISceneObjectsService sceneObjects, IRandomService random)
        {
            _random = random;

            var size = sceneObjects.Camera.ViewportToWorldPoint(Vector3.one);
            Width = size.x;
            Height = size.y;
        }

        public float Width { get; }
        public float Height { get; }

        public Vector2 RandomSpawnPoint()
        {
            {
                Vector2 position;

                if (_random.IsTrue)
                {
                    position.x = _random.Range(-Width, Width);
                    position.y = _random.RandomSign(Height);
                }
                else
                {
                    position.x = _random.RandomSign(Width);
                    position.y = _random.Range(-Height, Height);
                }

                return position;
            }
        }
    }
}