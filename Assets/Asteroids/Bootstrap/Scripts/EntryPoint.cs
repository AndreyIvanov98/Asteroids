using Asteroids.Bootstrap.Bootstrappers;
using Asteroids.Domain.Services;
using Asteroids.Presentation.Services;
using Asteroids.Services;
using Asteroids.Services.Input;
using EcsCore;
using UnityEngine;

namespace Asteroids.Bootstrap
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private SceneObjects _sceneObjects;
        [SerializeField] private ModelSettings _modelSettings;

        private EcsWorld _world;
        private SystemsArray _systems;
        private ServiceLocator.Services _services;

        private IBootstrapper[] _bootstrappers = 
        {
            new ModelBootstrapper(),
            new PresentationBootstrapper(),
            new UiBootstrapper(),
        };

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new SystemsArray();

            SetupServices();
            SetupBootstrappers();
        }

        private void Update() => 
            _systems.Update();

        private void OnDestroy() =>
            _systems.Dispose();

        private void SetupServices()
        {
            var random = new RandomService();

            _services = new ServiceLocator.Services()
                .RegisterSingle<ISettingsService>(_modelSettings)
                .RegisterSingle<ISceneObjectsService>(_sceneObjects)
                .RegisterSingle<IInputService>(new InputService())
                .RegisterSingle<ITimeService>(new TimeService())
                .RegisterSingle<IMapBorderService>(new MapService(_sceneObjects, random))
                .RegisterSingle<IRandomService>(random)
                .RegisterSingle<IResourcesService>(new ResourcesService());
        }

        private void SetupBootstrappers()
        {
            foreach (IBootstrapper bootstrapper in _bootstrappers) 
                bootstrapper.Setup(_systems, _services);

            _systems.Init(_world);
        }
    }
}
