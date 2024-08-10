using EcsCore;
using UnityEngine;

namespace Asteroids.Domain.Components.SpaceShip
{
    public class SpawnPosition : IComponent
    {
        public Vector2 Point;
        public float DirectionAngle;
        public bool FromPlayer;
    }
}