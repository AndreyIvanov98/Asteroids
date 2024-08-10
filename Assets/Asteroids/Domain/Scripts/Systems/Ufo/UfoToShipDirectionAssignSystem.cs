using Asteroids.Domain.Components.Common;
using EcsCore;
using UnityEngine;

namespace Asteroids.Domain.Systems.Ufo
{
    public class UfoToShipDirectionAssignSystem : IInitSystem, IUpdateSystem
    {
        private Filter _ufos;
        private Filter _ship;

        public void Init(EcsWorld world)
        {
            _ufos = new Filter(world)
                .Include<Components.Ufo.Ufo>()
                .Include<Rotation>()
                .Include<Position>();

            _ship = new Filter(world)
                .Include<Ship>()
                .Include<Position>();
        }

        public void Update()
        {
            _ufos.ForEach(ufo =>
            {
                Vector2 position = ufo.Get<Position>().Value;
                Rotation rotation = ufo.Get<Rotation>();

                _ship.ForEach(ship =>
                {
                    Vector2 shipPosition = ship.Get<Position>().Value;
                    rotation.Angle = Vector2.SignedAngle(Vector2.right, (shipPosition - position).normalized);
                });
            });
        }
    }
}