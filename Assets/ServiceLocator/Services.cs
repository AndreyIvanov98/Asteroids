using System;
using System.Collections.Generic;

namespace ServiceLocator
{
    public class Services
    {
        private readonly Dictionary<Type, object> _services = new();

        public Services RegisterSingle<T>(T service)
        {
            if (_services.ContainsKey(typeof(T)))
                throw new Exception($"Already instantiated service {typeof(T)}");

            _services[typeof(T)] = service;

            return this;
        }

        public T Get<T>()
        {
            if (!_services.ContainsKey(typeof(T)))
                throw new UnavailableServiceException(typeof(T));

            return (T) _services[typeof(T)];
        }
    }
}
