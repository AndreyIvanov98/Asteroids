using Asteroids.Domain;
using Asteroids.Domain.Services;
using UnityEngine;

namespace Asteroids.Services
{
    [CreateAssetMenu(menuName = "Asteroids/Create ModelSettings", fileName = "ModelSettings", order = 0)]
    public class ModelSettings : ScriptableObject, ISettingsService
    {
        public ShipSettings Ship => _shipSettings;
        public UfoSettings Ufo => _ufoSettings;
        public AsteroidSettings Asteroid => _asteroidSettings;

        [SerializeField] private ShipSettings _shipSettings;
        [SerializeField] private UfoSettings _ufoSettings;
        [SerializeField] private AsteroidSettings _asteroidSettings;
    }
}