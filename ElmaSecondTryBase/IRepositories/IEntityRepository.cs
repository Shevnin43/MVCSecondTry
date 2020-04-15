using System;
using System.Collections.Generic;
using System.Text;

namespace ElmaSecondTryBase.Repositories
{
    public interface IEntityRepository<TEntity> where TEntity : class
    {
        TEntity Create(TEntity entity);
        bool Delete(Guid id);
        TEntity Update(TEntity entity);
        IEnumerable<TEntity> GetEntities();
        TEntity Find(Guid id);
    }
}
