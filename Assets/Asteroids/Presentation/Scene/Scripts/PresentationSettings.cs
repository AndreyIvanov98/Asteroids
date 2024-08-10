using System.Collections.Generic;
using Asteroids.Domain.Common;

namespace Asteroids.Presentation.Scene
{
    public static class PresentationSettings
    {
        public static readonly Dictionary<Tag, bool> ViewTagToRotationPossibility = new()
        {
            [Tag.SpaceShip] = true,
            [Tag.Asteroid] = true,
            [Tag.SmallAsteroid] = true,
            [Tag.Bullet] = true,
            [Tag.Ufo] = false,
        };

        public static readonly List<Tag> CanImaginaryRotate = new()
        {
            Tag.Asteroid,
            Tag.SmallAsteroid,
        };
    }
}