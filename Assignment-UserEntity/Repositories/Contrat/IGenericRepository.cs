namespace Assignment_UserEntity.Repositories.Contrat
{
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Get the entity from the db and returns queryable
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();
        /// <summary>
        /// Gets the object by id from the db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(object id);
        /// <summary>
        /// updates the tracked entity in the db
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
        /// <summary>
        /// dletes the entered entity from the db
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
        /// <summary>
        /// adds new entity in the db
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);
        /// <summary>
        /// save changes to the db
        /// </summary>
        void Save();
    }
}
