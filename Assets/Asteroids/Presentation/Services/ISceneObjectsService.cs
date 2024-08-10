using UnityEngine;

namespace Asteroids.Presentation.Services
{
    public interface ISceneObjectsService
    {
        Camera Camera { get; }
        Canvas Canvas { get; }
        Transform SpaceObjectsParent { get; }
    }
}