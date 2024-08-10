using Asteroids.Domain.Services;
using UnityEngine;

namespace Asteroids.Services
{
    public class TimeService : ITimeService
    {
        public float DeltaTime => Time.deltaTime;
    }
}