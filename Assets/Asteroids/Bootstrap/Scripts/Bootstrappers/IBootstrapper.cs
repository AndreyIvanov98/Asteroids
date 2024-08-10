using EcsCore;

namespace Asteroids.Bootstrap.Bootstrappers
{
    public interface IBootstrapper
    {
        void Setup(SystemsArray systems, ServiceLocator.Services services);
    }
}