using System;
using System.Collections.Generic;

namespace EcsCore
{
    public sealed class Entity : IDisposable
    {
        public event Action<Entity> Disposed;
        internal event Action<Entity> CanDispose;

        private readonly List<IComponent> _components;

        internal Entity() => 
            _components = new();

        public T Get<T>() where T : IComponent
        {
            if (!Has<T>())
                throw new Exception($"Entity doesn't contain a component with type {typeof(T)}");

            return (T) _components.Find(x => x.GetType() == typeof(T));
        }

        public bool Has(Type type) =>
            _components.Find(x => x.GetType() == type) != null;
        
        public bool Has<T>() where T : IComponent => 
            _components.Find(x => x.GetType() == typeof(T)) != null;

        public Entity Add<T>() where T : IComponent, new()
        {
            if (Has<T>())
                throw new Exception($"Entity already contain a component with type {typeof(T)}. You cannot add it twice");

            T component = new T();
            _components.Add(component);

            return this;
        }

        public void Remove<T>() where T : IComponent
        {
            if (Has<T>())
                _components.Remove(Get<T>());

            if (_components.Count == 0)
                CanDispose?.Invoke(this);
        }

        public void Dispose() => 
            Disposed?.Invoke(this);
    }
}