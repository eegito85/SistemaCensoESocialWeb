using System.Collections.Generic;

namespace Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories.Common
{
    public interface IRepostitoryBase<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        TEntity GetById(int? id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity obj);
        void Remove(TEntity obj);
    }
}
