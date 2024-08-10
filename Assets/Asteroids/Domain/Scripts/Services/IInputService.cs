namespace Asteroids.Domain.Services
{
    public interface IInputService
    {
        bool IsShipAccelerated { get; }
        float RotateDirection { get; }
        bool CanShoot { get; }
        bool CanShootLaser { get; }
    }
}
