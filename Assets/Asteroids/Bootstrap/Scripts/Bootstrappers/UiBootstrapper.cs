using Asteroids.Presentation.Services;
using Asteroids.Presentation.UI.Scripts.Systems;
using EcsCore;
using UnityEngine;

namespace Asteroids.Bootstrap.Bootstrappers
{
    public class UiBootstrapper : IBootstrapper
    {
        public void Setup(SystemsArray systems, ServiceLocator.Services services)
        {
            Transform canvas = services.Get<ISceneObjectsService>().Canvas.transform;
            IResourcesService resources = services.Get<IResourcesService>();

            systems
                .Add(new CreateScorePanelSystem(canvas, resources))
                .Add(new UpdateScorePanelSystem())
                .Add(new CreateCoordinatePanelSystem(canvas, resources))
                .Add(new UpdateCoordinatesPanelSystem())
                .Add(new CreateAnglePanelSystem(canvas, resources))
                .Add(new UpdateAnglePanelSystem())
                .Add(new CreateVelocityPanelSystem(canvas, resources))
                .Add(new UpdateVelocityPanelSystem())
                .Add(new CreateLaserShootsCountPanelSystem(canvas, resources))
                .Add(new UpdateLaserShootsCountPanelSystem())
                .Add(new CreateLaserShootRestorePanelSystem(canvas, resources))
                .Add(new UpdateRestoreLaserShootPanelSystem())
                .Add(new CreateRestartMenu());
        }
    }
}