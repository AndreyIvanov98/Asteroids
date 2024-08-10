using System.Collections.Generic;

namespace EcsCore
{
    public sealed class EcsWorld
    {
        internal readonly List<Entity> Entities = new();

        public Entity NewEntity()
        {
            var entity = new Entity();
            Entities.Add(entity);

            entity.CanDispose += Remove;

            return entity;
        }

        public void DestroyEntity(Entity entity)
        {
            entity.CanDispose -= Remove;
            Remove(entity);
            entity.Dispose();
        }

        private void Remove(Entity entity) => 
            Entities.Remove(entity);
    }
}
