using EcsCore;
using UnityEngine;

namespace Asteroids.Domain.Components.Asteroids
{
    public class CreateAsteroidIntent : IComponent
    {
        public Vector2 Point;
    }
}