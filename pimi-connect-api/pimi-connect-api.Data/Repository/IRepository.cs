namespace pimi_connect_app.Data.Repository;

public interface IRepository<TEntity>
{
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity?> GetById(Guid id);
    Task<TEntity> Add(TEntity entity);
    Task<TEntity> Update(TEntity entity);
    Task Delete(TEntity entity);
    Task Save();
}