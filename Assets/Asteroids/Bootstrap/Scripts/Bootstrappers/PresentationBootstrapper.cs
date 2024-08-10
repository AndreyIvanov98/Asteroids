using Asteroids.Domain.Services;
using Asteroids.Presentation.Scene.Systems;
using Asteroids.Presentation.Services;
using EcsCore;

namespace Asteroids.Bootstrap.Bootstrappers
{
    public class PresentationBootstrapper : IBootstrapper
    {
        public void Setup(SystemsArray systems, ServiceLocator.Services services)
        {
            ITimeService time = services.Get<ITimeService>();
            IRandomService random = services.Get<IRandomService>();
            ISceneObjectsService sceneObjects = services.Get<ISceneObjectsService>();
            IResourcesService resources = services.Get<IResourcesService>();

            systems
                .Add(new CreateViewSystem(resources, sceneObjects))
                .Add(new UpdateViewPositionSystem())
                .Add(new UpdateViewRotationSystem())
                .Add(new DestroyViewSystem())

                .Add(new AssignImaginaryRotationSystem(random))
                .Add(new ImaginaryRotateSystem(time))

                .Add(new CreateLaserViewSystem(sceneObjects, resources))
                .Add(new UpdateLaserViewSystem());

#if UNITY_EDITOR
            systems
                .Add(new SphereColliderDebugViewSystem())
                .Add(new RayColliderDebugViewSystem());
#endif
        }
    }
}