using System;
using Asteroids.Domain.Common;

namespace Asteroids.Domain
{
    [Serializable]
    public class ShipSettings
    {
        public Tag Tag = Tag.SpaceShip;
        public float MaxVelocity = 18f;
        public float RotationSpeed = 180f;
        public float AccelerationSpeed = 15f;
        public float Friction = 15f;
        public float ColliderRadius = 2f;
        public int MaxLaserShoots = 5;
        public float LaserCooldown = 1f;
        public float ShootCooldown = .25f;
        public float ShotOffset = 2f;
    }
}