using System.Collections.Generic;
using Asteroids.Domain.Common;
using Asteroids.Presentation.Services;
using UnityEngine;

namespace Asteroids.Services
{
    public class ResourcesService : IResourcesService
    {
        private static readonly Dictionary<Tag, string[]> s_randomSprites = new()
        {
            [Tag.SmallAsteroid] = new[] {"SmallAsteroid0", "SmallAsteroid1", "SmallAsteroid2"},
        };

        public Sprite GetViewSprite(Tag tag)
        {
            if (s_randomSprites.ContainsKey(tag))
                return LoadRandomSprite(tag);
            else
                return Resources.Load<Sprite>(tag.ToString());
        }

        public T Load<T>(string name) where T : Object => 
            Resources.Load<T>(name);

        private static Sprite LoadRandomSprite(Tag tag)
        {
            string name = s_randomSprites[tag][Random.Range(0, s_randomSprites[tag].Length)];
            return Resources.Load<Sprite>(name);
        }
    }
}