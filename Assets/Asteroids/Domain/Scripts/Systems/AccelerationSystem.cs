using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Components.SpaceShip;
using Asteroids.Domain.Services;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class AccelerationSystem : IInitSystem, IUpdateSystem
    {
        private readonly IInputService _inputService;
        private readonly ITimeService _timeService;
        private Filter _filter;

        public AccelerationSystem(IInputService inputService, ITimeService timeService)
        {
            _inputService = inputService;
            _timeService = timeService;
        }

        public void Init(EcsWorld world)
        {
            _filter = new Filter(world)
                .Include<CanAccelerateByPlayer>()
                .Include<Velocity>()
                .Include<MaxVelocity>()
                .Include<AccelerationSpeed>();
        }

        public void Update()
        {
            if (_inputService.IsShipAccelerated)
                Accelerate();
        }

        private void Accelerate()
        {
            float deltaTime = _timeService.DeltaTime;

            _filter.ForEach(entity =>
            {
                Velocity velocity = entity.Get<Velocity>();
                float accelerationSpeed = entity.Get<AccelerationSpeed>().Amount;
                float maxVelocity = entity.Get<MaxVelocity>().Amount;

                velocity.Amount += accelerationSpeed * deltaTime;

                if (velocity.Amount > maxVelocity)
                    velocity.Amount = maxVelocity;
            });
        }
    }
}