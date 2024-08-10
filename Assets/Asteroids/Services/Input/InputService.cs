using System;
using Asteroids.Domain.Services;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Services.Input
{
    public class InputService : IInputService, IDisposable
    {
        private ShipInputActions _shipInputActions;
        private InputAction _acceleration;
        private InputAction _rotation;
        private readonly InputAction _shoot;
        private readonly InputAction _laser;

        public bool IsShipAccelerated => Mathf.Approximately(_acceleration.ReadValue<float>(), 1f);
        public float RotateDirection => _rotation.ReadValue<float>();
        public bool CanShoot => Mathf.Approximately(_shoot.ReadValue<float>(), 1f);
        public bool CanShootLaser => Mathf.Approximately(_laser.ReadValue<float>(), 1f);

        public InputService()
        {
            _shipInputActions = new ShipInputActions();

            _acceleration = _shipInputActions.SpaceShip.Acceleration;
            _rotation = _shipInputActions.SpaceShip.Rotation;
            _shoot = _shipInputActions.SpaceShip.Shoot;
            _laser = _shipInputActions.SpaceShip.Laser;

            _acceleration.Enable();
            _rotation.Enable();
            _shoot.Enable();
            _laser.Enable();
        }

        public void Dispose()
        {
            _shipInputActions.Disable();
            _acceleration.Disable();
            _rotation.Disable();
            _shoot.Disable();
            _laser.Disable();
        }
    }
}
