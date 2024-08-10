using Asteroids.Domain.Components;
using Asteroids.Domain.Components.Common;
using Asteroids.Domain.Components.SpaceShip;
using Asteroids.Domain.Services;
using EcsCore;

namespace Asteroids.Domain.Systems
{
    public class FrictionSystem : IInitSystem, IUpdateSystem
    {
        private readonly IInputService _inputService;
        private readonly ITimeService _timeService;
        private Filter _filter;

        public FrictionSystem(IInputService inputService, ITimeService timeService)
        {
            _inputService = inputService;
            _timeService = timeService;
        }

        public void Init(EcsWorld world)
        {
            _filter = new Filter(world)
                .Include<Friction>()
                .Include<Velocity>();
        }

        public void Update()
        {
            float deltaTime = _timeService.DeltaTime;

            if (_inputService.IsShipAccelerated)
                return;

            _filter.ForEach(entity =>
            {
                var friction = entity.Get<Friction>().Amount * deltaTime;
                var velocity = entity.Get<Velocity>();

                if (velocity.Amount < friction)
                    velocity.Amount = 0f;
                else
                    velocity.Amount -= friction;
            });
        }
    }
}