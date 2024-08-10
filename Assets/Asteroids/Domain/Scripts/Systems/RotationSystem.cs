using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Components.SpaceShip;
using Asteroids.Domain.Services;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class RotationSystem : IInitSystem, IUpdateSystem
    {
        private readonly IInputService _inputService;
        private readonly ITimeService _timeService;
        private Filter _filter;

        public RotationSystem(IInputService inputService, ITimeService timeService)
        {
            _inputService = inputService;
            _timeService = timeService;
        }

        public void Init(EcsWorld world) =>
            _filter = new Filter(world)
                .Include<CanRotateByPlayer>()
                .Include<RotationSpeed>()
                .Include<Rotation>();

        public void Update()
        {
            float direction = _inputService.RotateDirection;
            float deltaTime = _timeService.DeltaTime;

            _filter.ForEach(entity =>
            {
                var rotation = entity.Get<Rotation>();
                var rotationSpeed = entity.Get<RotationSpeed>().Amount;

                rotation.Angle += direction * rotationSpeed * deltaTime;

                if (rotation.Angle > 360f)
                    rotation.Angle -= 360f;
                else if (rotation.Angle < -360f)
                    rotation.Angle += 360f;
            });
        }
    }
}