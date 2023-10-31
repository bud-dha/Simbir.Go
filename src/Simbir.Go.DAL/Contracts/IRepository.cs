namespace Simbir.Go.DAL.Contracts
{
    public interface IRepository<T> where T : class
    {
        void Create(T entity);

        void Delete(T entity);

        void Update(T entity);

        public Task<IEnumerable<T>> GetAll();

        public Task<T> GetByIdAsync(long id);

        public T GetById(long id);
    }
}