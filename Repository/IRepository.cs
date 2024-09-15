namespace E_Learning.Repository.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> GetById(int id);

        Task<List<T>> GetAll();

        Task Add(T entity);

        Task Update(T entity);

        Task Delete(T entity);

        Task<bool> DeleteById(int id);
    }
}