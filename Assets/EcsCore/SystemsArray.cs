using System.Collections.Generic;

namespace EcsCore
{
    public sealed class SystemsArray
    {
        private readonly List<IInitSystem> _initSystems = new();
        private readonly List<IUpdateSystem> _updateSystems = new();
        private readonly List<IDisposeSystem> _disposeSystems = new();

        public SystemsArray Add(ISystem system)
        {
            if (system is IInitSystem initSystem)
                _initSystems.Add(initSystem);

            if (system is IUpdateSystem updateSystem)
                _updateSystems.Add(updateSystem);

            if (system is IDisposeSystem disposeSystem)
                _disposeSystems.Add(disposeSystem);

            return this;
        }

        public void Init(EcsWorld world) => 
            _initSystems.ForEach(x => x.Init(world));

        public void Update() => 
            _updateSystems.ForEach(x => x.Update());

        public void Dispose() => 
            _disposeSystems.ForEach(x => x.Dispose());
    }
}