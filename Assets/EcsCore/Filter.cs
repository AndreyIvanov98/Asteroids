using System;
using System.Collections.Generic;
using System.Linq;

namespace EcsCore
{
    public sealed class Filter
    {
        private readonly EcsWorld _world;
        private readonly Dictionary<Type, Mode> _conditions = new();

        private static readonly Dictionary<Mode, Func<Entity, Type, bool>> s_modeConditions = new()
        {
            [Mode.Include] = (entity, type) => entity.Has(type),
            [Mode.Exclude] = (entity, type) => !entity.Has(type),
        };

        public Filter(EcsWorld world)
        {
            _world = world;
        }

        public Filter Include<T>() where T : IComponent
        {
            _conditions.Add(typeof(T), Mode.Include);
            return this;
        }

        public Filter Exclude<T>() where T : IComponent
        {
            _conditions.Add(typeof(T), Mode.Exclude);
            return this;
        }

        public void ForEach(Action<Entity> forEachAction)
        {
            var entities = GetEntitiesByConditions();

            foreach (Entity entity in entities) 
                forEachAction.Invoke(entity);
        }

        public void ForFirst(Action<Entity> action)
        {
            var entities = GetEntitiesByConditions();

            if (entities.Count == 0)
                return;

            action.Invoke(entities.First());
        }

        private List<Entity> GetEntitiesByConditions()
        {
            var entities = _world.Entities;

            foreach (var (type, mode) in _conditions)
                entities = entities
                    .Where(x => s_modeConditions[mode].Invoke(x, type))
                    .ToList();

            return entities;
        }

        private enum Mode
        {
            Include,
            Exclude
        }
    }
}