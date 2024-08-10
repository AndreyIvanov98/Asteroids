using System;

namespace EcsCore
{
    public interface ISystem
    {
    }
    
    public interface IInitSystem : ISystem
    {
        void Init(EcsWorld world);
    }
    
    public interface IUpdateSystem : ISystem
    {
        void Update();
    }

    public interface IDisposeSystem : IDisposable, ISystem
    {
    }
}