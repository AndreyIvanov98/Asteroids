using System;

namespace ServiceLocator
{
    public class UnavailableServiceException : Exception
    {
        public UnavailableServiceException(Type service) 
            : base($"There is no registered service with type {service}") {}
    }
}