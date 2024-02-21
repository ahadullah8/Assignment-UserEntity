namespace Assignment_UserEntity.Repositories.Contrat
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Update(T entity);
        void Delete(T entity);
        void Add(T entity);
        void Save();
    }
}
