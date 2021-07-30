using Noname.UnitOfWork.Lib.Entities.Interfaces;

namespace Noname.UnitOfWork.Lib.Entities
{
    /// <summary>
    ///  Abstract entity class
    /// </summary>
    /// <typeparam name="TId">The Id type</typeparam>
    public abstract class Entity<TId> : IEntity<TId>
    {
        public TId Id { get; set; }

        public bool IsTransient()
        {
            return Id.Equals(default(TId));
        }
    }
}