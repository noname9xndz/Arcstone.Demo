namespace Noname.UnitOfWork.Lib.Repositories.Interfaces
{
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Get repository that would allow CRUD operations over the given database entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type for which repository required</typeparam>
        /// <returns>Repository object</returns>
        ICrudRepository<T> GetCrudRepository<T>() where T : class;
    }
}