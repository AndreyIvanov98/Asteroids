using System;
using Asteroids.Domain.Components.Common;
using UnityEngine;

namespace Asteroids.Domain.Components.Extensions
{
    public static class RotationExtensions
    {
        public static Vector2 GetDirection(this Rotation rotation)
        {
            double angleRad = Mathf.Deg2Rad * rotation.Angle;
            return new Vector2((float) Math.Cos(angleRad), (float) Math.Sin(angleRad));
        }
    }
}