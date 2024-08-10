namespace Asteroids.Domain.Services
{
    public interface ISettingsService
    {
        ShipSettings Ship { get; }
        UfoSettings Ufo { get; }
        AsteroidSettings Asteroid { get; }
    }
}