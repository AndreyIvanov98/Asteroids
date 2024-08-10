using Asteroids.Presentation.Services;
using UnityEngine;

namespace Asteroids.Services
{
    public class SceneObjects : MonoBehaviour, ISceneObjectsService
    {
        public Camera Camera => _camera;
        public Canvas Canvas => _canvas;
        public Transform SpaceObjectsParent => _sceneObjectsParent;

        [SerializeField] private Camera _camera;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Transform _sceneObjectsParent;
    }
}