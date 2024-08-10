using Asteroids.Domain.Common;
using UnityEngine;

namespace Asteroids.Presentation.Services
{
    public interface IResourcesService
    {
        Sprite GetViewSprite(Tag tag);
        T Load<T>(string name) where T : Object;
    }
}